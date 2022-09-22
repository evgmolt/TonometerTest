using HRV;
using TTestApp.Commands;
using TTestApp.Decomposers;
using TTestApp.Enums;
using TTestApp.Spline;

namespace TTestApp
{
    public partial class Form1 : Form, IMessageHandler
    {
        USBSerialPort USBPort;
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
//        int MaxValue = 200000; // Для BCI
        int MaxValue = 200;   // Для ADS1115
        bool ViewMode = false;
        int ViewShift;
        double ScaleY = 1;
        List<int[]> VisirList;
        bool Compression;
        int PressureMeasStatus = (int)PressureMeasurementStatus.Ready;
        int PumpStatus = (int)PumpingStatus.Ready;
        double MaxFoundMoment;
        double MaxTimeAfterMaxFound = 2.5; //sec

        double MaxPressure = 0;

        double MaxDerivValue;

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
            InitArraysForFlow();
            USBPort = new USBSerialPort(this, Decomposer.BaudRate);
            USBPort.ConnectionFailure += OnConnectionFailure;
            USBPort.ConnectionOk += OnConnectionOk;
            USBPort.Connect();
            GigaDevStatus = new GigaDeviceStatus();
        }

        private void InitArraysForFlow()
        {
            DataA = new DataArrays(ByteDecomposer.DataArrSize);
            Decomposer = new ByteDecomposerADS1115(DataA);
            Decomposer.OnDecomposePacketEvent += OnPacketReceived;
            Painter = new CurvesPainter(BufPanel, Decomposer);
        }

        private void OnConnectionOk()
        {
            if (Decomposer is ByteDecomposerBCI)
            {
                CommandsBCI.BCISetup(USBPort);
            }
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

            labArrythmia.Text = WD.Arrythmia.ToString();
            var ArrayOfWaveIndexesDerivative = WD.FiltredPoints.ToArray(); //Используем только интервалы, прошедшие фильтр 25%
            if (ArrayOfWaveIndexesDerivative.Length == 0)
            {
                return;
            }

            int[] ArrayOfWaveIndexes = new int[ArrayOfWaveIndexesDerivative.Length];
            //Поиск максимумов пульсаций давления (в окрестностях максимума производной)
            for (int i = 0; i < ArrayOfWaveIndexes.Length - 1; i++)
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
            for (int i = XMaxIndex; i < ArrayOfWaveIndexes.Length - 1; i++)
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
                envelopeMmhGArray[i] = DataProcessing.ValueToMmHg(DataA.RealTimeArray[ArrayOfWaveIndexes[i]]);
            }

/*            int UpsampleFactor = 10;
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

            DataProcessing.SaveArray("env_spline.txt", ys);*/

            DataProcessing.SaveArray("env.txt", envelopeMmhGArray);
            labMeanPressure.Text = "Mean : " + DataProcessing.ValueToMmHg(MeanPress).ToString();
            labSys.Text = "Sys : " + DataProcessing.ValueToMmHg(P1).ToString();
            labDia.Text = "Dia : " + DataProcessing.ValueToMmHg(P2).ToString();
            //FormHRV formHRV = new(ArrayOfWaveIndexes, Decomposer.SamplingFrequency);
            //formHRV.ShowDialog();
            //formHRV.Dispose();
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
//                    ArrayList.Add(DataA.DiffArray);
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
//            ArrayList.Clear();
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

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            Cfg.Maximized = WindowState == FormWindowState.Maximized;
            Cfg.WindowWidth = Width;
            Cfg.WindowHeight = Height;
            TTestConfig.SaveConfig(Cfg);
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

            int index = e.X + ViewShift;
            double sec = index / Decomposer.SamplingFrequency;
            labelX.Text = String.Format("X : {0}, Time {1:f2} s ", index, sec);
            labY0.Text = String.Format("PressureViewArray : {0:f0}", DataA.PressureViewArray[index]);
            labY1.Text = String.Format("DerivArray : {0:f0}", DataA.DerivArray[index]);
            labY2.Text = String.Format("DCArray : {0:f0}", DataA.DCArray[index]) + "  " +
                         DataProcessing.ValueToMmHg(DataA.DCArray[index]).ToString();
        }

        int FileNum = 0;
        private void NewWaveDetected(object? sender, WaveDetectorEventArgs e)
        {
            double StopMeasCoeff = 0.5;

            string fileName = "PointsOnCompression" + FileNum.ToString() + ".txt";
            string text = e.WaveCount.ToString() + " " + e.Interval.ToString() + " " + e.Amplitude.ToString("0.0");
            labNumOfWaves.Text = "Waves detected: " + text;

            switch (PressureMeasStatus)
            {
                case (int)PressureMeasurementStatus.Pumping:
                    switch (PumpStatus)
                    {
                        case (int)PumpingStatus.SearchLevelWaiting:
                            if (e.Amplitude > Decomposer.StartSearchMaxLevel)
                            {
                                PumpStatus = (int)PumpingStatus.MaximumSearch;
                                File.AppendAllText(fileName, "Search\t\t" + text + Environment.NewLine);
                            }
                            else
                            {
                                File.AppendAllText(fileName, "Ready\t\t" + text + Environment.NewLine);
                            }
                            break;
                        case (int)PumpingStatus.MaximumSearch:
                            MaxDerivValue = Math.Max(MaxDerivValue, e.Amplitude);
                            if (MaxDerivValue > e.Amplitude)
                            {
                                File.AppendAllText(fileName, "Maximum found\t\t" + text + Environment.NewLine);
                                PumpStatus = (int)PumpingStatus.MaximumFound;
                                MaxFoundMoment = (int)Decomposer.MainIndex;
                            }
                            else
                            {
                                File.AppendAllText(fileName, "Search\t\t" + text + Environment.NewLine);
                            }
                            break;
                        case (int)PumpingStatus.MaximumFound:
                            int Index = (int)Decomposer.MainIndex;
                            bool timeout = (Index - MaxFoundMoment) / Decomposer.SamplingFrequency > MaxTimeAfterMaxFound;
                            if (timeout)
                            {
                                label5.Text = "Timeout";
                            }
                            if (/*e.Amplitude < Decomposer.StopPumpingLevel ||*/ timeout)
                            {
                                File.AppendAllText(fileName, "Stop pumping\t\t" + text + Environment.NewLine);
                                PumpStatus = (int)PumpingStatus.Ready;
                                Decomposer.PacketCounter = 0;
                                Decomposer.MainIndex = 0;
                                MaxDerivValue = 0;
                                Detector?.Reset();
                                //USBPort.WriteByte((byte)CmdGigaDevice.Valve1PWMOn);
                                //USBPort.WriteByte((byte)CmdGigaDevice.PumpSwitchOff);
                                PressureMeasStatus = (int)PressureMeasurementStatus.Measurement;
                                //Останавливаем накачку
                            }
                            else
                            {
                                File.AppendAllText(fileName, "Maximum found\t\t" + text + Environment.NewLine);
                            }
                            break;
                    }
                    break;
                case (int)PressureMeasurementStatus.Measurement:
                    MaxDerivValue = Math.Max(e.Amplitude, MaxDerivValue);

                    if (e.Amplitude < MaxDerivValue * StopMeasCoeff)
                    {
                        PressureMeasStatus = (int)PressureMeasurementStatus.Ready;
                        butStopRecord_Click(this, EventArgs.Empty);
                    }
                    break;
                default:
                    break;
            }
        }
        private void OnPacketReceived(object? sender, PacketEventArgs e)
        {
            labPumpStatus.Text = "Pumping status : " + PumpStatus switch
            {
                (int)PumpingStatus.Ready         => "Ready",
                (int)PumpingStatus.SearchLevelWaiting => "SearchLevel waiting",
                (int)PumpingStatus.MaximumSearch => "Maximum search",
                (int)PumpingStatus.MaximumFound  => "Maximum found",
                _ => "Ready",
            };

            labMeasStatus.Text = "Measurement status : " + PressureMeasStatus switch
            {
                (int)PressureMeasurementStatus.Ready       => "Ready",
                (int)PressureMeasurementStatus.Calibration => "Calibration",
                (int)PressureMeasurementStatus.Pumping     => "Pumping",
                (int)PressureMeasurementStatus.Measurement => "Measurement",
                _ => "Ready",
            };

            uint currentIndex = (e.MainIndex - 1) & (ByteDecomposer.DataArrSize - 1);
            double CurrentPressure = e.RealTimeValue;
            DataA.DerivArray[currentIndex] = DataProcessing.GetDerivative(DataA.PressureViewArray, currentIndex);
            if (Decomposer.RecordStarted)
            {
                MaxPressure = (int)Math.Max(DataA.DerivArray[currentIndex], MaxPressure);
            }
            if (currentIndex > 0)
            {
                labCurrentPressure.Text = "Current : " + 
                                           DataProcessing.ValueToMmHg(CurrentPressure).ToString() +
                                           " Deriv : " +
                                           DataA.DerivArray[currentIndex].ToString("0.0").PadLeft(6, ' ') + " " +
                    MaxPressure.ToString();
            }
            if (Decomposer.RecordStarted)
            {
                if (PressureMeasStatus == (int)PressureMeasurementStatus.Calibration)
                {
                    Decomposer.ZeroLine = Decomposer.tmpZero;
                    PressureMeasStatus = (int)PressureMeasurementStatus.Pumping;
                    PumpStatus = (int)PumpingStatus.SearchLevelWaiting;
                }
                labRecordSize.Text = "Record size : " + (e.PacketCounter / Decomposer.SamplingFrequency).ToString() + " c";
                DataA.DebugArray[currentIndex] = (int)Detector.Detect(DataA.DerivArray, (int)currentIndex);
            }
        }
    }
}