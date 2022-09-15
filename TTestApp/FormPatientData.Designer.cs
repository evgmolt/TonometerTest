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
            this.components = new System.ComponentModel.Container();
            this.numUpDownSYS = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numUpDownDIA = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numUpDownPULSE = new System.Windows.Forms.NumericUpDown();
            this.gbSex = new System.Windows.Forms.GroupBox();
            this.rbFemale = new System.Windows.Forms.RadioButton();
            this.rbMale = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.numUpDownAge = new System.Windows.Forms.NumericUpDown();
            this.cbArrythmia = new System.Windows.Forms.CheckBox();
            this.butOk = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tbComment = new System.Windows.Forms.TextBox();
            this.timerStatus = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownSYS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownDIA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPULSE)).BeginInit();
            this.gbSex.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownAge)).BeginInit();
            this.SuspendLayout();
            // 
            // numUpDownSYS
            // 
            this.numUpDownSYS.Location = new System.Drawing.Point(96, 207);
            this.numUpDownSYS.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDownSYS.Name = "numUpDownSYS";
            this.numUpDownSYS.Size = new System.Drawing.Size(100, 23);
            this.numUpDownSYS.TabIndex = 3;
            this.numUpDownSYS.Enter += new System.EventHandler(this.numUpDownSYS_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "SYS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 261);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "DIA";
            // 
            // numUpDownDIA
            // 
            this.numUpDownDIA.Location = new System.Drawing.Point(96, 260);
            this.numUpDownDIA.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDownDIA.Name = "numUpDownDIA";
            this.numUpDownDIA.Size = new System.Drawing.Size(100, 23);
            this.numUpDownDIA.TabIndex = 4;
            this.numUpDownDIA.Enter += new System.EventHandler(this.numUpDownDIA_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 320);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "PULSE";
            // 
            // numUpDownPULSE
            // 
            this.numUpDownPULSE.Location = new System.Drawing.Point(96, 319);
            this.numUpDownPULSE.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDownPULSE.Name = "numUpDownPULSE";
            this.numUpDownPULSE.Size = new System.Drawing.Size(100, 23);
            this.numUpDownPULSE.TabIndex = 5;
            this.numUpDownPULSE.Enter += new System.EventHandler(this.numUpDownPULSE_Enter);
            // 
            // gbSex
            // 
            this.gbSex.Controls.Add(this.rbFemale);
            this.gbSex.Controls.Add(this.rbMale);
            this.gbSex.Location = new System.Drawing.Point(22, 12);
            this.gbSex.Name = "gbSex";
            this.gbSex.Size = new System.Drawing.Size(171, 43);
            this.gbSex.TabIndex = 8;
            this.gbSex.TabStop = false;
            this.gbSex.Text = "Sex";
            // 
            // rbFemale
            // 
            this.rbFemale.AutoSize = true;
            this.rbFemale.Location = new System.Drawing.Point(75, 17);
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.Size = new System.Drawing.Size(63, 19);
            this.rbFemale.TabIndex = 1;
            this.rbFemale.Text = "Female";
            this.rbFemale.UseVisualStyleBackColor = true;
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
            this.label5.Location = new System.Drawing.Point(22, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Age";
            // 
            // numUpDownAge
            // 
            this.numUpDownAge.Location = new System.Drawing.Point(96, 68);
            this.numUpDownAge.Name = "numUpDownAge";
            this.numUpDownAge.Size = new System.Drawing.Size(100, 23);
            this.numUpDownAge.TabIndex = 1;
            this.numUpDownAge.Enter += new System.EventHandler(this.numUpDownAge_Enter);
            // 
            // cbArrythmia
            // 
            this.cbArrythmia.AutoSize = true;
            this.cbArrythmia.Location = new System.Drawing.Point(96, 373);
            this.cbArrythmia.Name = "cbArrythmia";
            this.cbArrythmia.Size = new System.Drawing.Size(86, 19);
            this.cbArrythmia.TabIndex = 6;
            this.cbArrythmia.Text = "Arrhythmia";
            this.cbArrythmia.UseVisualStyleBackColor = true;
            // 
            // butOk
            // 
            this.butOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.butOk.Location = new System.Drawing.Point(96, 460);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(100, 23);
            this.butOk.TabIndex = 7;
            this.butOk.Text = "Ok";
            this.butOk.UseVisualStyleBackColor = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(204, 460);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(100, 23);
            this.butCancel.TabIndex = 8;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "Comment";
            // 
            // tbComment
            // 
            this.tbComment.Location = new System.Drawing.Point(96, 114);
            this.tbComment.Name = "tbComment";
            this.tbComment.Size = new System.Drawing.Size(208, 23);
            this.tbComment.TabIndex = 2;
            // 
            // timerStatus
            // 
            this.timerStatus.Tick += new System.EventHandler(this.timerStatus_Tick);
            // 
            // FormPatientData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butCancel;
            this.ClientSize = new System.Drawing.Size(364, 515);
            this.Controls.Add(this.cbArrythmia);
            this.Controls.Add(this.tbComment);
            this.Controls.Add(this.numUpDownSYS);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.numUpDownDIA);
            this.Controls.Add(this.butOk);
            this.Controls.Add(this.numUpDownPULSE);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numUpDownAge);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gbSex);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPatientData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New record";
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownSYS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownDIA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPULSE)).EndInit();
            this.gbSex.ResumeLayout(false);
            this.gbSex.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownAge)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private NumericUpDown numUpDownSYS;
        private Label label2;
        private Label label3;
        private NumericUpDown numUpDownDIA;
        private Label label4;
        private NumericUpDown numUpDownPULSE;
        private GroupBox gbSex;
        private RadioButton rbFemale;
        private RadioButton rbMale;
        private Label label5;
        private NumericUpDown numUpDownAge;
        private Button butOk;
        private Button butCancel;
        private CheckBox cbArrythmia;
        private Label label6;
        private TextBox tbComment;
        private System.Windows.Forms.Timer timerStatus;
    }
}