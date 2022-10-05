using TTestApp.Commands;
using TTestApp.Decomposers;
using TTestApp.Enums;

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
        Patient CurrentPatient;
        int CurrentFileSize;
        const string TmpDataFile = "tmpdata.t";
        int MaxValue = 200;   // Для ADS1115
        bool ViewMode = false;
        int ViewShift;
        double ScaleY = 1;
        List<int[]> VisirList;
        int PressureMeasStatus = (int)PressureMeasurementStatus.Ready;

        double MaxPressure = 0;
        double MinPressure = 300;

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
            panelGraph.Dock = DockStyle.Fill;
            panelGraph.Controls.Add(BufPanel);
            BufPanel.Dock = DockStyle.Fill;
            BufPanel.Paint += bufferedPanel_Paint;
            VisirList = new List<int[]>();
            InitArraysForFlow();
            USBPort = new USBserialPort(this, Decomposer.BaudRate);
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


        private void bufferedPanel_Paint(object? sender, PaintEventArgs e)
        {
            if (DataA == null)
            {
                return;
            }
            var ArrayList = new List<double[]>();
            if (ViewMode)
            {
                    ArrayList.Add(DataA.PressureViewArray);
//                    ArrayList.Add(DataA.DerivArray);
//                    ArrayList.Add(DataA.DebugArray);
//                    ArrayList.Add(DataA.EnvelopeArray);
            }
            else
            {
                ArrayList.Add(DataA.PressureViewArray);
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
        }

        int FileNum = 0;
        private void OnPacketReceived(object? sender, PacketEventArgs e)
        {
            uint currentIndex = (e.MainIndex - 1) & (ByteDecomposer.DataArrSize - 1);
            double CurrentPressure = e.RealTimeValue;
            MaxPressure = (int)Math.Max(CurrentPressure, MaxPressure);            
            DataA.DerivArray[currentIndex] = DataProcessing.GetDerivative(DataA.PressureViewArray, currentIndex);
            if (currentIndex > 0)
            {
                labCurrentPressure.Text = "Current pressure, mmHG: " +
                                           DataProcessing.ValueToMmhG(CurrentPressure).ToString();// +
                                           //" Deriv : " +
                                           //                                           MaxPressure.ToString();
                                           //DataA.DerivArray[currentIndex].ToString();
//                labCurrentPressure.Text = "Current : " + (DataA.RealTimeArray[Decomposer.MainIndex - 1]).ToString() + " Max : " + 
//                    MaxPressure.ToString();
            }
            if (Decomposer.RecordStarted)
            {
                if (PressureMeasStatus == (int)PressureMeasurementStatus.Calibration)
                {
                    Decomposer.ZeroLine = Decomposer.tmpZero;
                    PressureMeasStatus = (int)PressureMeasurementStatus.Ready;
                }
                labRecordSize.Text = "Record size : " + (e.PacketCounter / Decomposer.SamplingFrequency).ToString() + " c";
                DataA.DebugArray[currentIndex] = (int)Detector.Detect(DataA.DerivArray, (int)currentIndex);
            }
        }
    }
}