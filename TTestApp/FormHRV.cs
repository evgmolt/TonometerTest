using HRV;

namespace TTestApp
{
    public partial class FormHRV : Form
    {
        private readonly Histogram histo;
        public FormHRV(int[]? arrayOfIndexes, int samplingFrequency)
        {
            InitializeComponent();
            histo = new Histogram(arrayOfIndexes, samplingFrequency);
            labSDNN.Text = "SDNN : " + histo.SDNN.ToString();
            labAMo.Text = "Moda amp : " + histo.ModaAmplitude.ToString();
        }

        private void panelRythmogram_Paint(object sender, PaintEventArgs e)
        {
            int barWidth = 5;
            e.Graphics.Clear(Color.White);
            using (Brush brush1 = new SolidBrush(Color.Red))
            {
                for (int i = 0; i < histo.NNArray.Length; i++)
                {
                    int x1 = i * barWidth;
                    int y1 = panelRythmogram.Height - histo.NNArray[i] / 10;// * YScaleCoeff;
                    int w = barWidth;
                    int h = panelRythmogram.Height;
                    var R1 = new Rectangle(x1, y1, w, h);
                    e.Graphics.FillRectangle(brush1, R1);
                }
            }
        }

        private void panelHisto_Paint(object sender, PaintEventArgs e)
        {
            int barWidth = 5;
            if (histo is null)
            {
                return;
            }
            int YScaleCoeff = panelHisto.Height / histo.ModaAmplitude - 2;
            YScaleCoeff = 13;
            e.Graphics.Clear(Color.White);
            Brush brush0 = new SolidBrush(Color.Black);
            for (int i = 0; i < panelHisto.Width / barWidth; i++)
            {
                int x1 = i * barWidth;
                int y1 = panelHisto.Height - histo.HistoBuffer[i] * YScaleCoeff;
                int w = barWidth;
                int h = panelHisto.Height;
                var R1 = new Rectangle(x1, y1, w, h);
                e.Graphics.FillRectangle(brush0, R1);
            }
            brush0.Dispose();
        }
    }
}
