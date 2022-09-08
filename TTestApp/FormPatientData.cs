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

        public void SetStateAfterRecord()
        {
            panelResult.Enabled = true;
            butOk.Text = "Ok";
            butOk.ForeColor = SystemColors.ControlText;
            timerStatus.Enabled = true;
        }

        public void SetStateBeforeRecord()
        {
            panelResult.Enabled = false;
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
    }
}
