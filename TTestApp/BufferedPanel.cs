namespace TTestApp
{
    public class BufferedPanel : Panel
    {
        public int Number;
        public BufferedPanel(int number)
        {
            Number = number;
            DoubleBuffered = true;
        }
    }
}
