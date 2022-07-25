﻿namespace TTestApp
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
            this.butBCISetup = new System.Windows.Forms.Button();
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
            this.histoPanel = new System.Windows.Forms.Panel();
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
            this.tableLayoutPanel1.Controls.Add(this.panelBottom, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 178F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(958, 504);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.butBCISetup);
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
            this.panel1.Size = new System.Drawing.Size(161, 295);
            this.panel1.TabIndex = 0;
            // 
            // butBCISetup
            // 
            this.butBCISetup.Location = new System.Drawing.Point(22, 8);
            this.butBCISetup.Name = "butBCISetup";
            this.butBCISetup.Size = new System.Drawing.Size(85, 23);
            this.butBCISetup.TabIndex = 17;
            this.butBCISetup.Text = "BCI setup";
            this.butBCISetup.UseVisualStyleBackColor = true;
            this.butBCISetup.Click += new System.EventHandler(this.butBCISetup_Click);
            // 
            // butRefresh
            // 
            this.butRefresh.Location = new System.Drawing.Point(22, 179);
            this.butRefresh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.butRefresh.Name = "butRefresh";
            this.butRefresh.Size = new System.Drawing.Size(82, 22);
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
            this.butFlow.TabIndex = 9;
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
            this.panelGraph.Location = new System.Drawing.Point(170, 3);
            this.panelGraph.Name = "panelGraph";
            this.panelGraph.Size = new System.Drawing.Size(733, 295);
            this.panelGraph.TabIndex = 1;
            // 
            // trackBarAmp
            // 
            this.trackBarAmp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBarAmp.Location = new System.Drawing.Point(909, 3);
            this.trackBarAmp.Minimum = -10;
            this.trackBarAmp.Name = "trackBarAmp";
            this.trackBarAmp.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarAmp.Size = new System.Drawing.Size(46, 295);
            this.trackBarAmp.TabIndex = 4;
            this.trackBarAmp.ValueChanged += new System.EventHandler(this.trackBarAmp_ValueChanged);
            // 
            // panelBottom
            // 
            this.panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBottom.Controls.Add(this.histoPanel);
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
            this.panelBottom.Location = new System.Drawing.Point(170, 329);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(733, 172);
            this.panelBottom.TabIndex = 3;
            // 
            // histoPanel
            // 
            this.histoPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.histoPanel.Location = new System.Drawing.Point(460, 0);
            this.histoPanel.Name = "histoPanel";
            this.histoPanel.Size = new System.Drawing.Size(271, 170);
            this.histoPanel.TabIndex = 16;
            this.histoPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panelHisto_Paint);
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
            this.panel2.Location = new System.Drawing.Point(170, 304);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(733, 19);
            this.panel2.TabIndex = 5;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 0);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(731, 17);
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
            this.panel3.Location = new System.Drawing.Point(3, 329);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(161, 172);
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
            this.butStartRecord.Location = new System.Drawing.Point(22, 6);
            this.butStartRecord.Name = "butStartRecord";
            this.butStartRecord.Size = new System.Drawing.Size(85, 23);
            this.butStartRecord.TabIndex = 0;
            this.butStartRecord.Text = "Start record";
            this.butStartRecord.UseVisualStyleBackColor = true;
            this.butStartRecord.Click += new System.EventHandler(this.butStartRecord_Click);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 504);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
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
        private Panel histoPanel;
        private Button butBCISetup;
    }
}