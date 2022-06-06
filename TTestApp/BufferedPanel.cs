namespace TTestApp
{
    public class BufferedPanel : Panel
    {
        public int Number;
        public BufferedPanel(int number)
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            Number = number;
            DoubleBuffered = true;
        }
    }
}
