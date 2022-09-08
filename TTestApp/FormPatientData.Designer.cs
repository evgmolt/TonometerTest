namespace TTestApp
{
    partial class FormPatientData
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numUpDownSYS = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numUpDownDIA = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numUpDownPULSE = new System.Windows.Forms.NumericUpDown();
            this.gbSex = new System.Windows.Forms.GroupBox();
            this.rbMale = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownSYS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownDIA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPULSE)).BeginInit();
            this.gbSex.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(92, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 23);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // numUpDownSYS
            // 
            this.numUpDownSYS.Location = new System.Drawing.Point(92, 245);
            this.numUpDownSYS.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.numUpDownSYS.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numUpDownSYS.Name = "numUpDownSYS";
            this.numUpDownSYS.Size = new System.Drawing.Size(100, 23);
            this.numUpDownSYS.TabIndex = 2;
            this.numUpDownSYS.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 247);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "SYS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 300);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "DIA";
            // 
            // numUpDownDIA
            // 
            this.numUpDownDIA.Location = new System.Drawing.Point(92, 298);
            this.numUpDownDIA.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numUpDownDIA.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numUpDownDIA.Name = "numUpDownDIA";
            this.numUpDownDIA.Size = new System.Drawing.Size(100, 23);
            this.numUpDownDIA.TabIndex = 5;
            this.numUpDownDIA.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 359);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "PULSE";
            // 
            // numUpDownPULSE
            // 
            this.numUpDownPULSE.Location = new System.Drawing.Point(92, 357);
            this.numUpDownPULSE.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numUpDownPULSE.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numUpDownPULSE.Name = "numUpDownPULSE";
            this.numUpDownPULSE.Size = new System.Drawing.Size(100, 23);
            this.numUpDownPULSE.TabIndex = 7;
            this.numUpDownPULSE.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // gbSex
            // 
            this.gbSex.Controls.Add(this.radioButton2);
            this.gbSex.Controls.Add(this.rbMale);
            this.gbSex.Location = new System.Drawing.Point(21, 68);
            this.gbSex.Name = "gbSex";
            this.gbSex.Size = new System.Drawing.Size(171, 43);
            this.gbSex.TabIndex = 8;
            this.gbSex.TabStop = false;
            this.gbSex.Text = "Sex";
            // 
            // rbMale
            // 
            this.rbMale.AutoSize = true;
            this.rbMale.Location = new System.Drawing.Point(9, 17);
            this.rbMale.Name = "rbMale";
            this.rbMale.Size = new System.Drawing.Size(51, 19);
            this.rbMale.TabIndex = 0;
            this.rbMale.TabStop = true;
            this.rbMale.Text = "Male";
            this.rbMale.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(66, 17);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(63, 19);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Female";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // FormPatientData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gbSex);
            this.Controls.Add(this.numUpDownPULSE);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numUpDownDIA);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numUpDownSYS);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "FormPatientData";
            this.Text = "New patient";
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownSYS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownDIA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPULSE)).EndInit();
            this.gbSex.ResumeLayout(false);
            this.gbSex.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private NumericUpDown numUpDownSYS;
        private Label label2;
        private Label label3;
        private NumericUpDown numUpDownDIA;
        private Label label4;
        private NumericUpDown numUpDownPULSE;
        private GroupBox gbSex;
        private RadioButton radioButton2;
        private RadioButton rbMale;
    }
}