using TTestApp.Decomposers;
using TTestApp.Enums;

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
        const string TmpDataFile = "tmpdata.txt";
        int MaxValue = 200;   // Для ADS1115
        bool ViewMode = false;
        int ViewShift;
        double ScaleY = 1;
        List<int[]> VisirList;
        PressureMeasurementStatus PressureMeasStatus = PressureMeasurementStatus.Ready;

        double MaxPressure = 0;
        double MinPressure = 300;

        double MaxDerivValue;

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
            panelGraph.Dock = DockStyle.Fill;
            panelGraph.Controls.Add(BufPanel);
            BufPanel.Dock = DockStyle.Fill;
            BufPanel.Paint += bufferedPanel_Paint;
            VisirList = new List<int[]>();
            InitArraysForFlow();
            USBPort = new USBserialPort(this, Decomposer.BaudRate);
            USBPort.ConnectionOk += OnConnectionOk;
            USBPort.ConnectionFailure += OnConnectionFailure;
            USBPort.Connect();
        }


        private void InitArraysForFlow()
        {
            DataA = new DataArrays(ByteDecomposer.DataArrSize);
            Decomposer = new ByteDecomposer(DataA);
            Decomposer.OnDecomposePacketEvent += OnPacketReceived;
            Painter = new CurvesPainter(BufPanel, Decomposer);
        }

        private void OnConnectionFailure(Exception obj)
        {
            if (Decomposer?.RecordStarted == true)
            {
                butStopRecord_Click(this, EventArgs.Empty);
            }
            butFlow.Enabled = false;
        }

        private void OnConnectionOk()
        {
            butFlow.Enabled = true;
            //            butFlow_Click(this, EventArgs.Empty);
            //            butStartRecord_Click(this, EventArgs.Empty);
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

        private void timerStatus_Tick(object sender, EventArgs e)
        {
            if (Decomposer is null)
            {
                return;
            }
            butStartRecord.Enabled = !ViewMode && !Decomposer.RecordStarted!;
            butStopRecord.Enabled = Decomposer.RecordStarted;
            butFlow.Text = ViewMode ? "Start stream" : "Stop stream";
//            butFlow.Enabled = !Decomposer.RecordStarted;

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


        private void butStopRecord_Click(object sender, EventArgs e)
        {
            progressBarRecord.Visible = false;
            Decomposer.OnDecomposePacketEvent -= OnPacketReceived;
            Decomposer.RecordStarted = false;
            TextWriter?.Dispose();
            ViewMode = true;
            timerPaint.Enabled = !ViewMode;
            timerRead.Enabled = false;
            PressureMeasStatus = (int)PressureMeasurementStatus.Ready;
            CurrentFileSize = Decomposer.PacketCounter;
            labRecordSize.Text = "Record size : " + (CurrentFileSize / Decomposer.SamplingFrequency).ToString() + " s";
            UpdateScrollBar(CurrentFileSize);
            SaveFile();
            BufPanel.Refresh();
        }

        private void butStartRecord_Click(object sender, EventArgs e)
        {
            TextWriter = new StreamWriter(Cfg.DataDir + TmpDataFile);
            Decomposer.PacketCounter = 0;
            Decomposer.MainIndex = 0;
            progressBarRecord.Visible = true;
            FileNum++;
            PressureMeasStatus = PressureMeasurementStatus.Calibration;
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
        private void hScrollBar1_ValueChanged(object? sender, EventArgs e)
        {
            ViewShift = hScrollBar1.Value;
            BufPanel.Refresh();
        }
        private void trackBarAmp_ValueChanged(object? sender, EventArgs e)
        {
            double a = trackBarAmp.Value;
            ScaleY = Math.Pow(2, a / 2);
            BufPanel.Refresh();
        }
        private void SaveFile()
        {
            Cfg.DataFileNum++;
            TTestConfig.SaveConfig(Cfg);
            CurrentFile = Cfg.Prefix + Cfg.DataFileNum.ToString().PadLeft(5, '0') + ".txt";
            if (File.Exists(Cfg.DataDir + CurrentFile))
            {
                File.Delete(Cfg.DataDir + CurrentFile);
            }
            var DataStrings = File.ReadAllLines(Cfg.DataDir + TmpDataFile);
            File.AppendAllLines(Cfg.DataDir + CurrentFile, DataStrings);
            Text = "Pulse wave recorder. File : " + CurrentFile;
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
                    ArrayList.Add(DataA.RealTimeArray);
//                    ArrayList.Add(DataA.DerivArray);
//                    ArrayList.Add(DataA.DebugArray);
//                    ArrayList.Add(DataA.EnvelopeArray);
            }
            else
            {
                ArrayList.Add(DataA.RealTimeArray);
//                ArrayList.Add(DataA.DerivArray);
//                ArrayList.Add(DataA.DebugArray);
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
        }

        int FileNum = 0;
        private void OnPacketReceived(object? sender, PacketEventArgs e)
        {
            uint currentIndex = (e.MainIndex - 1) & (ByteDecomposer.DataArrSize - 1);
            double CurrentPressure = e.PressureViewValue;
            if (currentIndex > 0)
            {
                labCurrentPressure.Text = "Current pressure, mmHG: " +
                                           DataProcessing.ValueToMmhG(CurrentPressure).ToString() + " " +
                                           CurrentPressure.ToString();
            }
            if (PressureMeasStatus == PressureMeasurementStatus.Calibration)
            {
//                Decomposer.ZeroLine = Decomposer.tmpZero;
                Decomposer.RecordStarted = true;
                PressureMeasStatus = PressureMeasurementStatus.Measurement;
            }
            if (Decomposer.RecordStarted)
            {
                labRecordSize.Text = "Record size : " + (e.PacketCounter / Decomposer.SamplingFrequency).ToString() + " c";
            }
        }

        private void butCalibration_Click(object sender, EventArgs e)
        {
            PressureMeasStatus = PressureMeasurementStatus.Calibration;
        }
    }
}