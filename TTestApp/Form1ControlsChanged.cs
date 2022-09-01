namespace TTestApp
{
    public partial class Form1
    {
        private void hScrollBar1_ValueChanged(object? sender, EventArgs e)
        {
            ViewShift = hScrollBar1.Value;
            BufPanel.Refresh();
        }
        private void radioButton11_CheckedChanged(object? sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                Compression = false;
                hScrollBar1.Visible = radioButton11.Checked;
                BufPanel.Refresh();
            }
        }

        private void radioButtonFit_CheckedChanged(object? sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                Compression = true;
                hScrollBar1.Visible = !radioButtonFit.Checked;
                hScrollBar1.Value = 0;
                MaxValue = 2 * (int)DataA.CompressedArray.Max();
                BufPanel.Refresh();
            }
        }
        private void trackBarAmp_ValueChanged(object? sender, EventArgs e)
        {
            double a = trackBarAmp.Value;
            ScaleY = Math.Pow(2, a / 2);
            BufPanel.Refresh();
        }
        private void numUDLeft_ValueChanged(object sender, EventArgs e)
        {
            Cfg.CoeffLeft = numUDLeft.Value;
        }

        private void numUDRight_ValueChanged(object sender, EventArgs e)
        {
            Cfg.CoeffRight = numUDRight.Value;
        }
    }
}
