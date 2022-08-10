namespace TTestApp
{
    partial class FormHRV
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelRythmogram = new System.Windows.Forms.Panel();
            this.panelHisto = new System.Windows.Forms.Panel();
            this.panelHRVData = new System.Windows.Forms.Panel();
            this.labSDNN = new System.Windows.Forms.Label();
            this.labAMo = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelHRVData.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.125F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 86.875F));
            this.tableLayoutPanel1.Controls.Add(this.panelRythmogram, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelHisto, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelHRVData, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 334);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelRythmogram
            // 
            this.panelRythmogram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRythmogram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRythmogram.Location = new System.Drawing.Point(108, 3);
            this.panelRythmogram.Name = "panelRythmogram";
            this.panelRythmogram.Size = new System.Drawing.Size(689, 161);
            this.panelRythmogram.TabIndex = 0;
            this.panelRythmogram.Paint += new System.Windows.Forms.PaintEventHandler(this.panelRythmogram_Paint);
            // 
            // panelHisto
            // 
            this.panelHisto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHisto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHisto.Location = new System.Drawing.Point(108, 170);
            this.panelHisto.Name = "panelHisto";
            this.panelHisto.Size = new System.Drawing.Size(689, 161);
            this.panelHisto.TabIndex = 1;
            this.panelHisto.Paint += new System.Windows.Forms.PaintEventHandler(this.panelHisto_Paint);
            // 
            // panelHRVData
            // 
            this.panelHRVData.Controls.Add(this.labAMo);
            this.panelHRVData.Controls.Add(this.labSDNN);
            this.panelHRVData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHRVData.Location = new System.Drawing.Point(3, 170);
            this.panelHRVData.Name = "panelHRVData";
            this.panelHRVData.Size = new System.Drawing.Size(99, 161);
            this.panelHRVData.TabIndex = 2;
            // 
            // labSDNN
            // 
            this.labSDNN.AutoSize = true;
            this.labSDNN.Location = new System.Drawing.Point(9, 10);
            this.labSDNN.Name = "labSDNN";
            this.labSDNN.Size = new System.Drawing.Size(48, 15);
            this.labSDNN.TabIndex = 0;
            this.labSDNN.Text = "SDNN : ";
            // 
            // labAMo
            // 
            this.labAMo.AutoSize = true;
            this.labAMo.Location = new System.Drawing.Point(9, 34);
            this.labAMo.Name = "labAMo";
            this.labAMo.Size = new System.Drawing.Size(74, 15);
            this.labAMo.TabIndex = 1;
            this.labAMo.Text = "Moda amp : ";
            // 
            // FormHRV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 334);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormHRV";
            this.Text = "FormHRV";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelHRVData.ResumeLayout(false);
            this.panelHRVData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Panel panelRythmogram;
        private Panel panelHisto;
        private Panel panelHRVData;
        private Label labSDNN;
        private Label labAMo;
    }
}