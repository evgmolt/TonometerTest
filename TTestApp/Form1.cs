using HRV;
using TTestApp.Commands;

namespace TTestApp
{
    public partial class Form1 : Form, IMessageHandler
    {
        USBserialPort USBPort;
        DataArrays? DataA;
        ByteDecomposer Decomposer;
        CurvesPainter Painter;
        BufferedPanel BufPanel;
        TTestConfig Cfg;
        StreamWriter TextWriter;
        string CurrentFile;
        int CurrentFileSize;
        const string TmpDataFile = "tmpdata.t";
        int MaxValue = 1000000;
        bool ViewMode = false;
        int ViewShift;
        double ScaleY = 1;
        List<int[]> VisirList;
        bool Compression;
        int PressureMeasurementStatus = (int)PMStatus.Ready;

        int MaxPressure = 0;
        int MinPressure = 300;

        SF3Status sF3Status;

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
            DataProcessing.CompressionChanged += onCompressionChanged;
            InitArraysForFlow();
            USBPort = new USBserialPort(this, Decomposer.BaudRate);
            USBPort.ConnectionFailure += OnConnectionFailure;
            USBPort.ConnectionOk += OnConnectionOk;
            USBPort.Connect();
            sF3Status = new SF3Status();
        }

        private void onCompressionChanged(object? sender, EventArgs e)
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
//            DataA = DataProcessing.CutArray(DataA);
            DataA.CountViewArrays(BufPanel);

            //Детектор
            WaveDetector WD = new(Decomposer.SamplingFrequency);
            WD.Reset();
            for (int i = 0; i < DataA.DerivArray.Length; i++)
            {
                DataA.DebugArray[i] = WD.Detect(0, DataA.DerivArray, i);
            }

            var ArrayOfWaveIndexes = WD.FiltredPoints.ToArray();
            if (ArrayOfWaveIndexes.Length == 0)
            {
                return;
            }
            VisirList.Clear();
            VisirList.Add(ArrayOfWaveIndexes);

            double max = -1000000;
            int XMax = default;
            int XMaxIndex = 0;
            for (int i = 0; i < ArrayOfWaveIndexes.Length; i++)
            {
                if (ArrayOfWaveIndexes[i] > DataA.Size)
                {
                    break;
                }
                if (DataA.DerivArray[ArrayOfWaveIndexes[i]] > max)
                {
                    max = DataA.DerivArray[ArrayOfWaveIndexes[i]];
                    XMax = ArrayOfWaveIndexes[i];
                    XMaxIndex = i;
                }
            }

            int ArrayForPulseLen = 8;
            int skipSize = (XMaxIndex - ArrayForPulseLen / 2) > 0 ? XMaxIndex - ArrayForPulseLen / 2 : 0;
            int takeSize = (ArrayForPulseLen < ArrayOfWaveIndexes.Length - skipSize) ? ArrayForPulseLen : ArrayOfWaveIndexes.Length - skipSize;
            int[] ArrayForPulse = ArrayOfWaveIndexes.Skip(skipSize).Take(ArrayForPulseLen).ToArray();
            labPulse.Text = "Pulse : " + DataProcessing.GetPulseFromIndexesArray(ArrayForPulse, Decomposer.SamplingFrequency).ToString();
            labNumOfWaves.Text = "Waves detected : " + (ArrayOfWaveIndexes.Length - 1).ToString();

            double P1 = 0;
            double P2 = 0;
            int MeanPress = (int)DataA.DCArray[XMax];
            double V1 = max * (double)Cfg.CoeffLeft;
            double V2 = max * (double)Cfg.CoeffRight;
            for (int i = XMaxIndex; i > 0; i--)
            {
                if (DataA.DerivArray[ArrayOfWaveIndexes[i]] < V1)
                {
                    int x1 = ArrayOfWaveIndexes[i];
                    int x2 = ArrayOfWaveIndexes[i + 1];
                    double y1 = DataA.DerivArray[x1];
                    double y2 = DataA.DerivArray[x2];
                    double coeff = (V1 - y1) / (y2 - y1);
                    double yDC1 = DataA.DCArray[x1];
                    double yDC2 = DataA.DCArray[x2];
                    P1 = (int)(yDC1 + (yDC2 - yDC1) * coeff);
                    break;
                }
            }
            for (int i = XMaxIndex; i < ArrayOfWaveIndexes.Length; i++)
            {
                if (DataA.DerivArray[ArrayOfWaveIndexes[i]] < V2)
                {
                    int x1 = ArrayOfWaveIndexes[i];
                    int x2 = ArrayOfWaveIndexes[i - 1];
                    double y1 = DataA.DerivArray[x1];
                    double y2 = DataA.DerivArray[x2];
                    double coeff = (V2 - y1) / (y2 - y1);
                    double yDC1 = DataA.DCArray[x1];
                    double yDC2 = DataA.DCArray[x2];
                    P2 = (int)(yDC2 + (yDC1 - yDC2) * coeff);
                    break;
                }
            }

            int[] envelopeArray = new int[ArrayOfWaveIndexes.Length];
            int[] envelopeMmhGArray = new int[ArrayOfWaveIndexes.Length];
            for (int i = 0; i < ArrayOfWaveIndexes.Length; i++)
            {
                if (ArrayOfWaveIndexes[i] > DataA.RealTimeArray.Length - 1)
                {
                    break;
                }
                envelopeArray[i] = (int)DataA.RealTimeArray[ArrayOfWaveIndexes[i]];
                envelopeMmhGArray[i] = ValueToMmhG(DataA.RealTimeArray[ArrayOfWaveIndexes[i]]);
            }
            DataProcessing.SaveArray("env.txt", envelopeMmhGArray);
            labMeanPressure.Text = "Mean : " + ValueToMmhG(MeanPress).ToString();
            labSys.Text = "Sys : " + ValueToMmhG(P1).ToString();
            labDia.Text = "Dia : " + ValueToMmhG(P2).ToString();
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
                }
                else //fit
                {
                    ArrayList.Add(DataA.CompressedArray);
                }
            }
            else
            {
                ArrayList.Add(DataA.PressureViewArray);
//                ArrayList.Add(DataA.DerivArray);
//                ArrayList.Add(DataA.RealTimeArray);
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
            labY2.Text = String.Format("DCArray : {0:f0}", DataA.DCArray[index]);
        }

        private void trackBarAmp_ValueChanged(object? sender, EventArgs e)
        {
            double a = trackBarAmp.Value;
            ScaleY = Math.Pow(2, a / 2);
            BufPanel.Refresh();
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
        }

        private int ValueToMmhG(double value)
        {
            double zero = 465;
            double pressure = 142;
            double val = 2503287;
            return (int)((value - zero) * pressure / (val - zero));
        }

        private void timerStatus_Tick(object sender, EventArgs e)
        {
            labValve1.Text = sF3Status.Valve1IsClosed ? "Valve 1 : closed" : "Valve 1 : opened";
            labValve2.Text = sF3Status.Valve2IsClosed ? "Valve 2 : closed" : "Valve 2 : opened";
            labPump.Text = sF3Status.PumpIsOn ? "Pump : On" : "Pump : Off";
            butValve1Close.Enabled = !sF3Status.Valve1IsClosed;
            butValve1Open.Enabled = sF3Status.Valve1IsClosed;
            butValve2Close.Enabled = !sF3Status.Valve2IsClosed;
            butValve2Open.Enabled = sF3Status.Valve2IsClosed;
            butPumpOn.Enabled = !sF3Status.PumpIsOn;
            butPumpOff.Enabled = sF3Status.PumpIsOn;

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
                Decomposer?.Decompos(USBPort, null, TextWriter);
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

        private void butStopRecord_Click(object sender, EventArgs e)
        {
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
                labCurrentPressure.Text = "Current : " + ValueToMmhG(CurrentPressure).ToString() + " Max : " +
                    MaxPressure.ToString(); ;
//                labCurrentPressure.Text = "Current : " + (DataA.RealTimeArray[Decomposer.MainIndex - 1]).ToString() + " Max : " + 
//                    MaxPressure.ToString();
            }
            if (Decomposer.RecordStarted)
            {
                labRecordSize.Text = "Record size : " + (Decomposer.PacketCounter / Decomposer.SamplingFrequency).ToString() + " c";
            }
            switch (PressureMeasurementStatus)
            {
                case (int)PMStatus.Calibration:
                    //Вызов метода калибровки
                    PressureMeasurementStatus = (int)PMStatus.Pumping;
                    break;
                case (int)PMStatus.Pumping:
                    //Вызов метода оценки пульсаций (см. алгоритм)
                    if (CurrentPressure > MaxPressure)
                    {
                        Decomposer.PacketCounter = 0;
                        Decomposer.MainIndex = 0;
                        USBPort.WriteByte((byte)CmdSF3.Valve1PWMOn);
                        USBPort.WriteByte((byte)CmdSF3.PumpSwitchOff);

                        PressureMeasurementStatus = (int)PMStatus.Measurement;
                    }
                    break;
                case (int)PMStatus.Measurement:
                    if (CurrentPressure < MinPressure)
                    {
                        PressureMeasurementStatus = (int)PMStatus.Ready;
                        PrepareData();
                    }
                    break;
                default:
                    break;
            }
        }

        private void butPressureMeasStart_Click(object sender, EventArgs e)
        {
            sF3Status.Valve1IsClosed = true;
            sF3Status.Valve2IsClosed = true;
            sF3Status.PumpIsOn = true;
            USBPort.WriteByte((byte)CmdSF3.Valve1Close);
            USBPort.WriteByte((byte)CmdSF3.Valve2Close);
            USBPort.WriteByte((byte)CmdSF3.PumpSwitchOn);
            PressureMeasurementStatus = (int)PMStatus.Calibration;
            labMeasInProgress.Visible = true;
        }

        private void butPressureMeasAbort_Click(object sender, EventArgs e)
        {
            sF3Status.Valve1IsClosed = false;
            sF3Status.Valve2IsClosed = false;
            sF3Status.PumpIsOn = false;
            USBPort.WriteByte((byte)CmdSF3.Valve1Open);
            USBPort.WriteByte((byte)CmdSF3.Valve2Open);
            USBPort.WriteByte((byte)CmdSF3.PumpSwitchOff);
            PressureMeasurementStatus = (int)PMStatus.Ready;
            labMeasInProgress.Visible = false;
        }

        private void butValve1Open_Click(object sender, EventArgs e)
        {
            sF3Status.Valve1IsClosed = false;
            USBPort.WriteByte((byte)CmdSF3.Valve1Open);
        }

        private void butValve1Close_Click(object sender, EventArgs e)
        {
            sF3Status.Valve1IsClosed = true;
            USBPort.WriteByte((byte)CmdSF3.Valve1Close);
        }

        private void butValve2Open_Click(object sender, EventArgs e)
        {
            sF3Status.Valve2IsClosed = false;
            USBPort.WriteByte((byte)CmdSF3.Valve2Open);
        }

        private void butValve2Close_Click(object sender, EventArgs e)
        {
            sF3Status.Valve2IsClosed = true;
            USBPort.WriteByte((byte)CmdSF3.Valve2Close);
        }

        private void butPumpOn_Click(object sender, EventArgs e)
        {
            sF3Status.PumpIsOn = true;
            USBPort.WriteByte((byte)CmdSF3.PumpSwitchOn);
        }

        private void butPumpOff_Click(object sender, EventArgs e)
        {
            sF3Status.PumpIsOn = false;
            USBPort.WriteByte((byte)CmdSF3.PumpSwitchOff);
        }

        private void butValve1PWM_Click(object sender, EventArgs e)
        {
            sF3Status.Valve1PWM = true;
            USBPort.WriteByte((byte)CmdSF3.Valve1PWMOn);
        }

        private void panelBottom_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}