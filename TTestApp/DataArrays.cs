namespace TTestApp
{
    internal class DataArrays
    {
        private readonly int _size;
        public double[] RealTimeArray;
        public double[] EnvelopeArray;

        public int Size { get { return _size; } }
        public DataArrays(int size)
        {
            _size = size;
            RealTimeArray = new double[_size];
            EnvelopeArray = new double[_size];
        }


        public String GetDataString(uint index)
        {
            return RealTimeArray[index].ToString();
        }
    }
}
