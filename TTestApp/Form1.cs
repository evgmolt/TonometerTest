using HRV;
using System.Drawing.Drawing2D;
using TTestApp.Commands;
using TTestApp.Enums;
using TTestApp.Spline;

namespace TTestApp
{
    public partial class Form1 : Form, IMessageHandler
    {
        USBserialPort USBPort;
        DataArrays? DataA;
        ByteDecomposer Decomposer;
        WaveDetector? Detector;
        CurvesPainter Painter;
        BufferedPanel BufPanel;
        TTestConfig Cfg;
        StreamWriter TextWriter;
        string CurrentFile;
        int CurrentFileSize;
        const string TmpDataFile = "tmpdata.t";
        int MaxValue = 200000;
        bool ViewMode = false;
        int ViewShift;
        double ScaleY = 1;
        List<int[]> VisirList;
        bool Compression;
        int PressureMeasStatus = (int)Enums.PressureMeasurementStatus.Ready;

        int MaxPressure = 0;
        int MinPressure = 300;

        GigaDeviceStatus GigaDevStatus;

        public event Action<Message> WindowsMessage;

        public Form1()
        {
            InitializeComponent();
            BufPanel = new BufferedPanel(0);
            BufPanel.MouseMove += BufPanel_MouseMove;
            Cfg = TTestConfig.GetConfig();
            if (Cfg.Maximized)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
                Width = Cfg.WindowWidth;
                Height = Cfg.WindowHeight;
            }
            numUDLeft.Value = Cfg.CoeffLeft;
            numUDRight.Value = Cfg.CoeffRight;
            radioButton11.Checked = true;
            panelGraph.Dock = DockStyle.Fill;
            panelGraph.Controls.Add(BufPanel);
            BufPanel.Dock = DockStyle.Fill;
            BufPanel.Paint += bufferedPanel_Paint;
            VisirList = new List<int[]>();
            DataProcessing.CompressionChanged += OnCompressionChanged;
            InitArraysForFlow();
            USBPort = new USBserialPort(this, Decomposer.BaudRate);
            USBPort.ConnectionFailure += OnConnectionFailure;
            USBPort.ConnectionOk += OnConnectionOk;
            USBPort.Connect();
            GigaDevStatus = new GigaDeviceStatus();
        }

        private void OnCompressionChanged(object? sender, EventArgs e)
        {
            labCompressionRatio.Text = DataProcessing.CompressionRatio.ToString();
        }

        private void InitArraysForFlow()
        {
            DataA = new DataArrays(ByteDecomposer.DataArrSize);
            Decomposer = new ByteDecomposerBCI(DataA);
            Decomposer.OnDecomposePacketEvent += OnPacketReceived;
            Painter = new CurvesPainter(BufPanel, Decomposer);
        }

        private void OnConnectionOk()
        {
            CommandsBCI.BCISetup(USBPort);
        }

        private void OnConnectionFailure(Exception obj)
        {
            MessageBoxButtons but = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Error;
            MessageBox.Show("Connection failure", "Error", but, icon);
            ViewMode = true;
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_DEVICECHANGE = 0x0219;
            if (WindowsMessage != null)
            {
                if (m.Msg == WM_DEVICECHANGE)
                {
                    WindowsMessage(m);
                }
            }
            base.WndProc(ref m);
        }

        private void butSaveFile_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = Cfg.DataDir.ToString();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Cfg.DataDir = Path.GetDirectoryName(saveFileDialog1.FileName) + @"\";
                TTestConfig.SaveConfig(Cfg);
                CurrentFile = Path.GetFileName(saveFileDialog1.FileName);
                if (File.Exists(Cfg.DataDir + CurrentFile))
                {
                    File.Delete(Cfg.DataDir + CurrentFile);
                }
                File.Move(saveFileDialog1.InitialDirectory + TmpDataFile, Cfg.DataDir + CurrentFile);
                Text = "File : " + CurrentFile;
            }
        }

        private void butOpenFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.InitialDirectory = Cfg.DataDir.ToString();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ViewMode = true;
                if (File.Exists(openFileDialog1.FileName))
                {
                    Cfg.DataDir = Path.GetDirectoryName(openFileDialog1.FileName) + @"\";
                    TTestConfig.SaveConfig(Cfg);
                    timerRead.Enabled = false;

                    CurrentFile = Path.GetFileName(openFileDialog1.FileName);
                    ReadFile(Cfg.DataDir + CurrentFile);
                }
            }
            Text = "File : " + CurrentFile;
        }

        private void ReadFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            CurrentFileSize = lines.Length;
            labRecordSize.Text = "Record size : " + (CurrentFileSize / Decomposer.SamplingFrequency).ToString() + " s";
            UpdateScrollBar(CurrentFileSize);

            if (CurrentFileSize == 0)
            {
                MessageBox.Show("Error reading file " + fileName);
                return;
            }
            DataA = DataArrays.CreateDataFromLines(lines);
            if (DataA == null)
            {
                MessageBox.Show("Error reading file");
                return;
            }
            PrepareData();
            BufPanel.Refresh();
        }

        private void PrepareData()
        {
            DataA.CountViewArrays(BufPanel);

            //Детектор - обнаружение пульсовых волн по производной
            WaveDetector WD = new(Decomposer.SamplingFrequency);
            WD.Reset();
            for (int i = 0; i < CurrentFileSize; i++)
            {
                DataA.DebugArray[i] = WD.Detect(DataA.DerivArray, i);
            }

            var ArrayOfWaveIndexesDerivative = WD.FiltredPoints.ToArray(); //Используем только интервалы, прошедшие фильтр 25%
            if (ArrayOfWaveIndexesDerivative.Length == 0)
            {
                return;
            }

            int[] ArrayOfWaveIndexes = new int[ArrayOfWaveIndexesDerivative.Length];
            //Поиск максимумов пульсаций давления (в окрестностях максимума производной)
            for (int i = 0; i < ArrayOfWaveIndexes.Length; i++)
            {
                ArrayOfWaveIndexes[i] = DataProcessing.GetMaxIndexInRegion(DataA.PressureViewArray, ArrayOfWaveIndexesDerivative[i]);
            }
//            Array.Copy(ArrayOfWaveIndexesDerivative, ArrayOfWaveIndexes, ArrayOfWaveIndexes.Length);

            VisirList.Clear();
            VisirList.Add(ArrayOfWaveIndexes);

            //Поиск пульсовой волны с максимальной амплитудой
            double max = -1000000;
            int XMax = default;
            int XMaxIndex = 0;
            for (int i = 0; i < ArrayOfWaveIndexes.Length - 4; i++)
            {
                if (ArrayOfWaveIndexes[i] > DataA.Size)
                {
                    break;
                }
                if (DataA.PressureViewArray[ArrayOfWaveIndexes[i]] > max)
                {
                    max = DataA.PressureViewArray[ArrayOfWaveIndexes[i]];
                    XMax = ArrayOfWaveIndexes[i];
                    XMaxIndex = i;
                }
            }

            int[] ArrayLeft = new int[XMaxIndex];
            int[] ArrayRight = new int[ArrayOfWaveIndexes.Length - XMaxIndex];
            Array.Copy(ArrayOfWaveIndexes, ArrayLeft, ArrayLeft.Length);
            Array.Copy(ArrayOfWaveIndexes, XMaxIndex, ArrayRight, 0, ArrayRight.Length);
            double[] ArrayLeftValues = ArrayLeft.Select(x => DataA.PressureViewArray[x]).ToArray();
            double[] ArrayRightValues = ArrayRight.Select(x => DataA.PressureViewArray[x]).ToArray();
            double[] ArrLeftSorted = ArrayLeftValues.OrderBy(x => x).ToArray();
//            double[] ArrRightSorted = ArrayRightValues.OrderByDescending(x => x).ToArray();
            double[] ArrValues = ArrLeftSorted.Concat(ArrayRightValues).ToArray();

            //Построение огибающей максимумов пульсаций давления
            for (int i = 1; i < ArrayOfWaveIndexes.Length; i++)
            {
                int x1 = ArrayOfWaveIndexes[i - 1];
                int x2 = ArrayOfWaveIndexes[i];
                double y1 = ArrValues[i-1];
                double y2 = ArrValues[i];
                double coeff = (y2 - y1) / (x2 - x1);
                for (int j = x1 - 1; j < x2; j++)
                {
                    DataA.EnvelopeArray[i + j] = y1 + coeff * (j - x1);
                }
            }

            //Вычисление пульса 
            int DecreaseSize = 3; //Количество отбрасываемых интервалов справа и слева
            int TakeSize = ArrayOfWaveIndexes.Length - DecreaseSize * 2;
            int[] ArrayForPulse = ArrayOfWaveIndexes.Skip(DecreaseSize).Take(TakeSize).ToArray();

            labPulse.Text = "Pulse : " + DataProcessing.GetPulseFromIndexesArray(ArrayForPulse, Decomposer.SamplingFrequency).ToString();
            labNumOfWaves.Text = "Waves detected : " + (ArrayOfWaveIndexes.Length - 1).ToString();

            double P1 = 0;
            double P2 = 0;
            int indexP1 = 0;
            int indexP2 = 0;
            //Среднее давление
            int MeanPress = (int)DataA.DCArray[XMax];
            double V1 = max * (double)Cfg.CoeffLeft;
            double V2 = max * (double)Cfg.CoeffRight;

            //Определение систолического давления (влево от Max)
            for (int i = XMaxIndex; i > 0; i--)
            {
                if (ArrValues[i] < V1)
                {
                    int x1 = ArrayOfWaveIndexes[i];
                    int x2 = ArrayOfWaveIndexes[i + 1];
                    double y1 = ArrValues[i];
                    double y2 = ArrValues[i + 1];
                    indexP1 = (int)(x1 + (x2 - x1) * (V1 - y1) / (y2 - y1));
                    P1 = DataA.DCArray[indexP1];
                    break;
                }
            }
            //Определение диастолического давления (вправо от Max)
            for (int i = XMaxIndex; i < ArrayOfWaveIndexes.Length; i++)
            {
                if (ArrValues[i] < V2)
                {
                    int x2 = ArrayOfWaveIndexes[i];
                    int x1 = ArrayOfWaveIndexes[i - 1];
                    double y2 = ArrValues[i];
                    double y1 = ArrValues[i - 1];
                    indexP2 = (int)(x2 - (x1 - x2) * (V1 - y2) / (y1 - x2));
                    P2 = DataA.DCArray[indexP2];
                    break;
                }
            }
            int[] ArrayOfPoints = { indexP1, ArrayOfWaveIndexes[XMaxIndex], indexP2 };
            VisirList.Add(ArrayOfPoints);

            float[] envelopeArray = new float[ArrayOfWaveIndexes.Length];
            int[] envelopeMmhGArray = new int[ArrayOfWaveIndexes.Length];
            for (int i = 0; i < ArrayOfWaveIndexes.Length; i++)
            {
                if (ArrayOfWaveIndexes[i] > DataA.RealTimeArray.Length - 1)
                {
                    break;
                }
                envelopeArray[i] = (float)DataA.PressureViewArray[ArrayOfWaveIndexes[i]];
                envelopeMmhGArray[i] = DataProcessing.ValueToMmhG(DataA.RealTimeArray[ArrayOfWaveIndexes[i]]);
            }

            int UpsampleFactor = 10;
            int InterpolatedLength = envelopeArray.Length * UpsampleFactor;
            float[] xs = new float[InterpolatedLength];
            for (int i = 0; i < InterpolatedLength; i++)
            {
                xs[i] = (float)i * (envelopeArray.Length - 1) / (float)(InterpolatedLength - 1);
            }
            int[] xint = Enumerable.Range(0, envelopeArray.Length).ToArray();
            float[] x = new float[xint.Length];
            for (int i = 0; i < xint.Length; i++)
            {
                x[i] = (float)xint[i];
            }
            float[] ys = CubicSpline.Compute(x, envelopeArray, xs, 0.0f, Single.NaN, true);

            DataProcessing.SaveArray("env_spline.txt", ys);

            DataProcessing.SaveArray("env.txt", envelopeMmhGArray);
            labMeanPressure.Text = "Mean : " + DataProcessing.ValueToMmhG(MeanPress).ToString();
            labSys.Text = "Sys : " + DataProcessing.ValueToMmhG(P1).ToString();
            labDia.Text = "Dia : " + DataProcessing.ValueToMmhG(P2).ToString();
            FormHRV formHRV = new(ArrayOfWaveIndexes, Decomposer.SamplingFrequency);
            formHRV.ShowDialog();
            formHRV.Dispose();
        }

        private void bufferedPanel_Paint(object? sender, PaintEventArgs e)
        {
            if (DataA == null)
            {
                return;
            }
            var ArrayList = new List<double[]>();
            if (ViewMode)
            {
                if (radioButton11.Checked) //1:1
                {
                    ArrayList.Add(DataA.PressureViewArray);
                    ArrayList.Add(DataA.DerivArray);
                    ArrayList.Add(DataA.DebugArray);
                    ArrayList.Add(DataA.EnvelopeArray);
                }
                else //fit
                {
                    ArrayList.Add(DataA.CompressedArray);
                }
            }
            else
            {
                ArrayList.Add(DataA.PressureViewArray);
                ArrayList.Add(DataA.DerivArray);
//                ArrayList.Add(DataA.DCArray);
            }
            Painter.Paint(ViewMode, ViewShift, ArrayList, VisirList, ScaleY, MaxValue, e);
            ArrayList.Clear();
        }

        private void UpdateScrollBar(int size)
        {
            hScrollBar1.Maximum = size;
            hScrollBar1.LargeChange = panelGraph.Width - 50;
            hScrollBar1.SmallChange = panelGraph.Width / 10;
            hScrollBar1.AutoSize = true;
            hScrollBar1.Value = 0;
            hScrollBar1.Visible = hScrollBar1.Maximum > hScrollBar1.Width;
        }

        private void hScrollBar1_ValueChanged(object? sender, EventArgs e)
        {
            ViewShift = hScrollBar1.Value;
            BufPanel.Refresh();
        }

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            Cfg.Maximized = WindowState == FormWindowState.Maximized;
            Cfg.WindowWidth = Width;
            Cfg.WindowHeight = Height;
            TTestConfig.SaveConfig(Cfg);
        }

        private void radioButton11_CheckedChanged(object? sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                Compression = false;
                hScrollBar1.Visible = radioButton11.Checked;
                BufPanel.Refresh();
            }
        }

        private void radioButtonFit_CheckedChanged(object? sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                Compression = true;
                hScrollBar1.Visible = !radioButtonFit.Checked;
                hScrollBar1.Value = 0;
                MaxValue = 2 * (int)DataA.CompressedArray.Max();
                BufPanel.Refresh();
            }
        }

        private void Form1_Resize(object? sender, EventArgs e)
        {
            if (DataA == null)
            {
                return;
            }
            if (!ViewMode)
            {
                return;
            }
            DataA.CountViewArrays(BufPanel);
            BufPanel.Refresh();
        }

        private void BufPanel_MouseMove(object? sender, MouseEventArgs e)
        {
            if (!ViewMode)
            {
                return;
            }
            if (DataA == null)
            {
                return;
            }
            double x = e.X + ViewShift;
            if (x > DataA.Size - 1)
            {
                return;
            }
            int compressionMult = Compression ? DataProcessing.CompressionRatio : 1;

            int index = e.X * compressionMult + ViewShift;
            double sec = index / Decomposer.SamplingFrequency;
            labelX.Text = String.Format("X : {0}, Time {1:f2} s ", index, sec);
            labY0.Text = String.Format("PressureViewArray : {0:f0}", DataA.PressureViewArray[index]);
            labY1.Text = String.Format("DerivArray : {0:f0}", DataA.DerivArray[index]);
            labY2.Text = String.Format("DCArray : {0:f0}", DataA.DCArray[index]) + "  " +
                         DataProcessing.ValueToMmhG(DataA.DCArray[index]).ToString();
        }

        private void trackBarAmp_ValueChanged(object? sender, EventArgs e)
        {
            double a = trackBarAmp.Value;
            ScaleY = Math.Pow(2, a / 2);
            BufPanel.Refresh();
        }

        private void butStopRecord_Click(object sender, EventArgs e)
        {
            Detector.OnWaveDetected -= NewWaveDetected;
            Detector = null;
            progressBarRecord.Visible = false;
            Decomposer.OnDecomposePacketEvent -= OnPacketReceived;
            Decomposer.RecordStarted = false;
            TextWriter?.Dispose();
            ViewMode = true;
            timerPaint.Enabled = !ViewMode;
            timerRead.Enabled = false;

            CurrentFileSize = Decomposer.PacketCounter;
            labRecordSize.Text = "Record size : " + (CurrentFileSize / Decomposer.SamplingFrequency).ToString() + " s";
            UpdateScrollBar(CurrentFileSize);

            PrepareData();
            BufPanel.Refresh();
            controlPanel.Refresh();
            //            ReadFile(Cfg.DataDir + TmpDataFile);
        }

        private void butStartRecord_Click(object sender, EventArgs e)
        {
            TextWriter = new StreamWriter(Cfg.DataDir + TmpDataFile);
            Decomposer.PacketCounter = 0;
            Decomposer.MainIndex = 0;
            Decomposer.RecordStarted = true;
            progressBarRecord.Visible = true;
            labMeanPressure.Text = "Mean : ";
            labSys.Text = "Sys : ";
            labDia.Text = "Dia : ";
            labPulse.Text = "Pulse : ";
            Detector = new WaveDetector(Decomposer.SamplingFrequency);
            Detector.OnWaveDetected += NewWaveDetected;
            FileNum++;
        }

        int FileNum = 0;
        private void NewWaveDetected(object? sender, WaveDetectorEventArgs e)
        {
            string fileName = "PointsOnCompression" + FileNum.ToString() + ".txt";
            string text = e.WaveCount.ToString() + "  " + ((int)e.Value).ToString();
            labNumOfWaves.Text = text;
            File.AppendAllText(fileName, text + Environment.NewLine);
        }

        private void timerStatus_Tick(object sender, EventArgs e)
        {
            labValve1.Text = GigaDevStatus.Valve1IsClosed ? "Valve 1 : closed" : "Valve 1 : opened";
            labValve2.Text = GigaDevStatus.Valve2IsClosed ? "Valve 2 : closed" : "Valve 2 : opened";
            labPump.Text = GigaDevStatus.PumpIsOn ? "Pump : On" : "Pump : Off";
            butValve1Close.Enabled = !GigaDevStatus.Valve1IsClosed;
            butValve1Open.Enabled = GigaDevStatus.Valve1IsClosed;
            butValve2Close.Enabled = !GigaDevStatus.Valve2IsClosed;
            butValve2Open.Enabled = GigaDevStatus.Valve2IsClosed;
            butPumpOn.Enabled = !GigaDevStatus.PumpIsOn;
            butPumpOff.Enabled = GigaDevStatus.PumpIsOn;

            if (Decomposer is null)
            {
                return;
            }
            butStartRecord.Enabled = !ViewMode && !Decomposer.RecordStarted!;
            butStopRecord.Enabled = Decomposer.RecordStarted;
            butSaveFile.Enabled = ViewMode && Decomposer.PacketCounter != 0;
            butFlow.Text = ViewMode ? "Start stream" : "Stop stream";
            panelView.Enabled = ViewMode;
//            labDeviceIsOff.Visible = !decomposer.DeviceTurnedOn;
            if (USBPort == null)
            {
                labPort.Text = "Disconnected";
                ViewMode = true;
                return;
            }
            if (USBPort.PortHandle == null)
            {
                labPort.Text = "Disconnected";
                ViewMode = true;
                return;
            }
            if (USBPort.PortHandle.IsOpen)
            {
                labPort.Text = "Connected to " + USBPort.PortNames[USBPort.CurrentPort];
            }
            else
            {
                labPort.Text = "Disconnected";
            }
        }

        private void timerRead_Tick(object sender, EventArgs e)
        {
            if (USBPort?.PortHandle?.IsOpen == true)
            {
                Decomposer?.Decompos(USBPort, TextWriter);
            }
        }

        private void timerPaint_Tick(object sender, EventArgs e)
        {
            BufPanel.Refresh();
        }

        private void butFlow_Click(object sender, EventArgs e)
        {
            ViewMode = !ViewMode;
            timerRead.Enabled = !ViewMode;
            timerPaint.Enabled = !ViewMode;
            if (!ViewMode)
            {
                InitArraysForFlow();
                hScrollBar1.Visible = false;
            }
        }

        private void numUDLeft_ValueChanged(object sender, EventArgs e)
        {
            Cfg.CoeffLeft = numUDLeft.Value;
        }

        private void numUDRight_ValueChanged(object sender, EventArgs e)
        {
            Cfg.CoeffRight = numUDRight.Value;
        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            PrepareData();
            BufPanel.Refresh();
            controlPanel.Refresh();
        }

        private void OnPacketReceived(object? sender, EventArgs e)
        {
            uint currentIndex = (Decomposer.MainIndex - 1) & (ByteDecomposer.DataArrSize - 1);
            double CurrentPressure = DataA.RealTimeArray[currentIndex];
            MaxPressure = (int)Math.Max(CurrentPressure, MaxPressure);            
            DataA.DerivArray[currentIndex] = DataProcessing.GetDerivative(DataA.PressureViewArray, currentIndex);
            if (Decomposer.MainIndex > 0)
            {
                labCurrentPressure.Text = "Current : " + DataProcessing.ValueToMmhG(CurrentPressure).ToString() + " Max : " +
                    MaxPressure.ToString();
//                labCurrentPressure.Text = "Current : " + (DataA.RealTimeArray[Decomposer.MainIndex - 1]).ToString() + " Max : " + 
//                    MaxPressure.ToString();
            }
            if (Decomposer.RecordStarted)
            {
                labRecordSize.Text = "Record size : " + (Decomposer.PacketCounter / Decomposer.SamplingFrequency).ToString() + " c";
                Detector?.Detect(DataA.DerivArray, (int)Decomposer.MainIndex);
            }
            switch (PressureMeasStatus)
            {
                case (int)Enums.PressureMeasurementStatus.Calibration:
                    //Вызов метода калибровки
                    PressureMeasStatus = (int)Enums.PressureMeasurementStatus.Pumping;
                    break;
                case (int)Enums.PressureMeasurementStatus.Pumping:
                    //Вызов метода оценки пульсаций (см. алгоритм)
                    if (CurrentPressure > MaxPressure)
                    {
                        Decomposer.PacketCounter = 0;
                        Decomposer.MainIndex = 0;
                        USBPort.WriteByte((byte)CmdGigaDevice.Valve1PWMOn);
                        USBPort.WriteByte((byte)CmdGigaDevice.PumpSwitchOff);

                        PressureMeasStatus = (int)Enums.PressureMeasurementStatus.Measurement;
                    }
                    break;
                case (int)Enums.PressureMeasurementStatus.Measurement:
                    if (CurrentPressure < MinPressure)
                    {
                        PressureMeasStatus = (int)Enums.PressureMeasurementStatus.Ready;
                        PrepareData();
                    }
                    break;
                default:
                    break;
            }
        }

        private void butPressureMeasStart_Click(object sender, EventArgs e)
        {
            GigaDevStatus.Valve1IsClosed = true;
            GigaDevStatus.Valve2IsClosed = true;
            GigaDevStatus.PumpIsOn = true;
            USBPort.WriteByte((byte)CmdGigaDevice.Valve1Close);
            USBPort.WriteByte((byte)CmdGigaDevice.Valve2Close);
            USBPort.WriteByte((byte)CmdGigaDevice.PumpSwitchOn);
            PressureMeasStatus = (int)Enums.PressureMeasurementStatus.Calibration;
            labMeasInProgress.Visible = true;
        }

        private void butPressureMeasAbort_Click(object sender, EventArgs e)
        {
            GigaDevStatus.Valve1IsClosed = false;
            GigaDevStatus.Valve2IsClosed = false;
            GigaDevStatus.PumpIsOn = false;
            USBPort.WriteByte((byte)CmdGigaDevice.Valve1Open);
            USBPort.WriteByte((byte)CmdGigaDevice.Valve2Open);
            USBPort.WriteByte((byte)CmdGigaDevice.PumpSwitchOff);
            PressureMeasStatus = (int)Enums.PressureMeasurementStatus.Ready;
            labMeasInProgress.Visible = false;
        }

        private void butValve1Open_Click(object sender, EventArgs e)
        {
            GigaDevStatus.Valve1IsClosed = false;
            USBPort.WriteByte((byte)CmdGigaDevice.Valve1Open);
        }

        private void butValve1Close_Click(object sender, EventArgs e)
        {
            GigaDevStatus.Valve1IsClosed = true;
            USBPort.WriteByte((byte)CmdGigaDevice.Valve1Close);
        }

        private void butValve2Open_Click(object sender, EventArgs e)
        {
            GigaDevStatus.Valve2IsClosed = false;
            USBPort.WriteByte((byte)CmdGigaDevice.Valve2Open);
        }

        private void butValve2Close_Click(object sender, EventArgs e)
        {
            GigaDevStatus.Valve2IsClosed = true;
            USBPort.WriteByte((byte)CmdGigaDevice.Valve2Close);
        }

        private void butPumpOn_Click(object sender, EventArgs e)
        {
            GigaDevStatus.PumpIsOn = true;
            USBPort.WriteByte((byte)CmdGigaDevice.PumpSwitchOn);
            MaxPressure = 0;
        }

        private void butPumpOff_Click(object sender, EventArgs e)
        {
            GigaDevStatus.PumpIsOn = false;
            USBPort.WriteByte((byte)CmdGigaDevice.PumpSwitchOff);
        }

        private void butValve1PWM_Click(object sender, EventArgs e)
        {
            GigaDevStatus.Valve1PWM = true;
            USBPort.WriteByte((byte)CmdGigaDevice.Valve1PWMOn);
        }
    }
}