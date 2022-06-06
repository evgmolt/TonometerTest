namespace TTestApp
{
    public partial class Form1 : Form, IMessageHandler
    {
        USBserialPort USBPort;
        public bool Connected;
        private DataArrays DataA;
        private ByteDecomposer decomposer;
        readonly StreamWriter textWriter;
        TTestConfig Cfg;
        string CurrentFile;
        int MaxSize;
        bool ViewMode = true;
        int ViewShift;
        BufferedPanel bufPanel;

        public event Action<Message> WindowsMessage;

        public Form1()
        {
            InitializeComponent();
            bufPanel = new BufferedPanel(0);
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
            radioButton11.Checked = true;
            panelGraph.Controls.Add(bufPanel);
            panelGraph.Dock = DockStyle.Fill;
            bufPanel.Dock = DockStyle.Fill;
            bufPanel.Paint += bufferedPanel_Paint;
            USBPort = new USBserialPort(this, 115200);
            USBPort.ConnectionFailure += onConnectionFailure;
            USBPort.Connect();
            //            InitArraysForFlow();
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
            int size = lines.Count();
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
            DataA.CountViewArray(bufPanel.Width, Cfg.FilterOn);
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
        private void buffPanel_Paint(int[] data, Control panel, double ScaleY, int MaxSize, PaintEventArgs e)
        {
            if (data == null)
            {
                return;
            }
            if (data.Length == 0)
            {
                return;
            }
            float tension = 0.1F;
            var R0 = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
            var pen0 = new Pen(Color.Black, 1);
            e.Graphics.Clear(Color.White);
            e.Graphics.DrawRectangle(pen0, R0);
            pen0.Dispose();
            Point[] OutArray;
            if (!ViewMode)
            {
                OutArray = ViewArrayMaker.MakeArray(panel, data, (uint)data.Length, MaxSize, ScaleY);
            }
            else
            {
                OutArray = ViewArrayMaker.MakeArrayForView(panel, data, ViewShift, MaxSize, ScaleY, 1);
            }
            var pen = new Pen(Color.Red, 1);
            e.Graphics.DrawCurve(pen, OutArray, tension);
            pen.Dispose();
        }

        private void bufferedPanel_Paint(object sender, PaintEventArgs e)
        {
            if (DataA == null)
            {
                return;
            }
            if (radioButton11.Checked)
            {
                buffPanel_Paint(DataA.PressureViewArray, bufPanel, 1, MaxSize, e);
            }
            else
            {
                buffPanel_Paint(DataA.PressureCompressedArray, bufPanel, 1, MaxSize, e);

            }
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            ViewShift = hScrollBar1.Value;
            bufPanel.Refresh();

        }

        private void checkBoxFilter_CheckedChanged(object sender, EventArgs e)
        {
            Cfg.FilterOn = checkBoxFilter.Checked;
            if (DataA == null)
            {
                return;
            }
            DataA.CountViewArray(bufPanel.Width, Cfg.FilterOn);
            MaxSize = DataProcessing.GetRange(DataA.PressureViewArray);
            bufPanel.Refresh();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cfg.Maximized = WindowState == FormWindowState.Maximized;
            Cfg.WindowWidth = Width;
            Cfg.WindowHeight = Height;
            TTestConfig.SaveConfig(Cfg);
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            bufPanel.Refresh();
        }

        private void radioButtonFit_CheckedChanged(object sender, EventArgs e)
        {
            bufPanel.Refresh();
        }
    }
}