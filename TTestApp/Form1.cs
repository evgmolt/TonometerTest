namespace TTestApp
{
    public partial class Form1 : Form, IMessageHandler
    {
        USBserialPort USBPort;
        DataArrays? DataA;
        ByteDecomposerBCI decomposer;
        Painter painter;
        WaveDetector WD;
        HRV hrv;
        BufferedPanel bufPanel;
        TTestConfig Cfg;
        StreamWriter textWriter;
        string CurrentFile;
        int CurrentFileSize;
        string TmpDataFile = "tmpdata.t";
        int MaxValue = 100000;
//        int MaxValue = 1000;
        bool ViewMode = false;
        int ViewShift;
        double ScaleY = 1;
        List<int[]> VisirList;
        bool Compression = false;

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
            numUDLeft.Value = Cfg.CoeffLeft;
            numUDRight.Value = Cfg.CoeffRight;
            radioButton11.Checked = true;
            panelGraph.Dock = DockStyle.Fill;
            panelGraph.Controls.Add(bufPanel);
            bufPanel.Dock = DockStyle.Fill;
            bufPanel.Paint += bufferedPanel_Paint;
            WD = new WaveDetector();
            VisirList = new List<int[]>();
            USBPort = new USBserialPort(this, 460800);
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
            decomposer = new ByteDecomposerBCI(DataA);
            decomposer.DecomposeLineEvent += NewLineReceived;
            painter = new Painter(bufPanel, decomposer);
        }

        private void onConnectionFailure(Exception obj)
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
            int DCArrayWindow = 100;
            DataA.DCArray = DataProcessing.GetSmoothArray(DataA.RealTimeArray, DCArrayWindow);
            DataA.CountViewArrays(bufPanel);

            //Детектор
            WD.Reset();
            for (int i = 0; i < DataA.DerivArray.Length; i++)
            {
                DataA.DebugArray[i] = WD.Detect(0, DataA.DerivArray, i);
            }

            var ArrayOfWaveIndexes = WD.FiltredPoints.ToArray();
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
            labPulse.Text = "Pulse : " + DataProcessing.GetPulseFromIndexesArray(ArrayForPulse).ToString();
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
            labSys.Text = "Sys : " + ValueToMmhG(P2).ToString();
            labDia.Text = "Dia : " + ValueToMmhG(P1).ToString();

            hrv = new HRV(ArrayOfWaveIndexes);
            BCICommands.CountCheckSum(ref BCICommands.CommandADC);
            panelHisto.Refresh();
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
//                    ArrayList.Add(DataA.DCArray);
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
//                ArrayList.Add(DataA.RealTimeArray);
//                ArrayList.Add(DataA.DCArray);
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
                bufPanel.Refresh();
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
            DataA.CountViewArrays(bufPanel);
            bufPanel.Refresh();
        }

        private void bufPanel_MouseMove(object? sender, MouseEventArgs e)
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
            double sec = index / ByteDecomposer.SamplingFrequency;
            labelX.Text = String.Format("X : {0}, Time {1:f2} s ", index, sec);
            labY0.Text = String.Format("PressureViewArray : {0:f0}", DataA.PressureViewArray[index]);
            labY1.Text = String.Format("DerivArray : {0:f0}", DataA.DerivArray[index]);
            labY2.Text = String.Format("DCArray : {0:f0}", DataA.DCArray[index]);
        }

        private void trackBarAmp_ValueChanged(object? sender, EventArgs e)
        {
            double a = trackBarAmp.Value;
            ScaleY = Math.Pow(2, a / 2);
            bufPanel.Refresh();
        }

        private void butStartRecord_Click(object sender, EventArgs e)
        {
            textWriter = new StreamWriter(Cfg.DataDir + TmpDataFile);
            decomposer.LineCounter = 0;
            decomposer.RecordStarted = true;
            progressBarRecord.Visible = true;
            labMeanPressure.Text = "Mean : ";
            labSys.Text = "Sys : ";
            labDia.Text = "Dia : ";
            labPulse.Text = "Pulse : ";
        }

        private void NewLineReceived(object? sender, EventArgs e)
        {

            if (decomposer.MainIndex > 0)
            {
                labCurrentPressure.Text = "Current : " + ValueToMmhG(DataA.DCArray[decomposer.MainIndex - 1]).ToString();
//                labCurrentPressure.Text = "Current : " + DataA.RealTimeArray[decomposer.MainIndex - 1].ToString() + " " +
                    DataA.DCArray[decomposer.MainIndex - 1].ToString();
            }
            if (decomposer.RecordStarted)
            {
                labRecordSize.Text = "Record size : " + (decomposer.LineCounter / ByteDecomposer.SamplingFrequency).ToString() + " c";
            }
        }

        private int ValueToMmhG(double value)
        {
            double zero = 201230;
            double pressure = 182;
            double val = 1011080;
            return (int)((value - zero) * pressure / (val - zero));
        }

        private void timerStatus_Tick(object sender, EventArgs e)
        {
            if (decomposer is null)
            {
                return;
            }
            butStartRecord.Enabled = !ViewMode && !decomposer.RecordStarted!;
            butStopRecord.Enabled = decomposer.RecordStarted;
            butSaveFile.Enabled = ViewMode && decomposer.LineCounter != 0;
            butFlow.Text = ViewMode ? "Start stream" : "Stop stream";
            panelView.Enabled = ViewMode;

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
            bufPanel.Refresh();
        }

        private void panelHisto_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            var R0 = new Rectangle(0, 0, panelHisto.Width, panelHisto.Height);
            var pen0 = new Pen(Color.Black, 1);
            e.Graphics.DrawRectangle(pen0, R0);
        }

        private void butBCISetup_Click(object sender, EventArgs e)
        {
            int numOfRegs = 24;
            for (int i = 0; i < numOfRegs; i++)
            {
//                USBPort.WriteByte()
            }
        }
    }
}