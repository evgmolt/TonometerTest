using TTestApp.Commands;
using TTestApp.Decomposers;
using TTestApp.Enums;

namespace TTestApp
{
    public partial class Form1 : Form, IMessageHandler
    {
        string ConnectStringFile = "connstr.txt";
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
//            string connectStr = File.ReadAllText(ConnectStringFile);
            USBPort = new USBserialPort(this, Decomposer.BaudRate);
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
//            PrepareData();
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
                envelopeMmhGArray[i] = DataProcessing.ValueToMmhG(DataA.RealTimeArray[ArrayOfWaveIndexes[i]]);
            }

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
            int compressionMult = Compression ? DataProcessing.CompressionRatio : 1;

            int index = e.X * compressionMult + ViewShift;
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

        private void button1_Click(object sender, EventArgs e)
        {
            USBPort.Connect();
        }
    }
}