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
        PressureMeasurementStatus PressureMeasStatus = PressureMeasurementStatus.Ready;
        PumpingStatus PumpStatus = PumpingStatus.Ready;
        double CurrentPressure;
        double MaxAllowablePressure = 180;
        double MinPressure = 120;
        double MomentMaxFound;
        double MaxTimeAfterMaxFound = 4; //sec
        double MaxTimeAfterStartPumping = 15; //sec
        int DelayCounter;
        int DelayValue;
        const double DelayInSeconds = 0.3; //sec задержка начала записи сигнала после выключения компрессора
        int HeartVisibleDelay = 50;
        int HeartVisibleCounter;
        bool HRVmode = false;

        double MaxPressure = 0;

        double MaxDerivValue;

        DeviceStatus DevStatus;

        public event Action<Message> WindowsMessageHandler;

        public Form1()
        {
            InitializeComponent();
            labHeart.Text = "♥";
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
            numUDpressure.Value = Cfg.ToPressure;
            panelGraph.Dock = DockStyle.Fill;
            panelGraph.Controls.Add(BufPanel);
            BufPanel.Dock = DockStyle.Fill;
            BufPanel.Paint += bufferedPanel_Paint;
            VisirList = new List<int[]>();
            InitArraysForFlow();
            string ConnectionString = String.Empty;
            try
            {
                ConnectionString = File.ReadAllText("conectstr.txt");
            }
            catch (Exception)
            {
                MessageBox.Show("Connection string not found. The device cannot be connected");
                return;
            }
            USBPort = new USBSerialPort(this, Decomposer.BaudRate, ConnectionString);
            USBPort.ConnectionFailure += OnConnectionFailure;
            USBPort.ConnectionOk += OnConnectionOk;
            USBPort.Connect();
            DevStatus = new DeviceStatus();
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
            if (Decomposer is ByteDecomposerADS1115)
            {
                USBPort.WriteByte((byte)CmdDevice.StartReading);
            }
        }

        private void OnConnectionFailure(Exception obj)
        {
            ShowError(BPMError.Connection);
            ViewMode = true;
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_DEVICECHANGE = 0x0219;
            if (m.Msg == WM_DEVICECHANGE)
            {
                WindowsMessageHandler?.Invoke(m);
            }
            base.WndProc(ref m);
        }

        private void ReadFile(string fileName)
        {
            int PatientDataCount = 7;

            string[] lines = File.ReadAllLines(fileName);
            lines = lines.Skip(PatientDataCount).ToArray();
            CurrentFileSize = lines.Length;
            labRecordSize.Text = "Record size : " + (CurrentFileSize / Decomposer.SamplingFrequency).ToString() + " s";
            UpdateScrollBar(CurrentFileSize);

            if (CurrentFileSize == 0)
            {
                ShowError(BPMError.ReadingFile);
                return;
            }
            DataA = DataArrays.CreateDataFromLines(lines);
            if (DataA == null)
            {
                ShowError(BPMError.ReadingFile);
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

            labArrythmia.Text = WD.Arrhythmia.ToString();
            var ArrayOfWaveIndexesDerivative = WD.FiltredPoints.ToArray(); //Используем только интервалы, прошедшие фильтр 25%
            if (ArrayOfWaveIndexesDerivative.Length == 0)
            {
                return;
            }

            int[] ArrayOfWaveIndexes = new int[ArrayOfWaveIndexesDerivative.Length];
            //Поиск максимумов пульсаций давления (в окрестностях максимума производной)
            for (int i = 0; i < ArrayOfWaveIndexes.Length; i++)
            {
//                ArrayOfWaveIndexes[i] = ArrayOfWaveIndexesDerivative[i] + 9;
                ArrayOfWaveIndexes[i] = DataProcessing.GetMaxIndexInRegion(DataA.PressureViewArray, ArrayOfWaveIndexesDerivative[i]);
            }

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
                    int ind = i + j;
                    if (ind >= DataA.Size)
                    {
                        break;
                    }
                    DataA.EnvelopeArray[i + j] = y1 + coeff * (j - x1);
                }
            }

            //Вычисление пульса 
            //int DecreaseSize = 2; //Количество отбрасываемых интервалов справа и слева
            //int TakeSize = ArrayOfWaveIndexes.Length - DecreaseSize * 2;
            //int[] ArrayForPulse = ArrayOfWaveIndexes.Skip(DecreaseSize).Take(TakeSize).ToArray();

            int ArrayForPulseSize = 10;
            int shift = 6;
            int[] ArrayForPulse = new int[ArrayForPulseSize];// ArrayOfWaveIndexes.Skip(DecreaseSize).Take(TakeSize).ToArray();
            for (int i = 0; i < ArrayForPulseSize; i++)
            {
                ArrayForPulse[i] = ArrayOfWaveIndexes[XMaxIndex - shift + i];
            }


            labPulse.Text = "Pulse : " + DataProcessing.GetPulseFromIndexesArray(ArrayForPulse, Decomposer.SamplingFrequency).ToString();
            labNumOfWaves.Text = "Waves detected : " + (ArrayOfWaveIndexes.Length - 1).ToString();

            if (HRVmode)
            {
                FormHRV formHRV = new(ArrayOfWaveIndexes, Decomposer.SamplingFrequency);
                formHRV.ShowDialog();
                formHRV.Dispose();
                return;
            }

            double P1 = 0;
            double P2 = 0;
            int indexP1 = 0;
            int indexP2 = 0;
            //Среднее давление
            int MeanPress = (int)DataA.DCArray[XMax];
            double V1 = max * (double)Cfg.CoeffLeft;
            double V2 = max * (double)Cfg.CoeffRight;

            //Определение систолического давления (влево от Max)
            for (int i = XMaxIndex; i >= 0; i--)
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
            if (P1 == 0)
            {
                P1 = DataA.DCArray[ArrayOfWaveIndexes[0]];
//                ShowError(BPMError.Sys);
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

            if (P2 == 0)
            {
                P2 = DataA.DCArray[ArrayOfWaveIndexes[ArrayOfWaveIndexes.Length - 1]];
//                ShowError(BPMError.Dia);
            }
            int[] ArrayOfPoints = { indexP1, ArrayOfWaveIndexes[XMaxIndex], indexP2 };
            VisirList.Add(ArrayOfPoints);

            labMeanPressure.Text = "Mean : " + DataProcessing.ValueToMmHg(MeanPress).ToString();
            labSys.Text = "Sys : " + DataProcessing.ValueToMmHg(P1).ToString();
            labDia.Text = "Dia : " + DataProcessing.ValueToMmHg(P2).ToString();
        }

        private void bufferedPanel_Paint(object sender, PaintEventArgs e)
        {
            if (DataA == null)
            {
                return;
            }
            var ArrayList = new List<double[]>();
            if (ViewMode)
            {
                ArrayList.Add(DataA.PressureViewArray);
                ArrayList.Add(DataA.DerivArray);
                ArrayList.Add(DataA.DebugArray);
                ArrayList.Add(DataA.EnvelopeArray);
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
            butValvesOpen_Click(this, EventArgs.Empty);
            Cfg.Maximized = WindowState == FormWindowState.Maximized;
            Cfg.WindowWidth = Width;
            Cfg.WindowHeight = Height;
            TTestConfig.SaveConfig(Cfg);
        }

        private void Form1_Resize(object sender, EventArgs e)
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

        private void BufPanel_MouseMove(object sender, MouseEventArgs e)
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
            labY1.Text = String.Format("RealTimeArray : {0:f0}", DataA.RealTimeArray[index]);
            labY2.Text = String.Format("DCArray : {0:f0}", DataA.DCArray[index]) + "  " +
                                        DataProcessing.ValueToMmHg(DataA.DCArray[index]).ToString();
        }

        private void NewWaveDetected(object sender, WaveDetectorEventArgs e)
        {
            double StopMeasCoeff = 0.5;
            HeartVisibleCounter = HeartVisibleDelay;
            string text = e.WaveCount.ToString() + " " + e.Interval.ToString() + " " + e.Amplitude.ToString("0.0");
            labNumOfWaves.Text = "Waves detected: " + text;

            switch (PressureMeasStatus)
            {
                case PressureMeasurementStatus.Pumping:
                    switch (PumpStatus)
                    {
                        case PumpingStatus.WaitingForLevel:
                            if (e.Amplitude > Decomposer.StartSearchMaxLevel)
                            {
                                PumpStatus = PumpingStatus.MaximumSearch;
                            }
                            break;
                        case PumpingStatus.MaximumSearch:
                            MaxDerivValue = Math.Max(MaxDerivValue, e.Amplitude);
                            if (MaxDerivValue > e.Amplitude)
                            {
                                PumpStatus = PumpingStatus.MaximumFound;
                                MomentMaxFound = (int)Decomposer.MainIndex;
                            }
                            break;
                        case PumpingStatus.MaximumFound:
                            if (e.Amplitude < Decomposer.StopPumpingLevel && CurrentPressure > MinPressure)
                            {
                                StopPumping("Stop pumping level reached");
                            }
                            break;
                    }
                    break;
                case PressureMeasurementStatus.Measurement:
                    MaxDerivValue = Math.Max(e.Amplitude, MaxDerivValue);
                    if (e.Amplitude < MaxDerivValue * StopMeasCoeff)
                    {
                        DevStatus.ValveSlowClosed = false; 
                        DevStatus.ValveFastClosed = false;
                        PressureMeasStatus = PressureMeasurementStatus.Ready;
                        USBPort.WriteByte((byte)CmdDevice.ValveFastOpen);
                        USBPort.WriteByte((byte)CmdDevice.StopReading);
                        butStopRecord_Click(this, EventArgs.Empty);
                    }
                    break;
                default:
                    break;
            }
        }

        private void OnPacketReceived(object sender, PacketEventArgs e)
        {
            labZero.Text = "Zero : " + Decomposer.ZeroLine.ToString();
            labHeart.Visible = HeartVisibleCounter != 0;
            if (HeartVisibleCounter != 0)
            {
                HeartVisibleCounter--;
            }
            labPumpStatus.Text = "Pumping status : " + PumpStatus switch
            {
                PumpingStatus.Ready           => "Ready",
                PumpingStatus.WaitingForLevel => "Waiting for level",
                PumpingStatus.MaximumSearch   => "Maximum search",
                PumpingStatus.MaximumFound    => "Maximum found",
                _ => "Ready",
            };

            labMeasStatus.Text = "Measurement status : " + PressureMeasStatus switch
            {
                PressureMeasurementStatus.Ready          => "Ready",
                PressureMeasurementStatus.Calibration    => "Calibration",
                PressureMeasurementStatus.Pumping        => "Pumping",
                PressureMeasurementStatus.Delay          => "Delay",
                PressureMeasurementStatus.Measurement    => "Measurement",
                PressureMeasurementStatus.PumpingToLevel => "PumpingToLevel",
                _ => "Ready",
            };
            uint currentIndex = (e.MainIndex - 1) & (ByteDecomposer.DataArrSize - 1);
            CurrentPressure = DataProcessing.ValueToMmHg(e.RealTimeValue);

            if (PressureMeasStatus == PressureMeasurementStatus.PumpingToLevel)
            {
                if (CurrentPressure >= (int)Cfg.ToPressure)
                {
                    PressureMeasStatus = PressureMeasurementStatus.Ready;
                    butPumpOff_Click(this, EventArgs.Empty);
                }
            }

            DataA.DerivArray[currentIndex] = DataProcessing.GetDerivative(DataA.PressureViewArray, currentIndex);
            MaxPressure = (int)Math.Max(DataA.RealTimeArray[currentIndex], MaxPressure);
            labCurrentPressure.Text = "Current : " +
                                        CurrentPressure.ToString() +
                                        " RealTime : " +
                                        DataA.RealTimeArray[currentIndex].ToString();// + " " +
//                                        MaxPressure.ToString();
            if (Decomposer.RecordStarted)
            {
                if (PressureMeasStatus == PressureMeasurementStatus.Calibration)
                {
                    Decomposer.ZeroLine = Decomposer.tmpZero;
                    USBPort.WriteByte((byte)CmdDevice.ValveFastClose);
                    USBPort.WriteByte((byte)CmdDevice.ValveSlowClose);
                    USBPort.WriteByte((byte)CmdDevice.PumpSwitchOn);
                    PressureMeasStatus = PressureMeasurementStatus.Pumping;
                    PumpStatus = PumpingStatus.WaitingForLevel;
                }
                labRecordSize.Text = "Record size : " + (e.PacketCounter / Decomposer.SamplingFrequency).ToString() + " c";
                DataA.DebugArray[currentIndex] = (int)Detector.Detect(DataA.DerivArray, (int)currentIndex);
                if (CurrentPressure > MaxAllowablePressure)
                { 
                    StopPumping("Max Allowable Pressure");
                }
                if (PumpStatus == PumpingStatus.WaitingForLevel)
                {
                    bool timeout = e.PacketCounter / Decomposer.SamplingFrequency > MaxTimeAfterStartPumping;
                    if (timeout)
                    {
                        ShowError(BPMError.AirLeak);

                    }
                }
                if (PumpStatus == PumpingStatus.MaximumFound)
                {
                    int Index = e.PacketCounter;
                    bool timeout = (Index - MomentMaxFound) / Decomposer.SamplingFrequency > MaxTimeAfterMaxFound;
                    if (timeout && CurrentPressure > MinPressure)
                    {
                        StopPumping("Timeout");
                    }
                }
                if (PressureMeasStatus == PressureMeasurementStatus.Delay)
                {
                    DelayCounter++;
                    if (DelayCounter > DelayValue)
                    {
                        PressureMeasStatus = PressureMeasurementStatus.Measurement;
                        AfterDelay();
                    }
                }
            }
        }

        private void StopPumping(string mess)
        {
            DevStatus.PumpIsOn = false;
            DevStatus.ValveSlowClosed = false;
            labStopPumpingReason.Text = mess;
            PumpStatus = PumpingStatus.Ready;
            USBPort.WriteByte((byte)CmdDevice.PumpSwitchOff);
            PressureMeasStatus = PressureMeasurementStatus.Delay;
            DelayCounter = 0;
            DelayValue = (int)(Decomposer.SamplingFrequency * DelayInSeconds);
        }

        private void AfterDelay()
        {
            PumpStatus = PumpingStatus.Ready;
            Decomposer.PacketCounter = 0;
            timerDetectRate.Enabled = true;
            Decomposer.MainIndex = 0;
            MaxDerivValue = 0;
            Detector?.Reset();
            USBPort.WriteByte((byte)CmdDevice.ValveSlowOpen);
            PressureMeasStatus = PressureMeasurementStatus.Measurement;
            timerDetectRate.Enabled = true;
        }

        private void ShowError(BPMError error)
        {
            if (error == BPMError.AirLeak)
            {
                butPressureMeasAbort_Click(this, EventArgs.Empty);
            }
            string errorText = error switch
            {
                BPMError.AirLeak     => "Air leak",
                BPMError.ReadingFile => "Reading file error",
                BPMError.Connection  => "Connection failure",
                BPMError.Sys         => "Systolic pressure error",
                BPMError.Dia         => "Diastolic pressure error",
                _ => "",
            };
            MessageBox.Show(errorText, "Error");
        }
    }
}