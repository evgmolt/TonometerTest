namespace TTestApp
{
    public class BufferedPanel : Panel
    {
        public BufferedPanel()
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            DoubleBuffered = true;
        }
    }
}
