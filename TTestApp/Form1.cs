namespace TTestApp
{
    public partial class Form1 : Form, IMessageHandler
    {
        USBserialPort USBPort;
        public bool Connected;
        private DataArrays? DataA;
        private ByteDecomposer decomposer;
        StreamWriter textWriter;
        TTestConfig Cfg;
        string CurrentFile;
        string TmpDataFile = "tmpdata.t";
        int MaxSize = 500;
        bool ViewMode = false;
        int ViewShift;
        BufferedPanel bufPanel;
        double ScaleY = 1;

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
                if (m.Msg == WM_DEVICECHANGE) WindowsMessage(m);
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
            int size = lines.Length;
            labRecordSize.Text = "Record size : " + (size / ByteDecomposer.SamplingFrequency).ToString() + " s";
            UpdateScrollBar(size);

            if (size == 0)
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
            CreateDetrendArray(size);
            DataA.CountViewArrays(bufPanel.Width, Cfg);
            bufPanel.Refresh();
        }

        private void CreateDetrendArray(int size)
        {
            for (int i = 0; i < DataA.RealTimeArray.Length; i++)
            {
                DataA.PressureViewArray[i] = Filter.Median(6, DataA.RealTimeArray, i);
            }
            DataA.DetrendArray = new double[size];
            double max = DataA.PressureViewArray.Max<double>();
            int maxInd = DataA.PressureViewArray.ToList().IndexOf(max);
            double startVal = DataA.PressureViewArray[0];
            for (int i = 0; i < maxInd; i++)
            {
                DataA.DetrendArray[i] = startVal + i * (max - startVal) / maxInd;
            }

            for (int i = 0; i < maxInd; i++)
            {
                DataA.PressureArray[i] = DataA.PressureViewArray[i] - DataA.DetrendArray[i];
            }

            DataA.PressureViewArray = DataProcessing.GetSmoothArray(DataA.PressureArray, 40);
            
            string[] corr = File.ReadAllLines("file.txt");
            double[] corrArray = new double[corr.Length];
            for (int i = 0; i < corr.Length; i++)
            {
                corrArray[i] = double.Parse(corr[i]);
            }
            DataA.PressureArray = DataProcessing.Corr(DataA.PressureViewArray, corrArray);
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

        private void buffPanel_Paint(
            List<double[]> data, 
            List<int[]> visirs, 
            Control panel, 
            double ScaleY, 
            int MaxSize, 
            PaintEventArgs e)
        {
            Color[] curveColors = { Color.Red, Color.Blue, Color.Green };
            if (data == null)
            {
                return;
            }
            if (data.Count == 0)
            {
                return;
            }
            float tension = 0.1F;
            var R0 = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
            var pen0 = new Pen(Color.Black, 1);
            e.Graphics.Clear(Color.White);
            e.Graphics.DrawRectangle(pen0, R0);
            if (!ViewMode)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    var pen = new Pen(curveColors[i], 1);
                    Point[] OutArray = ViewArrayMaker.MakeArray(panel, 
                                                                data[i], 
                                                                decomposer.MainIndex, 
                                                                MaxSize, 
                                                                ScaleY);

                    e.Graphics.DrawCurve(pen, OutArray, tension);
                    pen.Dispose();
                }
            }
            else
            {
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i] == null) break;
                    var pen = new Pen(curveColors[i], 1);
                    Point[] OutArray = ViewArrayMaker.MakeArrayForView(panel, 
                                                                       data[i], 
                                                                       ViewShift, 
                                                                       MaxSize, 
                                                                       ScaleY, 
                                                                       1);
                    e.Graphics.DrawCurve(pen, OutArray, tension);
                    pen.Dispose();
                }
                int X1 = ViewShift;
                int X2 = panel.Width + ViewShift;
                for (int i = 0; i < visirs.Count; i++)
                {
                    if (visirs[i] == null) break;
                    var pen = new Pen(curveColors[i], 1);
                    for (int j = 0; j < visirs[i].Length; j++)
                    {
                        if (visirs[i][j] > X1 && visirs[i][j] < X2)
                        {
                            e.Graphics.DrawLine(pen, visirs[i][j] - ViewShift, 0, visirs[i][j] - ViewShift, panel.Width);
                        }
                    }
                    pen.Dispose();
                }
            }
            pen0.Dispose();
        }

        private void bufferedPanel_Paint(object? sender, PaintEventArgs e)
        {
            if (DataA == null)
            {
                return;
            }
            var ArrayList = new List<double[]>();
            var VisirList = new List<int[]>();
            int[] visirs1 = { 100, 1500, 1600, 3000, 6000 };
            VisirList.Add(visirs1);

            if (ViewMode)
            {
                if (radioButton11.Checked) //1:1
                {
                    ArrayList.Add(DataA.PressureArray);
                    ArrayList.Add(DataA.PressureViewArray);
                }
                else //fit
                {
                    ArrayList.Add(DataA.PressureCompressedArray);
                }
            }
            else
            {
                ArrayList.Add(DataA.PressureArray);
                ArrayList.Add(DataA.PressureViewArray);
            }
            buffPanel_Paint(ArrayList, VisirList, bufPanel, ScaleY, MaxSize, e);
            ArrayList.Clear();
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
            DataA.CountViewArrays(bufPanel.Width, Cfg);
            MaxSize = DataProcessing.GetRange(DataA.PressureViewArray);
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
            hScrollBar1.Visible = radioButton11.Checked;
            bufPanel.Refresh();
        }

        private void radioButtonFit_CheckedChanged(object? sender, EventArgs e)
        {
            hScrollBar1.Visible = !radioButtonFit.Checked;
            hScrollBar1.Value = 0;
            bufPanel.Refresh();
        }

        private void Form1_Resize(object? sender, EventArgs e)
        {
            if (DataA == null)
            {
                return;
            }
            DataA.CountViewArrays(bufPanel.Width, Cfg);
            bufPanel.Refresh();
        }

        private void bufPanel_MouseMove(object? sender, MouseEventArgs e)
        {
            labelXY.Text = String.Format("X : {0}  Y : {1}", e.X + ViewShift, e.Y);
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
            decomposer.TotalBytes = 0;
            decomposer.LineCounter = 0;
            decomposer.RecordStarted = true;
            progressBarRecord.Visible = true;
        }

        private void NewLineReceived(object? sender, EventArgs e)
        {
            if (decomposer.MainIndex > 0)
            {
                label4.Text = DataA.DCRealTimeArray[decomposer.MainIndex - 1].ToString();
            }
            if (decomposer.RecordStarted)
            {
                labRecordSize.Text = "Record size : " + (decomposer.LineCounter / ByteDecomposer.SamplingFrequency).ToString() + " c";
            }
        }

        private void timerStatus_Tick(object sender, EventArgs e)
        {
            if (USBPort == null)
            {
                labPort.Text = "Disconnected";
                Connected = false;
                return;
            }
            if (USBPort.PortHandle == null)
            {
                labPort.Text = "Disconnected";
                Connected = false;
                return;
            }
            if (USBPort.PortHandle.IsOpen)
            {
                labPort.Text = "Connected to " + USBPort.PortNames[USBPort.CurrentPort];
                Connected = true;
            }
            else
            {
                labPort.Text = "Disconnected";
                Connected = false;
            }
            butStartRecord.Enabled = !ViewMode && !decomposer.RecordStarted!;
            butStopRecord.Enabled = decomposer.RecordStarted;
            butSaveFile.Enabled = ViewMode && decomposer.LineCounter != 0;
            butFlow.Text = ViewMode? "Start stream" : "Stop stream";
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
            timerRead.Enabled = false;

            ReadFile(Cfg.DataDir + TmpDataFile);
        }
    }
}