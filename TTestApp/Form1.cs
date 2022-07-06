namespace TTestApp
{
    public partial class Form1 : Form, IMessageHandler
    {
        USBserialPort USBPort;
        DataArrays? DataA;
        ByteDecomposer decomposer;
        Painter painter;
        WaveDetector WD;
        BufferedPanel bufPanel;
        TTestConfig Cfg;
        StreamWriter textWriter;
        string CurrentFile;
        int CurrentFileSize;
        string TmpDataFile = "tmpdata.t";
        int MaxValue = 500;
        bool ViewMode = false;
        int ViewShift;
        double ScaleY = 1;
        List<int[]> VisirList;
        int MaxPress = 0;

        public event Action<Message> WindowsMessage;

        public Form1()
        {
            InitializeComponent();
            bufPanel = new BufferedPanel(0);
            bufPanel.MouseMove += bufPanel_MouseMove;
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
            checkBoxFilter.Checked = Cfg.FilterOn;
            numUDownSmooth.Value = Cfg.SmoothWindowSize;
            numUDownMedian.Value = Cfg.MedianWindowSize;
            radioButton11.Checked = true;
            panelGraph.Dock = DockStyle.Fill;
            panelGraph.Controls.Add(bufPanel);
            bufPanel.Dock = DockStyle.Fill;
            bufPanel.Paint += bufferedPanel_Paint;
            WD = new WaveDetector();
            VisirList = new List<int[]>();
            USBPort = new USBserialPort(this, 19200);
            USBPort.ConnectionFailure += onConnectionFailure;
            USBPort.Connect();
            DataProcessing.CompressionChanged += onCompressionChanged;
            InitArraysForFlow();
        }

        private void onCompressionChanged(object? sender, EventArgs e)
        {
            labCompressionRatio.Text = DataProcessing.CompressionRatio.ToString();
        }

        private void InitArraysForFlow()
        {

            DataA = new DataArrays(ByteDecomposer.DataArrSize);
            decomposer = new ByteDecomposer(DataA);
            decomposer.DecomposeLineEvent += NewLineReceived;
            painter = new Painter(bufPanel, decomposer);
        }

        private void onConnectionFailure(Exception obj)
        {
            MessageBoxButtons but = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Error;
            MessageBox.Show("Connection failure", "Error", but, icon);
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

        private void timerRead_Tick(object sender, EventArgs e)
        {
            if (USBPort == null) return;
            if (USBPort.PortHandle == null) return;
            if (!USBPort.PortHandle.IsOpen) return;
            if (decomposer != null)
            {
                decomposer.Decompos(USBPort, null, textWriter).ToString();
            }
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
                File.Move(Cfg.DataDir + TmpDataFile, Cfg.DataDir + CurrentFile);
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
            labRecordSize.Text = "Record size : " + (CurrentFileSize / ByteDecomposer.SamplingFrequency).ToString() + " s";
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
            bufPanel.Refresh();
        }

        private void PrepareData()
        {
            //int StartDetectValue = 150;
            //int StopDetectValue = 420;
            int DCArrayWindow = 100;
            int ExpandArrayBy = 5;
            DataA.DCArray = DataProcessing.GetSmoothArray(DataA.RealTimeArray, DCArrayWindow);
            DataA.CountViewArrays(bufPanel, Cfg);

            //Детектор
            WD.Reset();
            //int StartIndex = 0;
            //int StopIndex = 0;
            //for (int i = 0; i < DataA.DCArray.Length; i++)
            //{
            //    if (StartIndex == 0)
            //    {
            //        if (DataA.DCArray[i] > StartDetectValue)
            //        {
            //            StartIndex = i;
            //        }
            //    }
            //    if (DataA.DCArray[i] > StopDetectValue)
            //    {
            //        StopIndex = i;
            //        break;
            //    }
            //}
            //StartIndex = 0;
            //for (int i = StartIndex; i < StopIndex; i++)

            for (int i = 0; i < DataA.CorrelationArray.Length; i++)
            {
                DataA.DebugArray[i] = WD.Detect(0, DataA.CorrelationArray, i);
            }

            var NNArrSeq0 = DataProcessing.GetSequentialArray(WD.FiltredPoints.ToArray());
            if (NNArrSeq0 == null)
            {
                return;
            }
            labPulse.Text = "Pulse: " + DataProcessing.GetPulseFromPoints(NNArrSeq0).ToString();
            var NNArrSeq = DataProcessing.ExpandArray(NNArrSeq0, DataA.CorrelationArray, ExpandArrayBy);
            int X1 = NNArrSeq[0];
            int X2 = NNArrSeq[^1];
            double V1 = DataA.RealTimeArray[X1];
            double V2 = DataA.RealTimeArray[X2];
            int P1 = ValueToMmhG(V1);
            int P2 = ValueToMmhG(V2);
            VisirList.Clear();
            VisirList.Add(NNArrSeq);

            double max = 0;
            int XMax = 0;
            int XMaxIndex = 0;
            for (int i = 0; i < NNArrSeq.Length; i++)
            {
                if (DataA.PressureViewArray[NNArrSeq[i] + DataA.CorrPatternLength / 2] > max)
                {
                    max = DataA.PressureViewArray[NNArrSeq[i] + DataA.CorrPatternLength / 2];
                    XMax = NNArrSeq[i] + DataA.CorrPatternLength / 2;
                    XMaxIndex = i;
                }
            }
            int MaxPress = (int)DataA.RealTimeArray[XMax];
            double CoeffLeft = 0.48;
            double CoeffRight = 0.83;
            V1 = max * CoeffLeft;
            V2 = max * CoeffRight;
            for (int i = XMaxIndex; i > 0; i--)
            {
                if (DataA.PressureViewArray[NNArrSeq[i] + DataA.CorrPatternLength / 2] < V1)
                {
                    int x1 = NNArrSeq[i];
                    int x2 = NNArrSeq[i + 1];
                    double y1 = DataA.PressureViewArray[x1 + DataA.CorrPatternLength / 2];
                    double y2 = DataA.PressureViewArray[x2 + DataA.CorrPatternLength / 2];
                    double coeff = (V1 - y1) / (y2 - y1);
                    double yDC1 = DataA.DCArray[x1 + DataA.CorrPatternLength / 2];
                    double yDC2 = DataA.DCArray[x2 + DataA.CorrPatternLength / 2];
                    P1 = (int)(yDC1 + (yDC2 - yDC1) * coeff);
                    //P1 = (int)DataA.RealTimeArray[NNArrSeq[i] + DataA.CorrPatternLength / 2];
                    //P1 = (int)DataA.DCArray[NNArrSeq[i] + DataA.CorrPatternLength / 2];
                    break;
                }
            }
            for (int i = XMaxIndex; i < NNArrSeq.Length; i++)
            {
                if (DataA.PressureViewArray[NNArrSeq[i] + DataA.CorrPatternLength / 2] < V2)
                {
                    int x1 = NNArrSeq[i];
                    int x2 = NNArrSeq[i - 1];
                    double y1 = DataA.PressureViewArray[x1 + DataA.CorrPatternLength / 2];
                    double y2 = DataA.PressureViewArray[x2 + DataA.CorrPatternLength / 2];
                    double coeff = (V2 - y1) / (y2 - y1);
                    double yDC1 = DataA.DCArray[x1 + DataA.CorrPatternLength / 2];
                    double yDC2 = DataA.DCArray[x2 + DataA.CorrPatternLength / 2];
                    P2 = (int)(yDC2 + (yDC1 - yDC2) * coeff);
                    //P2 = (int)DataA.RealTimeArray[NNArrSeq[i] + DataA.CorrPatternLength / 2];
                    //P2 = (int)DataA.DCArray[NNArrSeq[i] + DataA.CorrPatternLength / 2];
                    break;
                }
            }

            int[] envelopeArray = new int[NNArrSeq.Length];
            int[] envelopeMmhGArray = new int[NNArrSeq.Length];
            for (int i = 0; i < NNArrSeq.Length; i++)
            {
                envelopeArray[i] = (int)DataA.RealTimeArray[NNArrSeq[i] + DataA.CorrPatternLength / 2];
                envelopeMmhGArray[i] = ValueToMmhG(DataA.RealTimeArray[NNArrSeq[i] + DataA.CorrPatternLength / 2]);
            }
            labMeanPressure.Text = "Mean : " + ValueToMmhG(MaxPress).ToString();
            labSys.Text = "Sys : " + ValueToMmhG(P2).ToString();
            labDia.Text = "Dia : " + ValueToMmhG(P1).ToString();
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
                    ArrayList.Add(DataA.DCArray);
                    ArrayList.Add(DataA.PressureViewArray);
                    ArrayList.Add(DataA.CorrelationArray);
                    ArrayList.Add(DataA.DebugArray);
                }
                else //fit
                {
                    ArrayList.Add(DataA.CompressedArray);
                }
            }
            else
            {
                ArrayList.Add(DataA.RealTimeArray);
                ArrayList.Add(DataA.DCArray);
            }
            painter.Paint(ViewMode, ViewShift, ArrayList, VisirList, ScaleY, MaxValue, e);
            ArrayList.Clear();
        }

        private void UpdateScrollBar(int size)
        {
            int space = 14;
            hScrollBar1.Maximum = size;
            hScrollBar1.LargeChange = panelGraph.Width - space - 50;
            hScrollBar1.SmallChange = panelGraph.Width - space / 10;
            hScrollBar1.AutoSize = true;
            hScrollBar1.Value = 0;
            hScrollBar1.Visible = hScrollBar1.Maximum > hScrollBar1.Width;
        }

        private void hScrollBar1_ValueChanged(object? sender, EventArgs e)
        {
            ViewShift = hScrollBar1.Value;
            bufPanel.Refresh();
        }

        private void checkBoxFilter_CheckedChanged(object? sender, EventArgs e)
        {
            Cfg.FilterOn = checkBoxFilter.Checked;
            if (DataA == null)
            {
                return;
            }
            DataA.CountViewArrays(bufPanel, Cfg);
//            MaxSize = DataProcessing.GetRange(DataA.PressureViewArray);
            bufPanel.Refresh();
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
                hScrollBar1.Visible = radioButton11.Checked;
                bufPanel.Refresh();
            }
        }

        private void radioButtonFit_CheckedChanged(object? sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                hScrollBar1.Visible = !radioButtonFit.Checked;
                hScrollBar1.Value = 0;
                MaxValue = 2 * (int)DataA.CompressedArray.Max();
                bufPanel.Refresh();
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
            DataA.CountViewArrays(bufPanel, Cfg);
            bufPanel.Refresh();
        }

        private void bufPanel_MouseMove(object? sender, MouseEventArgs e)
        {
            if (DataA == null)
            {
                return;
            }
            double x = e.X + ViewShift;
            double sec = x / ByteDecomposer.SamplingFrequency;
            labelXY.Text = String.Format(
                "X : {0}, Time {1:f2} s, Y : {2}", 
                e.X + ViewShift,
                sec,
                DataA.DCArray[e.X + ViewShift]);
        }

        private void trackBarAmp_ValueChanged(object? sender, EventArgs e)
        {
            double a = trackBarAmp.Value;
            ScaleY = Math.Pow(2, a / 2);
            bufPanel.Refresh();
        }

        private void numUDownSmooth_ValueChanged(object? sender, EventArgs e)
        {
            Cfg.SmoothWindowSize = (int)numUDownSmooth.Value;
        }

        private void numUDownMedian_ValueChanged(object sender, EventArgs e)
        {
            Cfg.MedianWindowSize = (int)numUDownMedian.Value;
        }

        private void butStartRecord_Click(object sender, EventArgs e)
        {
            textWriter = new StreamWriter(Cfg.DataDir + TmpDataFile);
            decomposer.LineCounter = 0;
            decomposer.RecordStarted = true;
            progressBarRecord.Visible = true;
        }

        private void NewLineReceived(object? sender, EventArgs e)
        {

            if (decomposer.MainIndex > 0)
            {
                labCurrentPressure.Text = "Current: " + ValueToMmhG(DataA.DCArray[decomposer.MainIndex - 1]).ToString();
            }
            if (decomposer.RecordStarted)
            {
                labRecordSize.Text = "Record size : " + (decomposer.LineCounter / ByteDecomposer.SamplingFrequency).ToString() + " c";
            }
        }

        private int ValueToMmhG(double value)
        {
            double zero = 100;
            double pressure = 172;
            double val = 459;
            return (int)((value - zero) * pressure / (val - zero));
        }

        private void timerStatus_Tick(object sender, EventArgs e)
        {
            butStartRecord.Enabled = !ViewMode && !decomposer.RecordStarted!;
            butStopRecord.Enabled = decomposer.RecordStarted;
            butSaveFile.Enabled = ViewMode && decomposer.LineCounter != 0;
            butFlow.Text = ViewMode ? "Start stream" : "Stop stream";

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

        private void timerRead_Tick_1(object sender, EventArgs e)
        {
            if (USBPort == null) return;
            if (USBPort.PortHandle == null) return;
            if (!USBPort.PortHandle.IsOpen) return;
            if (decomposer != null)
            {
                decomposer.Decompos(USBPort, null, textWriter);
            }
        }

        private void timerPaint_Tick(object sender, EventArgs e)
        {
            bufPanel.Refresh();
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
            decomposer.DecomposeLineEvent -= NewLineReceived;
            decomposer.RecordStarted = false;
            if (textWriter != null) textWriter.Dispose();
            ViewMode = true;
            timerPaint.Enabled = !ViewMode;
            timerRead.Enabled = false;

            ReadFile(Cfg.DataDir + TmpDataFile);
        }
    }
}