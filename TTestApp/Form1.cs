namespace TTestApp
{
    public partial class Form1 : Form, IMessageHandler
    {
        USBserialPort USBPort;
        public bool Connected;
        private DataArrays? DataA;
        private ByteDecomposer decomposer;
        readonly StreamWriter textWriter;
        TTestConfig Cfg;
        string CurrentFile;
        int MaxSize;
        bool ViewMode = true;
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
            radioButton11.Checked = true;
            panelGraph.Dock = DockStyle.Fill;
            panelGraph.Controls.Add(bufPanel);
            bufPanel.Dock = DockStyle.Fill;
            bufPanel.Paint += bufferedPanel_Paint;
            USBPort = new USBserialPort(this, 115200);
            USBPort.ConnectionFailure += onConnectionFailure;
            USBPort.Connect();
            DataProcessing.CompressionChanged += onCompressionChanged;
            //            InitArraysForFlow();
        }

        private void onCompressionChanged(object? sender, EventArgs e)
        {
            labCompressionRatio.Text = DataProcessing.CompressionRatio.ToString();
        }

        private void InitArraysForFlow()
        {
            DataA = new DataArrays(ByteDecomposer.DataArrSize);
            decomposer = new ByteDecomposer(DataA);
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

        private void butOpenFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.InitialDirectory = Cfg.DataDir.ToString();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog1.FileName))
                {
                    Cfg.DataDir = Path.GetDirectoryName(openFileDialog1.FileName) + @"\";
                    TTestConfig.SaveConfig(Cfg);
                    timerRead.Enabled = false;

                    CurrentFile = Path.GetFileName(openFileDialog1.FileName);
                    ReadFile(Cfg.DataDir + CurrentFile);
                }
            }

        }

        private void ReadFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            int size = lines.Length;
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
            DataA.CountViewArrays(bufPanel.Width, Cfg);
            MaxSize = DataProcessing.GetRange(DataA.PressureViewArray);
            bufPanel.Refresh();
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

        private void buffPanel_Paint(List<int[]> data, Control panel, double ScaleY, int MaxSize, PaintEventArgs e)
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
                Point[] OutArray = ViewArrayMaker.MakeArray(panel, data[0], (uint)data[0].Length, MaxSize, ScaleY);
                e.Graphics.DrawCurve(pen0, OutArray, tension);
            }
            else
            {
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i] == null) break;
                    var pen = new Pen(curveColors[i], 1);
                    Point[] OutArray = ViewArrayMaker.MakeArrayForView(panel, data[i], ViewShift, MaxSize, ScaleY, 1);
                    e.Graphics.DrawCurve(pen, OutArray, tension);
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
            var ArrayList = new List<int[]>();
            
            if (ViewMode)
            {
                if (radioButton11.Checked)
                {
                    ArrayList.Add(DataA.PressureViewArray);
                    ArrayList.Add(DataA.PressureFiltredMedian);
//                    ArrayList.Add(DataA.DiffArray);
                }
                else
                {
                    ArrayList.Add(DataA.PressureCompressedArray);
                    ArrayList.Add(DataA.PressureFiltredCompressedArray);
                    ArrayList.Add(DataA.PressureSmoothArray);
                }
            }
            else
            {
                ArrayList.Add(DataA.PressureViewArray);
            }
            buffPanel_Paint(ArrayList, bufPanel, ScaleY, MaxSize, e);
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
            bufPanel.Refresh();
        }

        private void radioButtonFit_CheckedChanged(object? sender, EventArgs e)
        {
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
    }
}