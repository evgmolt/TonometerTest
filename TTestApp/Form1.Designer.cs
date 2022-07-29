namespace TTestApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.butRefresh = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.numUDRight = new System.Windows.Forms.NumericUpDown();
            this.panelView = new System.Windows.Forms.Panel();
            this.radioButton11 = new System.Windows.Forms.RadioButton();
            this.radioButtonFit = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.butFlow = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numUDLeft = new System.Windows.Forms.NumericUpDown();
            this.butOpenFile = new System.Windows.Forms.Button();
            this.panelGraph = new System.Windows.Forms.Panel();
            this.trackBarAmp = new System.Windows.Forms.TrackBar();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panelHRV = new System.Windows.Forms.Panel();
            this.labAMo = new System.Windows.Forms.Label();
            this.labSDNN = new System.Windows.Forms.Label();
            this.labNumOfWaves = new System.Windows.Forms.Label();
            this.labY2 = new System.Windows.Forms.Label();
            this.labY1 = new System.Windows.Forms.Label();
            this.labY0 = new System.Windows.Forms.Label();
            this.labMaxSize = new System.Windows.Forms.Label();
            this.labPulse = new System.Windows.Forms.Label();
            this.labDia = new System.Windows.Forms.Label();
            this.labSys = new System.Windows.Forms.Label();
            this.labMeanPressure = new System.Windows.Forms.Label();
            this.labCurrentPressure = new System.Windows.Forms.Label();
            this.labPort = new System.Windows.Forms.Label();
            this.labCompressionRatio = new System.Windows.Forms.Label();
            this.labelX = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.progressBarRecord = new System.Windows.Forms.ProgressBar();
            this.butSaveFile = new System.Windows.Forms.Button();
            this.labRecordSize = new System.Windows.Forms.Label();
            this.butStopRecord = new System.Windows.Forms.Button();
            this.butStartRecord = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.histoPanel = new System.Windows.Forms.Panel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timerRead = new System.Windows.Forms.Timer(this.components);
            this.timerStatus = new System.Windows.Forms.Timer(this.components);
            this.timerPaint = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRight)).BeginInit();
            this.panelView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAmp)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.panelHRV.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.43267F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.56733F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelGraph, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.trackBarAmp, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelBottom, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 173F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1488, 820);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.butRefresh);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.numUDRight);
            this.panel1.Controls.Add(this.panelView);
            this.panel1.Controls.Add(this.butFlow);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.numUDLeft);
            this.panel1.Controls.Add(this.butOpenFile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(256, 356);
            this.panel1.TabIndex = 0;
            // 
            // butRefresh
            // 
            this.butRefresh.Location = new System.Drawing.Point(25, 239);
            this.butRefresh.Name = "butRefresh";
            this.butRefresh.Size = new System.Drawing.Size(94, 29);
            this.butRefresh.TabIndex = 13;
            this.butRefresh.Text = "Refresh";
            this.butRefresh.UseVisualStyleBackColor = true;
            this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Right";
            // 
            // numUDRight
            // 
            this.numUDRight.DecimalPlaces = 2;
            this.numUDRight.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numUDRight.Location = new System.Drawing.Point(25, 201);
            this.numUDRight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numUDRight.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            131072});
            this.numUDRight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numUDRight.Name = "numUDRight";
            this.numUDRight.Size = new System.Drawing.Size(86, 27);
            this.numUDRight.TabIndex = 11;
            this.numUDRight.Value = new decimal(new int[] {
            80,
            0,
            0,
            131072});
            this.numUDRight.ValueChanged += new System.EventHandler(this.numUDRight_ValueChanged);
            // 
            // panelView
            // 
            this.panelView.Controls.Add(this.radioButton11);
            this.panelView.Controls.Add(this.radioButtonFit);
            this.panelView.Controls.Add(this.label1);
            this.panelView.Location = new System.Drawing.Point(9, 287);
            this.panelView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelView.Name = "panelView";
            this.panelView.Size = new System.Drawing.Size(102, 91);
            this.panelView.TabIndex = 10;
            // 
            // radioButton11
            // 
            this.radioButton11.AutoSize = true;
            this.radioButton11.Location = new System.Drawing.Point(18, 29);
            this.radioButton11.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton11.Name = "radioButton11";
            this.radioButton11.Size = new System.Drawing.Size(49, 24);
            this.radioButton11.TabIndex = 2;
            this.radioButton11.TabStop = true;
            this.radioButton11.Text = "1:1";
            this.radioButton11.UseVisualStyleBackColor = true;
            this.radioButton11.CheckedChanged += new System.EventHandler(this.radioButton11_CheckedChanged);
            // 
            // radioButtonFit
            // 
            this.radioButtonFit.AutoSize = true;
            this.radioButtonFit.Location = new System.Drawing.Point(18, 63);
            this.radioButtonFit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButtonFit.Name = "radioButtonFit";
            this.radioButtonFit.Size = new System.Drawing.Size(46, 24);
            this.radioButtonFit.TabIndex = 3;
            this.radioButtonFit.TabStop = true;
            this.radioButtonFit.Text = "Fit";
            this.radioButtonFit.UseVisualStyleBackColor = true;
            this.radioButtonFit.CheckedChanged += new System.EventHandler(this.radioButtonFit_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "View";
            // 
            // butFlow
            // 
            this.butFlow.Location = new System.Drawing.Point(25, 49);
            this.butFlow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.butFlow.Name = "butFlow";
            this.butFlow.Size = new System.Drawing.Size(97, 31);
            this.butFlow.TabIndex = 9;
            this.butFlow.Text = "Stop stream";
            this.butFlow.UseVisualStyleBackColor = true;
            this.butFlow.Click += new System.EventHandler(this.butFlow_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Left";
            // 
            // numUDLeft
            // 
            this.numUDLeft.DecimalPlaces = 2;
            this.numUDLeft.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numUDLeft.Location = new System.Drawing.Point(25, 147);
            this.numUDLeft.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numUDLeft.Maximum = new decimal(new int[] {
            95,
            0,
            0,
            131072});
            this.numUDLeft.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numUDLeft.Name = "numUDLeft";
            this.numUDLeft.Size = new System.Drawing.Size(86, 27);
            this.numUDLeft.TabIndex = 5;
            this.numUDLeft.Value = new decimal(new int[] {
            57,
            0,
            0,
            131072});
            this.numUDLeft.ValueChanged += new System.EventHandler(this.numUDLeft_ValueChanged);
            // 
            // butOpenFile
            // 
            this.butOpenFile.Location = new System.Drawing.Point(25, 88);
            this.butOpenFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.butOpenFile.Name = "butOpenFile";
            this.butOpenFile.Size = new System.Drawing.Size(97, 31);
            this.butOpenFile.TabIndex = 0;
            this.butOpenFile.Text = "Open file";
            this.butOpenFile.UseVisualStyleBackColor = true;
            this.butOpenFile.Click += new System.EventHandler(this.butOpenFile_Click);
            // 
            // panelGraph
            // 
            this.panelGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGraph.Location = new System.Drawing.Point(265, 4);
            this.panelGraph.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelGraph.Name = "panelGraph";
            this.panelGraph.Size = new System.Drawing.Size(1157, 356);
            this.panelGraph.TabIndex = 1;
            // 
            // trackBarAmp
            // 
            this.trackBarAmp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBarAmp.Location = new System.Drawing.Point(1428, 4);
            this.trackBarAmp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.trackBarAmp.Minimum = -10;
            this.trackBarAmp.Name = "trackBarAmp";
            this.trackBarAmp.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarAmp.Size = new System.Drawing.Size(57, 356);
            this.trackBarAmp.TabIndex = 4;
            this.trackBarAmp.ValueChanged += new System.EventHandler(this.trackBarAmp_ValueChanged);
            // 
            // panelBottom
            // 
            this.panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBottom.Controls.Add(this.panelHRV);
            this.panelBottom.Controls.Add(this.labNumOfWaves);
            this.panelBottom.Controls.Add(this.labY2);
            this.panelBottom.Controls.Add(this.labY1);
            this.panelBottom.Controls.Add(this.labY0);
            this.panelBottom.Controls.Add(this.labMaxSize);
            this.panelBottom.Controls.Add(this.labPulse);
            this.panelBottom.Controls.Add(this.labDia);
            this.panelBottom.Controls.Add(this.labSys);
            this.panelBottom.Controls.Add(this.labMeanPressure);
            this.panelBottom.Controls.Add(this.labCurrentPressure);
            this.panelBottom.Controls.Add(this.labPort);
            this.panelBottom.Controls.Add(this.labCompressionRatio);
            this.panelBottom.Controls.Add(this.labelX);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottom.Location = new System.Drawing.Point(265, 584);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1157, 232);
            this.panelBottom.TabIndex = 3;
            // 
            // panelHRV
            // 
            this.panelHRV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHRV.Controls.Add(this.labAMo);
            this.panelHRV.Controls.Add(this.labSDNN);
            this.panelHRV.Location = new System.Drawing.Point(787, 25);
            this.panelHRV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelHRV.Name = "panelHRV";
            this.panelHRV.Size = new System.Drawing.Size(310, 179);
            this.panelHRV.TabIndex = 16;
            // 
            // labAMo
            // 
            this.labAMo.AutoSize = true;
            this.labAMo.Location = new System.Drawing.Point(3, 36);
            this.labAMo.Name = "labAMo";
            this.labAMo.Size = new System.Drawing.Size(93, 20);
            this.labAMo.TabIndex = 1;
            this.labAMo.Text = "Mode amp : ";
            // 
            // labSDNN
            // 
            this.labSDNN.AutoSize = true;
            this.labSDNN.Location = new System.Drawing.Point(3, 7);
            this.labSDNN.Name = "labSDNN";
            this.labSDNN.Size = new System.Drawing.Size(61, 20);
            this.labSDNN.TabIndex = 0;
            this.labSDNN.Text = "SDNN : ";
            // 
            // labNumOfWaves
            // 
            this.labNumOfWaves.AutoSize = true;
            this.labNumOfWaves.Location = new System.Drawing.Point(374, 13);
            this.labNumOfWaves.Name = "labNumOfWaves";
            this.labNumOfWaves.Size = new System.Drawing.Size(125, 20);
            this.labNumOfWaves.TabIndex = 15;
            this.labNumOfWaves.Text = "Waves detected : ";
            // 
            // labY2
            // 
            this.labY2.AutoSize = true;
            this.labY2.Location = new System.Drawing.Point(422, 139);
            this.labY2.Name = "labY2";
            this.labY2.Size = new System.Drawing.Size(64, 20);
            this.labY2.TabIndex = 14;
            this.labY2.Text = "DCArray";
            // 
            // labY1
            // 
            this.labY1.AutoSize = true;
            this.labY1.Location = new System.Drawing.Point(409, 112);
            this.labY1.Name = "labY1";
            this.labY1.Size = new System.Drawing.Size(79, 20);
            this.labY1.TabIndex = 13;
            this.labY1.Text = "DerivArray";
            // 
            // labY0
            // 
            this.labY0.AutoSize = true;
            this.labY0.Location = new System.Drawing.Point(361, 88);
            this.labY0.Name = "labY0";
            this.labY0.Size = new System.Drawing.Size(137, 20);
            this.labY0.TabIndex = 12;
            this.labY0.Text = "PressureViewArray :";
            // 
            // labMaxSize
            // 
            this.labMaxSize.AutoSize = true;
            this.labMaxSize.Location = new System.Drawing.Point(15, 183);
            this.labMaxSize.Name = "labMaxSize";
            this.labMaxSize.Size = new System.Drawing.Size(50, 20);
            this.labMaxSize.TabIndex = 11;
            this.labMaxSize.Text = "label4";
            // 
            // labPulse
            // 
            this.labPulse.AutoSize = true;
            this.labPulse.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labPulse.Location = new System.Drawing.Point(216, 167);
            this.labPulse.Name = "labPulse";
            this.labPulse.Size = new System.Drawing.Size(99, 37);
            this.labPulse.TabIndex = 10;
            this.labPulse.Text = "Pulse : ";
            // 
            // labDia
            // 
            this.labDia.AutoSize = true;
            this.labDia.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labDia.Location = new System.Drawing.Point(237, 127);
            this.labDia.Name = "labDia";
            this.labDia.Size = new System.Drawing.Size(70, 37);
            this.labDia.TabIndex = 9;
            this.labDia.Text = "Dia :";
            // 
            // labSys
            // 
            this.labSys.AutoSize = true;
            this.labSys.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labSys.Location = new System.Drawing.Point(237, 88);
            this.labSys.Name = "labSys";
            this.labSys.Size = new System.Drawing.Size(74, 37);
            this.labSys.TabIndex = 8;
            this.labSys.Text = "Sys : ";
            // 
            // labMeanPressure
            // 
            this.labMeanPressure.AutoSize = true;
            this.labMeanPressure.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labMeanPressure.Location = new System.Drawing.Point(209, 48);
            this.labMeanPressure.Name = "labMeanPressure";
            this.labMeanPressure.Size = new System.Drawing.Size(104, 37);
            this.labMeanPressure.TabIndex = 7;
            this.labMeanPressure.Text = "Mean : ";
            // 
            // labCurrentPressure
            // 
            this.labCurrentPressure.AutoSize = true;
            this.labCurrentPressure.CausesValidation = false;
            this.labCurrentPressure.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labCurrentPressure.Location = new System.Drawing.Point(190, 8);
            this.labCurrentPressure.Name = "labCurrentPressure";
            this.labCurrentPressure.Size = new System.Drawing.Size(125, 37);
            this.labCurrentPressure.TabIndex = 5;
            this.labCurrentPressure.Text = "Current : ";
            // 
            // labPort
            // 
            this.labPort.AutoSize = true;
            this.labPort.Location = new System.Drawing.Point(15, 115);
            this.labPort.Name = "labPort";
            this.labPort.Size = new System.Drawing.Size(46, 20);
            this.labPort.TabIndex = 4;
            this.labPort.Text = "Port : ";
            // 
            // labCompressionRatio
            // 
            this.labCompressionRatio.AutoSize = true;
            this.labCompressionRatio.Location = new System.Drawing.Point(15, 33);
            this.labCompressionRatio.Name = "labCompressionRatio";
            this.labCompressionRatio.Size = new System.Drawing.Size(114, 20);
            this.labCompressionRatio.TabIndex = 3;
            this.labCompressionRatio.Text = "Compression : 1";
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(463, 68);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(45, 20);
            this.labelX.TabIndex = 2;
            this.labelX.Text = "X : 0  ";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.hScrollBar1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(265, 368);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1157, 35);
            this.panel2.TabIndex = 5;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 0);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(1155, 33);
            this.hScrollBar1.TabIndex = 0;
            this.hScrollBar1.ValueChanged += new System.EventHandler(this.hScrollBar1_ValueChanged);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.progressBarRecord);
            this.panel3.Controls.Add(this.butSaveFile);
            this.panel3.Controls.Add(this.labRecordSize);
            this.panel3.Controls.Add(this.butStopRecord);
            this.panel3.Controls.Add(this.butStartRecord);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 584);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(256, 232);
            this.panel3.TabIndex = 6;
            // 
            // progressBarRecord
            // 
            this.progressBarRecord.Location = new System.Drawing.Point(25, 92);
            this.progressBarRecord.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.progressBarRecord.Name = "progressBarRecord";
            this.progressBarRecord.Size = new System.Drawing.Size(97, 31);
            this.progressBarRecord.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarRecord.TabIndex = 4;
            this.progressBarRecord.Visible = false;
            // 
            // butSaveFile
            // 
            this.butSaveFile.Location = new System.Drawing.Point(25, 151);
            this.butSaveFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.butSaveFile.Name = "butSaveFile";
            this.butSaveFile.Size = new System.Drawing.Size(97, 31);
            this.butSaveFile.TabIndex = 3;
            this.butSaveFile.Text = "Save file";
            this.butSaveFile.UseVisualStyleBackColor = true;
            this.butSaveFile.Click += new System.EventHandler(this.butSaveFile_Click);
            // 
            // labRecordSize
            // 
            this.labRecordSize.AutoSize = true;
            this.labRecordSize.Location = new System.Drawing.Point(27, 127);
            this.labRecordSize.Name = "labRecordSize";
            this.labRecordSize.Size = new System.Drawing.Size(92, 20);
            this.labRecordSize.TabIndex = 2;
            this.labRecordSize.Text = "Record size :";
            // 
            // butStopRecord
            // 
            this.butStopRecord.Location = new System.Drawing.Point(25, 47);
            this.butStopRecord.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.butStopRecord.Name = "butStopRecord";
            this.butStopRecord.Size = new System.Drawing.Size(97, 31);
            this.butStopRecord.TabIndex = 1;
            this.butStopRecord.Text = "Stop record";
            this.butStopRecord.UseVisualStyleBackColor = true;
            this.butStopRecord.Click += new System.EventHandler(this.butStopRecord_Click);
            // 
            // butStartRecord
            // 
            this.butStartRecord.Location = new System.Drawing.Point(25, 8);
            this.butStartRecord.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.butStartRecord.Name = "butStartRecord";
            this.butStartRecord.Size = new System.Drawing.Size(97, 31);
            this.butStartRecord.TabIndex = 0;
            this.butStartRecord.Text = "Start record";
            this.butStartRecord.UseVisualStyleBackColor = true;
            this.butStartRecord.Click += new System.EventHandler(this.butStartRecord_Click);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.histoPanel);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(265, 411);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1157, 165);
            this.panel4.TabIndex = 7;
            // 
            // histoPanel
            // 
            this.histoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.histoPanel.Location = new System.Drawing.Point(0, 0);
            this.histoPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.histoPanel.Name = "histoPanel";
            this.histoPanel.Size = new System.Drawing.Size(1155, 163);
            this.histoPanel.TabIndex = 17;
            this.histoPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panelHisto_Paint);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "txt";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            // 
            // timerRead
            // 
            this.timerRead.Enabled = true;
            this.timerRead.Interval = 50;
            this.timerRead.Tick += new System.EventHandler(this.timerRead_Tick_1);
            // 
            // timerStatus
            // 
            this.timerStatus.Enabled = true;
            this.timerStatus.Interval = 200;
            this.timerStatus.Tick += new System.EventHandler(this.timerStatus_Tick);
            // 
            // timerPaint
            // 
            this.timerPaint.Enabled = true;
            this.timerPaint.Tick += new System.EventHandler(this.timerPaint_Tick);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "txt";
            this.saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1488, 820);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRight)).EndInit();
            this.panelView.ResumeLayout(false);
            this.panelView.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAmp)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.panelHRV.ResumeLayout(false);
            this.panelHRV.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Button butOpenFile;
        private Panel panelGraph;
        private HScrollBar hScrollBar1;
        private RadioButton radioButtonFit;
        private RadioButton radioButton11;
        private OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timerRead;
        private Label labelX;
        private Panel panelBottom;
        private Label labCompressionRatio;
        private Label label1;
        private TrackBar trackBarAmp;
        private Label label2;
        private NumericUpDown numUDLeft;
        private Panel panel2;
        private Panel panel3;
        private Button butStopRecord;
        private Button butStartRecord;
        private Label labPort;
        private System.Windows.Forms.Timer timerStatus;
        private System.Windows.Forms.Timer timerPaint;
        private Label labCurrentPressure;
        private Button butFlow;
        private Label labRecordSize;
        private Button butSaveFile;
        private SaveFileDialog saveFileDialog1;
        private ProgressBar progressBarRecord;
        private Label labMeanPressure;
        private Label labDia;
        private Label labSys;
        private Label labPulse;
        private Panel panelView;
        private Label labMaxSize;
        private Label labY0;
        private Label labY1;
        private Label labY2;
        private Label label3;
        private NumericUpDown numUDRight;
        private Button butRefresh;
        private Label labNumOfWaves;
        private Panel panel4;
        private Panel histoPanel;
        private Panel panelHRV;
        private Label labSDNN;
        private Label labAMo;
    }
}