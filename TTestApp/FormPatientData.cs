namespace TTestApp
{
    public partial class FormPatientData : Form
    {
        public Patient patient { get; }
        public FormPatientData(Patient newPatient)
        {
            InitializeComponent();
            if (newPatient is null)
            {
                patient = new Patient();
            }
            else
            {
                patient = newPatient;
                rbMale.Checked = patient.Sex;
                numUpDownAge.Value = patient.Age;
                rbFemale.Checked = !patient.Sex;
                tbComment.Text = patient.Comment;
            }
        }

        private void numUDEnable(bool enable)
        {
            numUpDownSYS.Enabled = enable;
            numUpDownDIA.Enabled = enable;
            numUpDownPULSE.Enabled = enable;
            cbArrythmia.Enabled = enable;
        }

        public void SetStateAfterRecord()
        {
            numUDEnable(true);
            butOk.Text = "Ok";
            butOk.ForeColor = SystemColors.ControlText;
            timerStatus.Enabled = true;
            TabIndex = numUpDownSYS.TabIndex;
        }

        public void SetStateBeforeRecord()
        {
            numUDEnable(false);
            butOk.Text = "Start record";
            butOk.ForeColor = Color.Red;
            timerStatus.Enabled = false;
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            patient.Sex = rbMale.Checked;
            patient.Age = (int)numUpDownAge.Value;
            patient.Comment = tbComment.Text;
            patient.Sys = (int)numUpDownSYS.Value;
            patient.Dia = (int)numUpDownDIA.Value;
            patient.Pulse = (int)numUpDownPULSE.Value;
            patient.Arrythmia = cbArrythmia.Checked;
        }

        private void timerStatus_Tick(object sender, EventArgs e)
        {
            butOk.Enabled = numUpDownAge.Value != 0 &&
                            numUpDownDIA.Value != 0 &&
                            numUpDownSYS.Value != 0 &&
                            numUpDownPULSE.Value != 0;
        }

        private void numUpDownSYS_Enter(object sender, EventArgs e)
        {
            numUpDownSYS.Select(0, numUpDownSYS.Text.Length);
        }

        private void numUpDownDIA_Enter(object sender, EventArgs e)
        {
            numUpDownDIA.Select(0, numUpDownDIA.Text.Length);
        }

        private void numUpDownPULSE_Enter(object sender, EventArgs e)
        {
            numUpDownPULSE.Select(0, numUpDownPULSE.Text.Length);
        }

        private void numUpDownAge_Enter(object sender, EventArgs e)
        {
            numUpDownAge.Select(0, numUpDownAge.Text.Length);
        }
    }
}
