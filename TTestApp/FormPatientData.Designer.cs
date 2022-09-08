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
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numUpDownSYS = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numUpDownDIA = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numUpDownPULSE = new System.Windows.Forms.NumericUpDown();
            this.gbSex = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.rbMale = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.panelResult = new System.Windows.Forms.Panel();
            this.butOk = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cbArrythmia = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbComment = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownSYS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownDIA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPULSE)).BeginInit();
            this.gbSex.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.panelResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(96, 25);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(100, 23);
            this.tbName.TabIndex = 0;
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
            this.numUpDownSYS.Location = new System.Drawing.Point(83, 38);
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
            this.label2.Location = new System.Drawing.Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "SYS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "DIA";
            // 
            // numUpDownDIA
            // 
            this.numUpDownDIA.Location = new System.Drawing.Point(83, 91);
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
            this.label4.Location = new System.Drawing.Point(9, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "PULSE";
            // 
            // numUpDownPULSE
            // 
            this.numUpDownPULSE.Location = new System.Drawing.Point(83, 150);
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
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(66, 17);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(63, 19);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Female";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // rbMale
            // 
            this.rbMale.AutoSize = true;
            this.rbMale.Checked = true;
            this.rbMale.Location = new System.Drawing.Point(9, 17);
            this.rbMale.Name = "rbMale";
            this.rbMale.Size = new System.Drawing.Size(51, 19);
            this.rbMale.TabIndex = 0;
            this.rbMale.TabStop = true;
            this.rbMale.Text = "Male";
            this.rbMale.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Age";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(96, 134);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(100, 23);
            this.numericUpDown1.TabIndex = 10;
            this.numericUpDown1.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // panelResult
            // 
            this.panelResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelResult.Controls.Add(this.cbArrythmia);
            this.panelResult.Controls.Add(this.numUpDownSYS);
            this.panelResult.Controls.Add(this.label2);
            this.panelResult.Controls.Add(this.label3);
            this.panelResult.Controls.Add(this.numUpDownDIA);
            this.panelResult.Controls.Add(this.numUpDownPULSE);
            this.panelResult.Controls.Add(this.label4);
            this.panelResult.Location = new System.Drawing.Point(12, 231);
            this.panelResult.Name = "panelResult";
            this.panelResult.Size = new System.Drawing.Size(200, 257);
            this.panelResult.TabIndex = 11;
            // 
            // butOk
            // 
            this.butOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.butOk.Location = new System.Drawing.Point(12, 513);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(75, 23);
            this.butOk.TabIndex = 12;
            this.butOk.Text = "Ok";
            this.butOk.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(137, 513);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cbArrythmia
            // 
            this.cbArrythmia.AutoSize = true;
            this.cbArrythmia.Location = new System.Drawing.Point(17, 205);
            this.cbArrythmia.Name = "cbArrythmia";
            this.cbArrythmia.Size = new System.Drawing.Size(86, 19);
            this.cbArrythmia.TabIndex = 8;
            this.cbArrythmia.Text = "Arrhythmia";
            this.cbArrythmia.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "Comment";
            // 
            // tbComment
            // 
            this.tbComment.Location = new System.Drawing.Point(96, 183);
            this.tbComment.Name = "tbComment";
            this.tbComment.Size = new System.Drawing.Size(100, 23);
            this.tbComment.TabIndex = 15;
            // 
            // FormPatientData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 575);
            this.Controls.Add(this.tbComment);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.butOk);
            this.Controls.Add(this.panelResult);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gbSex);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPatientData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New patient";
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownSYS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownDIA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPULSE)).EndInit();
            this.gbSex.ResumeLayout(false);
            this.gbSex.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panelResult.ResumeLayout(false);
            this.panelResult.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox tbName;
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
        private Label label5;
        private NumericUpDown numericUpDown1;
        private Panel panelResult;
        private Button butOk;
        private Button button1;
        private CheckBox cbArrythmia;
        private Label label6;
        private TextBox tbComment;
    }
}