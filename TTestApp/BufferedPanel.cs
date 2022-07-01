namespace TTestApp
{
    public class BufferedPanel : Panel
    {
        private readonly int _number;
        public BufferedPanel(int number)
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            _number = number;
            DoubleBuffered = true;
        }
        public int Number { get { return _number; } }
    }
}
