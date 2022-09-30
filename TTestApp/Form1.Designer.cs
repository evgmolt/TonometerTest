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
            this.butFlow = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numUDLeft = new System.Windows.Forms.NumericUpDown();
            this.butOpenFile = new System.Windows.Forms.Button();
            this.panelGraph = new System.Windows.Forms.Panel();
            this.trackBarAmp = new System.Windows.Forms.TrackBar();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.labHeart = new System.Windows.Forms.Label();
            this.labelRate = new System.Windows.Forms.Label();
            this.labArrythmia = new System.Windows.Forms.Label();
            this.labStopPumpingReason = new System.Windows.Forms.Label();
            this.labMeasStatus = new System.Windows.Forms.Label();
            this.labPumpStatus = new System.Windows.Forms.Label();
            this.labDeviceIsOff = new System.Windows.Forms.Label();
            this.labNumOfWaves = new System.Windows.Forms.Label();
            this.labY2 = new System.Windows.Forms.Label();
            this.labY1 = new System.Windows.Forms.Label();
            this.labY0 = new System.Windows.Forms.Label();
            this.labPulse = new System.Windows.Forms.Label();
            this.labDia = new System.Windows.Forms.Label();
            this.labSys = new System.Windows.Forms.Label();
            this.labMeanPressure = new System.Windows.Forms.Label();
            this.labCurrentPressure = new System.Windows.Forms.Label();
            this.labPort = new System.Windows.Forms.Label();
            this.labelX = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.progressBarRecord = new System.Windows.Forms.ProgressBar();
            this.butSaveFile = new System.Windows.Forms.Button();
            this.labRecordSize = new System.Windows.Forms.Label();
            this.butStopRecord = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.butPumpOff = new System.Windows.Forms.Button();
            this.butPumpOn = new System.Windows.Forms.Button();
            this.labPump = new System.Windows.Forms.Label();
            this.butValveFastClose = new System.Windows.Forms.Button();
            this.butValveFastOpen = new System.Windows.Forms.Button();
            this.butValveSlowClose = new System.Windows.Forms.Button();
            this.butValveSlowOpen = new System.Windows.Forms.Button();
            this.labValve1 = new System.Windows.Forms.Label();
            this.labValve2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.labMeasInProgress = new System.Windows.Forms.Label();
            this.butPressureMeasAbort = new System.Windows.Forms.Button();
            this.butStartRecord = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timerRead = new System.Windows.Forms.Timer(this.components);
            this.timerStatus = new System.Windows.Forms.Timer(this.components);
            this.timerPaint = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.timerDetectRate = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRight)).BeginInit();
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
            this.butRefresh.Location = new System.Drawing.Point(22, 188);
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
            this.label3.Size = new System.Drawing.Size(65, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "Right (DIA)";
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
            95,
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
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Left (SYS)";
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
            this.panelBottom.Controls.Add(this.labHeart);
            this.panelBottom.Controls.Add(this.labelRate);
            this.panelBottom.Controls.Add(this.labArrythmia);
            this.panelBottom.Controls.Add(this.labStopPumpingReason);
            this.panelBottom.Controls.Add(this.labMeasStatus);
            this.panelBottom.Controls.Add(this.labPumpStatus);
            this.panelBottom.Controls.Add(this.labDeviceIsOff);
            this.panelBottom.Controls.Add(this.labNumOfWaves);
            this.panelBottom.Controls.Add(this.labY2);
            this.panelBottom.Controls.Add(this.labY1);
            this.panelBottom.Controls.Add(this.labY0);
            this.panelBottom.Controls.Add(this.labPulse);
            this.panelBottom.Controls.Add(this.labDia);
            this.panelBottom.Controls.Add(this.labSys);
            this.panelBottom.Controls.Add(this.labMeanPressure);
            this.panelBottom.Controls.Add(this.labCurrentPressure);
            this.panelBottom.Controls.Add(this.labPort);
            this.panelBottom.Controls.Add(this.labelX);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottom.Location = new System.Drawing.Point(233, 409);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1014, 203);
            this.panelBottom.TabIndex = 3;
            // 
            // labHeart
            // 
            this.labHeart.AutoSize = true;
            this.labHeart.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labHeart.ForeColor = System.Drawing.Color.Red;
            this.labHeart.Location = new System.Drawing.Point(967, 119);
            this.labHeart.Name = "labHeart";
            this.labHeart.Size = new System.Drawing.Size(42, 45);
            this.labHeart.TabIndex = 23;
            this.labHeart.Text = "♥";
            this.labHeart.Visible = false;
            // 
            // labelRate
            // 
            this.labelRate.AutoSize = true;
            this.labelRate.Location = new System.Drawing.Point(13, 6);
            this.labelRate.Name = "labelRate";
            this.labelRate.Size = new System.Drawing.Size(55, 15);
            this.labelRate.TabIndex = 22;
            this.labelRate.Text = "labelRate";
            // 
            // labArrythmia
            // 
            this.labArrythmia.AutoSize = true;
            this.labArrythmia.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labArrythmia.Location = new System.Drawing.Point(981, 15);
            this.labArrythmia.Name = "labArrythmia";
            this.labArrythmia.Size = new System.Drawing.Size(19, 21);
            this.labArrythmia.TabIndex = 21;
            this.labArrythmia.Text = "0";
            // 
            // labStopPumpingReason
            // 
            this.labStopPumpingReason.AutoSize = true;
            this.labStopPumpingReason.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labStopPumpingReason.Location = new System.Drawing.Point(772, 50);
            this.labStopPumpingReason.Name = "labStopPumpingReason";
            this.labStopPumpingReason.Size = new System.Drawing.Size(38, 21);
            this.labStopPumpingReason.TabIndex = 20;
            this.labStopPumpingReason.Text = "test";
            // 
            // labMeasStatus
            // 
            this.labMeasStatus.AutoSize = true;
            this.labMeasStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labMeasStatus.Location = new System.Drawing.Point(474, 50);
            this.labMeasStatus.Name = "labMeasStatus";
            this.labMeasStatus.Size = new System.Drawing.Size(223, 21);
            this.labMeasStatus.TabIndex = 19;
            this.labMeasStatus.Text = "Measurement status : Ready";
            // 
            // labPumpStatus
            // 
            this.labPumpStatus.AutoSize = true;
            this.labPumpStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labPumpStatus.Location = new System.Drawing.Point(509, 29);
            this.labPumpStatus.Name = "labPumpStatus";
            this.labPumpStatus.Size = new System.Drawing.Size(188, 21);
            this.labPumpStatus.TabIndex = 18;
            this.labPumpStatus.Text = "Pumping status : Ready";
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
            this.labNumOfWaves.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labNumOfWaves.Location = new System.Drawing.Point(509, 8);
            this.labNumOfWaves.Name = "labNumOfWaves";
            this.labNumOfWaves.Size = new System.Drawing.Size(142, 21);
            this.labNumOfWaves.TabIndex = 15;
            this.labNumOfWaves.Text = "Waves detected : ";
            // 
            // labY2
            // 
            this.labY2.AutoSize = true;
            this.labY2.Location = new System.Drawing.Point(580, 163);
            this.labY2.Name = "labY2";
            this.labY2.Size = new System.Drawing.Size(51, 15);
            this.labY2.TabIndex = 14;
            this.labY2.Text = "DCArray";
            // 
            // labY1
            // 
            this.labY1.AutoSize = true;
            this.labY1.Location = new System.Drawing.Point(569, 143);
            this.labY1.Name = "labY1";
            this.labY1.Size = new System.Drawing.Size(62, 15);
            this.labY1.TabIndex = 13;
            this.labY1.Text = "DerivArray";
            // 
            // labY0
            // 
            this.labY0.AutoSize = true;
            this.labY0.Location = new System.Drawing.Point(527, 125);
            this.labY0.Name = "labY0";
            this.labY0.Size = new System.Drawing.Size(110, 15);
            this.labY0.TabIndex = 12;
            this.labY0.Text = "PressureViewArray :";
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
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(616, 110);
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
            this.controlPanel.Controls.Add(this.butPumpOff);
            this.controlPanel.Controls.Add(this.butPumpOn);
            this.controlPanel.Controls.Add(this.labPump);
            this.controlPanel.Controls.Add(this.butValveFastClose);
            this.controlPanel.Controls.Add(this.butValveFastOpen);
            this.controlPanel.Controls.Add(this.butValveSlowClose);
            this.controlPanel.Controls.Add(this.butValveSlowOpen);
            this.controlPanel.Controls.Add(this.labValve1);
            this.controlPanel.Controls.Add(this.labValve2);
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlPanel.Location = new System.Drawing.Point(0, 0);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(1012, 93);
            this.controlPanel.TabIndex = 17;
            // 
            // butPumpOff
            // 
            this.butPumpOff.Location = new System.Drawing.Point(643, 21);
            this.butPumpOff.Name = "butPumpOff";
            this.butPumpOff.Size = new System.Drawing.Size(75, 23);
            this.butPumpOff.TabIndex = 9;
            this.butPumpOff.Text = "Pump Off";
            this.butPumpOff.UseVisualStyleBackColor = true;
            this.butPumpOff.Click += new System.EventHandler(this.butPumpOff_Click);
            // 
            // butPumpOn
            // 
            this.butPumpOn.Location = new System.Drawing.Point(562, 21);
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
            this.labPump.Location = new System.Drawing.Point(460, 25);
            this.labPump.Name = "labPump";
            this.labPump.Size = new System.Drawing.Size(48, 15);
            this.labPump.TabIndex = 6;
            this.labPump.Text = "Pump : ";
            // 
            // butValveFastClose
            // 
            this.butValveFastClose.Location = new System.Drawing.Point(259, 50);
            this.butValveFastClose.Name = "butValveFastClose";
            this.butValveFastClose.Size = new System.Drawing.Size(75, 23);
            this.butValveFastClose.TabIndex = 5;
            this.butValveFastClose.Text = "Close";
            this.butValveFastClose.UseVisualStyleBackColor = true;
            this.butValveFastClose.Click += new System.EventHandler(this.butValve2Close_Click);
            // 
            // butValveFastOpen
            // 
            this.butValveFastOpen.Location = new System.Drawing.Point(178, 50);
            this.butValveFastOpen.Name = "butValveFastOpen";
            this.butValveFastOpen.Size = new System.Drawing.Size(75, 23);
            this.butValveFastOpen.TabIndex = 4;
            this.butValveFastOpen.Text = "Open";
            this.butValveFastOpen.UseVisualStyleBackColor = true;
            this.butValveFastOpen.Click += new System.EventHandler(this.butValve2Open_Click);
            // 
            // butValveSlowClose
            // 
            this.butValveSlowClose.Location = new System.Drawing.Point(259, 21);
            this.butValveSlowClose.Name = "butValveSlowClose";
            this.butValveSlowClose.Size = new System.Drawing.Size(75, 23);
            this.butValveSlowClose.TabIndex = 3;
            this.butValveSlowClose.Text = "Close";
            this.butValveSlowClose.UseVisualStyleBackColor = true;
            this.butValveSlowClose.Click += new System.EventHandler(this.butValve1Close_Click);
            // 
            // butValveSlowOpen
            // 
            this.butValveSlowOpen.Location = new System.Drawing.Point(178, 21);
            this.butValveSlowOpen.Name = "butValveSlowOpen";
            this.butValveSlowOpen.Size = new System.Drawing.Size(75, 23);
            this.butValveSlowOpen.TabIndex = 2;
            this.butValveSlowOpen.Text = "Open";
            this.butValveSlowOpen.UseVisualStyleBackColor = true;
            this.butValveSlowOpen.Click += new System.EventHandler(this.butValve1Open_Click);
            // 
            // labValve1
            // 
            this.labValve1.AutoSize = true;
            this.labValve1.Location = new System.Drawing.Point(12, 29);
            this.labValve1.Name = "labValve1";
            this.labValve1.Size = new System.Drawing.Size(88, 15);
            this.labValve1.TabIndex = 1;
            this.labValve1.Text = "Valve 1 (Slow) : ";
            // 
            // labValve2
            // 
            this.labValve2.AutoSize = true;
            this.labValve2.Location = new System.Drawing.Point(12, 55);
            this.labValve2.Name = "labValve2";
            this.labValve2.Size = new System.Drawing.Size(84, 15);
            this.labValve2.TabIndex = 0;
            this.labValve2.Text = "Valve 2 (Fast) : ";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.button2);
            this.panel5.Controls.Add(this.button1);
            this.panel5.Controls.Add(this.labMeasInProgress);
            this.panel5.Controls.Add(this.butPressureMeasAbort);
            this.panel5.Controls.Add(this.butStartRecord);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 307);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(224, 97);
            this.panel5.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(130, 51);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Stop read";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(130, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Start read";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            // butStartRecord
            // 
            this.butStartRecord.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.butStartRecord.Location = new System.Drawing.Point(23, 23);
            this.butStartRecord.Name = "butStartRecord";
            this.butStartRecord.Size = new System.Drawing.Size(85, 23);
            this.butStartRecord.TabIndex = 0;
            this.butStartRecord.Text = "Start";
            this.butStartRecord.UseVisualStyleBackColor = true;
            this.butStartRecord.Click += new System.EventHandler(this.butStartRecord_Click);
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
            // timerDetectRate
            // 
            this.timerDetectRate.Interval = 10000;
            this.timerDetectRate.Tick += new System.EventHandler(this.timerDetectRate_Tick);
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
        private OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timerRead;
        private Label labelX;
        private Panel panelBottom;
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
        private Label label4;
        private Label labMeasInProgress;
        private Panel controlPanel;
        private Button butPumpOff;
        private Button butPumpOn;
        private Label labPump;
        private Button butValveFastClose;
        private Button butValveFastOpen;
        private Button butValveSlowClose;
        private Button butValveSlowOpen;
        private Label labValve1;
        private Label labValve2;
        private Label labPumpStatus;
        private Label labMeasStatus;
        private Label labStopPumpingReason;
        private Label labArrythmia;
        private System.Windows.Forms.Timer timerDetectRate;
        private Label labelRate;
        private Button button1;
        private Button button2;
        private Label labHeart;
    }
}