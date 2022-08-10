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
            this.labDeviceIsOff = new System.Windows.Forms.Label();
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
            this.controlPanel = new System.Windows.Forms.Panel();
            this.butValve1PWM = new System.Windows.Forms.Button();
            this.butPumpOff = new System.Windows.Forms.Button();
            this.butPumpOn = new System.Windows.Forms.Button();
            this.labPump = new System.Windows.Forms.Label();
            this.butValve2Close = new System.Windows.Forms.Button();
            this.butValve2Open = new System.Windows.Forms.Button();
            this.butValve1Close = new System.Windows.Forms.Button();
            this.butValve1Open = new System.Windows.Forms.Button();
            this.labValve2 = new System.Windows.Forms.Label();
            this.labValve1 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.labMeasInProgress = new System.Windows.Forms.Label();
            this.butPressureMeasAbort = new System.Windows.Forms.Button();
            this.butPressureMeasStart = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
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
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.controlPanel.SuspendLayout();
            this.panel5.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 209F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1302, 615);
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
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(224, 267);
            this.panel1.TabIndex = 0;
            // 
            // butRefresh
            // 
            this.butRefresh.Location = new System.Drawing.Point(22, 179);
            this.butRefresh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.butRefresh.Name = "butRefresh";
            this.butRefresh.Size = new System.Drawing.Size(85, 22);
            this.butRefresh.TabIndex = 13;
            this.butRefresh.Text = "Refresh";
            this.butRefresh.UseVisualStyleBackColor = true;
            this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 15);
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
            this.numUDRight.Location = new System.Drawing.Point(22, 151);
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
            this.numUDRight.Size = new System.Drawing.Size(75, 23);
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
            this.panelView.Location = new System.Drawing.Point(8, 215);
            this.panelView.Name = "panelView";
            this.panelView.Size = new System.Drawing.Size(89, 68);
            this.panelView.TabIndex = 10;
            // 
            // radioButton11
            // 
            this.radioButton11.AutoSize = true;
            this.radioButton11.Location = new System.Drawing.Point(16, 22);
            this.radioButton11.Name = "radioButton11";
            this.radioButton11.Size = new System.Drawing.Size(40, 19);
            this.radioButton11.TabIndex = 2;
            this.radioButton11.TabStop = true;
            this.radioButton11.Text = "1:1";
            this.radioButton11.UseVisualStyleBackColor = true;
            this.radioButton11.CheckedChanged += new System.EventHandler(this.radioButton11_CheckedChanged);
            // 
            // radioButtonFit
            // 
            this.radioButtonFit.AutoSize = true;
            this.radioButtonFit.Location = new System.Drawing.Point(16, 47);
            this.radioButtonFit.Name = "radioButtonFit";
            this.radioButtonFit.Size = new System.Drawing.Size(38, 19);
            this.radioButtonFit.TabIndex = 3;
            this.radioButtonFit.TabStop = true;
            this.radioButtonFit.Text = "Fit";
            this.radioButtonFit.UseVisualStyleBackColor = true;
            this.radioButtonFit.CheckedChanged += new System.EventHandler(this.radioButtonFit_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "View";
            // 
            // butFlow
            // 
            this.butFlow.Location = new System.Drawing.Point(22, 37);
            this.butFlow.Name = "butFlow";
            this.butFlow.Size = new System.Drawing.Size(85, 23);
            this.butFlow.TabIndex = 0;
            this.butFlow.Text = "Stop stream";
            this.butFlow.UseVisualStyleBackColor = true;
            this.butFlow.Click += new System.EventHandler(this.butFlow_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 15);
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
            this.numUDLeft.Location = new System.Drawing.Point(22, 110);
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
            this.numUDLeft.Size = new System.Drawing.Size(75, 23);
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
            this.butOpenFile.Location = new System.Drawing.Point(22, 66);
            this.butOpenFile.Name = "butOpenFile";
            this.butOpenFile.Size = new System.Drawing.Size(85, 23);
            this.butOpenFile.TabIndex = 0;
            this.butOpenFile.Text = "Open file";
            this.butOpenFile.UseVisualStyleBackColor = true;
            this.butOpenFile.Click += new System.EventHandler(this.butOpenFile_Click);
            // 
            // panelGraph
            // 
            this.panelGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGraph.Location = new System.Drawing.Point(233, 3);
            this.panelGraph.Name = "panelGraph";
            this.panelGraph.Size = new System.Drawing.Size(1014, 267);
            this.panelGraph.TabIndex = 1;
            // 
            // trackBarAmp
            // 
            this.trackBarAmp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBarAmp.Location = new System.Drawing.Point(1253, 3);
            this.trackBarAmp.Minimum = -10;
            this.trackBarAmp.Name = "trackBarAmp";
            this.trackBarAmp.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarAmp.Size = new System.Drawing.Size(46, 267);
            this.trackBarAmp.TabIndex = 4;
            this.trackBarAmp.ValueChanged += new System.EventHandler(this.trackBarAmp_ValueChanged);
            // 
            // panelBottom
            // 
            this.panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBottom.Controls.Add(this.labDeviceIsOff);
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
            this.panelBottom.Location = new System.Drawing.Point(233, 409);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1014, 203);
            this.panelBottom.TabIndex = 3;
            // 
            // labDeviceIsOff
            // 
            this.labDeviceIsOff.AutoSize = true;
            this.labDeviceIsOff.ForeColor = System.Drawing.Color.Red;
            this.labDeviceIsOff.Location = new System.Drawing.Point(13, 107);
            this.labDeviceIsOff.Name = "labDeviceIsOff";
            this.labDeviceIsOff.Size = new System.Drawing.Size(98, 15);
            this.labDeviceIsOff.TabIndex = 17;
            this.labDeviceIsOff.Text = "Device turned off";
            this.labDeviceIsOff.Visible = false;
            // 
            // labNumOfWaves
            // 
            this.labNumOfWaves.AutoSize = true;
            this.labNumOfWaves.Location = new System.Drawing.Point(327, 10);
            this.labNumOfWaves.Name = "labNumOfWaves";
            this.labNumOfWaves.Size = new System.Drawing.Size(99, 15);
            this.labNumOfWaves.TabIndex = 15;
            this.labNumOfWaves.Text = "Waves detected : ";
            // 
            // labY2
            // 
            this.labY2.AutoSize = true;
            this.labY2.Location = new System.Drawing.Point(369, 104);
            this.labY2.Name = "labY2";
            this.labY2.Size = new System.Drawing.Size(51, 15);
            this.labY2.TabIndex = 14;
            this.labY2.Text = "DCArray";
            // 
            // labY1
            // 
            this.labY1.AutoSize = true;
            this.labY1.Location = new System.Drawing.Point(358, 84);
            this.labY1.Name = "labY1";
            this.labY1.Size = new System.Drawing.Size(62, 15);
            this.labY1.TabIndex = 13;
            this.labY1.Text = "DerivArray";
            // 
            // labY0
            // 
            this.labY0.AutoSize = true;
            this.labY0.Location = new System.Drawing.Point(316, 66);
            this.labY0.Name = "labY0";
            this.labY0.Size = new System.Drawing.Size(110, 15);
            this.labY0.TabIndex = 12;
            this.labY0.Text = "PressureViewArray :";
            // 
            // labMaxSize
            // 
            this.labMaxSize.AutoSize = true;
            this.labMaxSize.Location = new System.Drawing.Point(13, 137);
            this.labMaxSize.Name = "labMaxSize";
            this.labMaxSize.Size = new System.Drawing.Size(38, 15);
            this.labMaxSize.TabIndex = 11;
            this.labMaxSize.Text = "label4";
            // 
            // labPulse
            // 
            this.labPulse.AutoSize = true;
            this.labPulse.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labPulse.Location = new System.Drawing.Point(189, 125);
            this.labPulse.Name = "labPulse";
            this.labPulse.Size = new System.Drawing.Size(80, 30);
            this.labPulse.TabIndex = 10;
            this.labPulse.Text = "Pulse : ";
            // 
            // labDia
            // 
            this.labDia.AutoSize = true;
            this.labDia.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labDia.Location = new System.Drawing.Point(207, 95);
            this.labDia.Name = "labDia";
            this.labDia.Size = new System.Drawing.Size(55, 30);
            this.labDia.TabIndex = 9;
            this.labDia.Text = "Dia :";
            // 
            // labSys
            // 
            this.labSys.AutoSize = true;
            this.labSys.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labSys.Location = new System.Drawing.Point(207, 66);
            this.labSys.Name = "labSys";
            this.labSys.Size = new System.Drawing.Size(61, 30);
            this.labSys.TabIndex = 8;
            this.labSys.Text = "Sys : ";
            // 
            // labMeanPressure
            // 
            this.labMeanPressure.AutoSize = true;
            this.labMeanPressure.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labMeanPressure.Location = new System.Drawing.Point(183, 36);
            this.labMeanPressure.Name = "labMeanPressure";
            this.labMeanPressure.Size = new System.Drawing.Size(85, 30);
            this.labMeanPressure.TabIndex = 7;
            this.labMeanPressure.Text = "Mean : ";
            // 
            // labCurrentPressure
            // 
            this.labCurrentPressure.AutoSize = true;
            this.labCurrentPressure.CausesValidation = false;
            this.labCurrentPressure.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labCurrentPressure.Location = new System.Drawing.Point(166, 6);
            this.labCurrentPressure.Name = "labCurrentPressure";
            this.labCurrentPressure.Size = new System.Drawing.Size(103, 30);
            this.labCurrentPressure.TabIndex = 5;
            this.labCurrentPressure.Text = "Current : ";
            // 
            // labPort
            // 
            this.labPort.AutoSize = true;
            this.labPort.Location = new System.Drawing.Point(13, 86);
            this.labPort.Name = "labPort";
            this.labPort.Size = new System.Drawing.Size(38, 15);
            this.labPort.TabIndex = 4;
            this.labPort.Text = "Port : ";
            // 
            // labCompressionRatio
            // 
            this.labCompressionRatio.AutoSize = true;
            this.labCompressionRatio.Location = new System.Drawing.Point(13, 25);
            this.labCompressionRatio.Name = "labCompressionRatio";
            this.labCompressionRatio.Size = new System.Drawing.Size(92, 15);
            this.labCompressionRatio.TabIndex = 3;
            this.labCompressionRatio.Text = "Compression : 1";
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(405, 51);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(35, 15);
            this.labelX.TabIndex = 2;
            this.labelX.Text = "X : 0  ";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.hScrollBar1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(233, 276);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1014, 26);
            this.panel2.TabIndex = 5;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 0);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(1012, 24);
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
            this.panel3.Location = new System.Drawing.Point(3, 409);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(224, 203);
            this.panel3.TabIndex = 6;
            // 
            // progressBarRecord
            // 
            this.progressBarRecord.Location = new System.Drawing.Point(22, 69);
            this.progressBarRecord.Name = "progressBarRecord";
            this.progressBarRecord.Size = new System.Drawing.Size(85, 23);
            this.progressBarRecord.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarRecord.TabIndex = 4;
            this.progressBarRecord.Visible = false;
            // 
            // butSaveFile
            // 
            this.butSaveFile.Location = new System.Drawing.Point(22, 113);
            this.butSaveFile.Name = "butSaveFile";
            this.butSaveFile.Size = new System.Drawing.Size(85, 23);
            this.butSaveFile.TabIndex = 3;
            this.butSaveFile.Text = "Save file";
            this.butSaveFile.UseVisualStyleBackColor = true;
            this.butSaveFile.Click += new System.EventHandler(this.butSaveFile_Click);
            // 
            // labRecordSize
            // 
            this.labRecordSize.AutoSize = true;
            this.labRecordSize.Location = new System.Drawing.Point(24, 95);
            this.labRecordSize.Name = "labRecordSize";
            this.labRecordSize.Size = new System.Drawing.Size(72, 15);
            this.labRecordSize.TabIndex = 2;
            this.labRecordSize.Text = "Record size :";
            // 
            // butStopRecord
            // 
            this.butStopRecord.Location = new System.Drawing.Point(22, 35);
            this.butStopRecord.Name = "butStopRecord";
            this.butStopRecord.Size = new System.Drawing.Size(85, 23);
            this.butStopRecord.TabIndex = 1;
            this.butStopRecord.Text = "Stop record";
            this.butStopRecord.UseVisualStyleBackColor = true;
            this.butStopRecord.Click += new System.EventHandler(this.butStopRecord_Click);
            // 
            // butStartRecord
            // 
            this.butStartRecord.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.butStartRecord.Location = new System.Drawing.Point(22, 6);
            this.butStartRecord.Name = "butStartRecord";
            this.butStartRecord.Size = new System.Drawing.Size(85, 23);
            this.butStartRecord.TabIndex = 0;
            this.butStartRecord.Text = "Start record";
            this.butStartRecord.UseVisualStyleBackColor = true;
            this.butStartRecord.Click += new System.EventHandler(this.butStartRecord_Click);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.controlPanel);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(233, 308);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1014, 95);
            this.panel4.TabIndex = 7;
            // 
            // controlPanel
            // 
            this.controlPanel.Controls.Add(this.butValve1PWM);
            this.controlPanel.Controls.Add(this.butPumpOff);
            this.controlPanel.Controls.Add(this.butPumpOn);
            this.controlPanel.Controls.Add(this.labPump);
            this.controlPanel.Controls.Add(this.butValve2Close);
            this.controlPanel.Controls.Add(this.butValve2Open);
            this.controlPanel.Controls.Add(this.butValve1Close);
            this.controlPanel.Controls.Add(this.butValve1Open);
            this.controlPanel.Controls.Add(this.labValve2);
            this.controlPanel.Controls.Add(this.labValve1);
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlPanel.Location = new System.Drawing.Point(0, 0);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(1012, 93);
            this.controlPanel.TabIndex = 17;
            // 
            // butValve1PWM
            // 
            this.butValve1PWM.Location = new System.Drawing.Point(280, 13);
            this.butValve1PWM.Name = "butValve1PWM";
            this.butValve1PWM.Size = new System.Drawing.Size(75, 23);
            this.butValve1PWM.TabIndex = 7;
            this.butValve1PWM.Text = "PWM";
            this.butValve1PWM.UseVisualStyleBackColor = true;
            this.butValve1PWM.Click += new System.EventHandler(this.butValve1PWM_Click);
            // 
            // butPumpOff
            // 
            this.butPumpOff.Location = new System.Drawing.Point(643, 13);
            this.butPumpOff.Name = "butPumpOff";
            this.butPumpOff.Size = new System.Drawing.Size(75, 23);
            this.butPumpOff.TabIndex = 9;
            this.butPumpOff.Text = "Pump Off";
            this.butPumpOff.UseVisualStyleBackColor = true;
            this.butPumpOff.Click += new System.EventHandler(this.butPumpOff_Click);
            // 
            // butPumpOn
            // 
            this.butPumpOn.Location = new System.Drawing.Point(562, 13);
            this.butPumpOn.Name = "butPumpOn";
            this.butPumpOn.Size = new System.Drawing.Size(75, 23);
            this.butPumpOn.TabIndex = 8;
            this.butPumpOn.Text = "Pump On";
            this.butPumpOn.UseVisualStyleBackColor = true;
            this.butPumpOn.Click += new System.EventHandler(this.butPumpOn_Click);
            // 
            // labPump
            // 
            this.labPump.AutoSize = true;
            this.labPump.Location = new System.Drawing.Point(460, 17);
            this.labPump.Name = "labPump";
            this.labPump.Size = new System.Drawing.Size(48, 15);
            this.labPump.TabIndex = 6;
            this.labPump.Text = "Pump : ";
            // 
            // butValve2Close
            // 
            this.butValve2Close.Location = new System.Drawing.Point(199, 40);
            this.butValve2Close.Name = "butValve2Close";
            this.butValve2Close.Size = new System.Drawing.Size(75, 23);
            this.butValve2Close.TabIndex = 5;
            this.butValve2Close.Text = "Close";
            this.butValve2Close.UseVisualStyleBackColor = true;
            this.butValve2Close.Click += new System.EventHandler(this.butValve2Close_Click);
            // 
            // butValve2Open
            // 
            this.butValve2Open.Location = new System.Drawing.Point(118, 40);
            this.butValve2Open.Name = "butValve2Open";
            this.butValve2Open.Size = new System.Drawing.Size(75, 23);
            this.butValve2Open.TabIndex = 4;
            this.butValve2Open.Text = "Open";
            this.butValve2Open.UseVisualStyleBackColor = true;
            this.butValve2Open.Click += new System.EventHandler(this.butValve2Open_Click);
            // 
            // butValve1Close
            // 
            this.butValve1Close.Location = new System.Drawing.Point(199, 13);
            this.butValve1Close.Name = "butValve1Close";
            this.butValve1Close.Size = new System.Drawing.Size(75, 23);
            this.butValve1Close.TabIndex = 3;
            this.butValve1Close.Text = "Close";
            this.butValve1Close.UseVisualStyleBackColor = true;
            this.butValve1Close.Click += new System.EventHandler(this.butValve1Close_Click);
            // 
            // butValve1Open
            // 
            this.butValve1Open.Location = new System.Drawing.Point(118, 13);
            this.butValve1Open.Name = "butValve1Open";
            this.butValve1Open.Size = new System.Drawing.Size(75, 23);
            this.butValve1Open.TabIndex = 2;
            this.butValve1Open.Text = "Open";
            this.butValve1Open.UseVisualStyleBackColor = true;
            this.butValve1Open.Click += new System.EventHandler(this.butValve1Open_Click);
            // 
            // labValve2
            // 
            this.labValve2.AutoSize = true;
            this.labValve2.Location = new System.Drawing.Point(13, 48);
            this.labValve2.Name = "labValve2";
            this.labValve2.Size = new System.Drawing.Size(52, 15);
            this.labValve2.TabIndex = 1;
            this.labValve2.Text = "Valve 2 : ";
            // 
            // labValve1
            // 
            this.labValve1.AutoSize = true;
            this.labValve1.Location = new System.Drawing.Point(13, 21);
            this.labValve1.Name = "labValve1";
            this.labValve1.Size = new System.Drawing.Size(52, 15);
            this.labValve1.TabIndex = 0;
            this.labValve1.Text = "Valve 1 : ";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.labMeasInProgress);
            this.panel5.Controls.Add(this.butPressureMeasAbort);
            this.panel5.Controls.Add(this.butPressureMeasStart);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 307);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(224, 97);
            this.panel5.TabIndex = 8;
            // 
            // labMeasInProgress
            // 
            this.labMeasInProgress.AutoSize = true;
            this.labMeasInProgress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labMeasInProgress.ForeColor = System.Drawing.Color.Red;
            this.labMeasInProgress.Location = new System.Drawing.Point(24, 74);
            this.labMeasInProgress.Name = "labMeasInProgress";
            this.labMeasInProgress.Size = new System.Drawing.Size(149, 15);
            this.labMeasInProgress.TabIndex = 3;
            this.labMeasInProgress.Text = "Measurement in progress";
            this.labMeasInProgress.Visible = false;
            // 
            // butPressureMeasAbort
            // 
            this.butPressureMeasAbort.Location = new System.Drawing.Point(23, 50);
            this.butPressureMeasAbort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.butPressureMeasAbort.Name = "butPressureMeasAbort";
            this.butPressureMeasAbort.Size = new System.Drawing.Size(85, 22);
            this.butPressureMeasAbort.TabIndex = 2;
            this.butPressureMeasAbort.Text = "Abort";
            this.butPressureMeasAbort.UseVisualStyleBackColor = true;
            this.butPressureMeasAbort.Click += new System.EventHandler(this.butPressureMeasAbort_Click);
            // 
            // butPressureMeasStart
            // 
            this.butPressureMeasStart.Location = new System.Drawing.Point(23, 23);
            this.butPressureMeasStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.butPressureMeasStart.Name = "butPressureMeasStart";
            this.butPressureMeasStart.Size = new System.Drawing.Size(85, 22);
            this.butPressureMeasStart.TabIndex = 1;
            this.butPressureMeasStart.Text = "Start";
            this.butPressureMeasStart.UseVisualStyleBackColor = true;
            this.butPressureMeasStart.Click += new System.EventHandler(this.butPressureMeasStart_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Pressure measurement";
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
            this.timerRead.Tick += new System.EventHandler(this.timerRead_Tick);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1302, 615);
            this.Controls.Add(this.tableLayoutPanel1);
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
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
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
        private Label labDeviceIsOff;
        private Panel panel5;
        private Button butPressureMeasAbort;
        private Button butPressureMeasStart;
        private Label label4;
        private Label label;
        private Label labMeasInProgress;
        private Panel controlPanel;
        private Button butValve1PWM;
        private Button butPumpOff;
        private Button butPumpOn;
        private Label labPump;
        private Button butValve2Close;
        private Button butValve2Open;
        private Button butValve1Close;
        private Button butValve1Open;
        private Label labValve2;
        private Label labValve1;
    }
}