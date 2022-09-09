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
            this.label1 = new System.Windows.Forms.Label();
            this.progressBarRecord = new System.Windows.Forms.ProgressBar();
            this.butFlow = new System.Windows.Forms.Button();
            this.labRecordSize = new System.Windows.Forms.Label();
            this.butStartRecord = new System.Windows.Forms.Button();
            this.butStopRecord = new System.Windows.Forms.Button();
            this.panelGraph = new System.Windows.Forms.Panel();
            this.trackBarAmp = new System.Windows.Forms.TrackBar();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.labCurrentPressure = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labPort = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timerRead = new System.Windows.Forms.Timer(this.components);
            this.timerStatus = new System.Windows.Forms.Timer(this.components);
            this.timerPaint = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 93F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1488, 820);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.progressBarRecord);
            this.panel1.Controls.Add(this.butFlow);
            this.panel1.Controls.Add(this.labRecordSize);
            this.panel1.Controls.Add(this.butStartRecord);
            this.panel1.Controls.Add(this.butStopRecord);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(256, 676);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 437);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // progressBarRecord
            // 
            this.progressBarRecord.Location = new System.Drawing.Point(25, 285);
            this.progressBarRecord.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.progressBarRecord.Name = "progressBarRecord";
            this.progressBarRecord.Size = new System.Drawing.Size(97, 31);
            this.progressBarRecord.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarRecord.TabIndex = 4;
            this.progressBarRecord.Visible = false;
            // 
            // butFlow
            // 
            this.butFlow.Location = new System.Drawing.Point(25, 18);
            this.butFlow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.butFlow.Name = "butFlow";
            this.butFlow.Size = new System.Drawing.Size(97, 31);
            this.butFlow.TabIndex = 0;
            this.butFlow.Text = "Stop stream";
            this.butFlow.UseVisualStyleBackColor = true;
            this.butFlow.Click += new System.EventHandler(this.butFlow_Click);
            // 
            // labRecordSize
            // 
            this.labRecordSize.AutoSize = true;
            this.labRecordSize.Location = new System.Drawing.Point(27, 320);
            this.labRecordSize.Name = "labRecordSize";
            this.labRecordSize.Size = new System.Drawing.Size(92, 20);
            this.labRecordSize.TabIndex = 2;
            this.labRecordSize.Text = "Record size :";
            // 
            // butStartRecord
            // 
            this.butStartRecord.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.butStartRecord.Location = new System.Drawing.Point(25, 148);
            this.butStartRecord.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.butStartRecord.Name = "butStartRecord";
            this.butStartRecord.Size = new System.Drawing.Size(97, 31);
            this.butStartRecord.TabIndex = 0;
            this.butStartRecord.Text = "New record";
            this.butStartRecord.UseVisualStyleBackColor = true;
            this.butStartRecord.Click += new System.EventHandler(this.butStartRecord_Click);
            // 
            // butStopRecord
            // 
            this.butStopRecord.Location = new System.Drawing.Point(25, 187);
            this.butStopRecord.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.butStopRecord.Name = "butStopRecord";
            this.butStopRecord.Size = new System.Drawing.Size(97, 31);
            this.butStopRecord.TabIndex = 1;
            this.butStopRecord.Text = "Stop record";
            this.butStopRecord.UseVisualStyleBackColor = true;
            this.butStopRecord.Click += new System.EventHandler(this.butStopRecord_Click);
            // 
            // panelGraph
            // 
            this.panelGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGraph.Location = new System.Drawing.Point(265, 4);
            this.panelGraph.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelGraph.Name = "panelGraph";
            this.panelGraph.Size = new System.Drawing.Size(1157, 676);
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
            this.trackBarAmp.Size = new System.Drawing.Size(57, 676);
            this.trackBarAmp.TabIndex = 4;
            this.trackBarAmp.ValueChanged += new System.EventHandler(this.trackBarAmp_ValueChanged);
            // 
            // panelBottom
            // 
            this.panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBottom.Controls.Add(this.labCurrentPressure);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottom.Location = new System.Drawing.Point(265, 731);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1157, 85);
            this.panelBottom.TabIndex = 3;
            // 
            // labCurrentPressure
            // 
            this.labCurrentPressure.AutoSize = true;
            this.labCurrentPressure.CausesValidation = false;
            this.labCurrentPressure.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labCurrentPressure.Location = new System.Drawing.Point(3, 8);
            this.labCurrentPressure.Name = "labCurrentPressure";
            this.labCurrentPressure.Size = new System.Drawing.Size(125, 37);
            this.labCurrentPressure.TabIndex = 5;
            this.labCurrentPressure.Text = "Current : ";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.hScrollBar1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(265, 688);
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
            this.panel3.Controls.Add(this.labPort);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 731);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(256, 85);
            this.panel3.TabIndex = 6;
            // 
            // labPort
            // 
            this.labPort.AutoSize = true;
            this.labPort.Location = new System.Drawing.Point(3, 56);
            this.labPort.Name = "labPort";
            this.labPort.Size = new System.Drawing.Size(46, 20);
            this.labPort.TabIndex = 4;
            this.labPort.Text = "Port : ";
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1488, 820);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pulse wave recorder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private Panel panelGraph;
        private HScrollBar hScrollBar1;
        private OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timerRead;
        private Panel panelBottom;
        private TrackBar trackBarAmp;
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
        private ProgressBar progressBarRecord;
        private Label label1;
    }
}