namespace TTestApp
{
    internal class DataArrays
    {
        private readonly int _size;
        public double[] RealTimeArray;
        public double[] DCArray;
        public double[] PressureArray;
        public double[] PressureViewArray;

        public int Size { get { return _size; } }
        public DataArrays(int size)
        {
            _size = size;
            RealTimeArray = new double[_size];
            DCArray = new double[_size];
            PressureArray = new double[_size];
            PressureViewArray = new double[_size];
        }

        public void CountViewArrays(Control panel)
        {
            int DCArrayWindow = 100;
            DCArray = DataProcessing.GetSmoothArray(RealTimeArray, DCArrayWindow);
            int SmoothWindowSize = 60;
            int MedianWindowSize = 6;
            for (int i = 0; i < RealTimeArray.Length; i++)
            {
                PressureViewArray[i] = Filter.Median(MedianWindowSize, RealTimeArray, i);
//                PressureViewArray[i] = RealTimeArray[i];
            }
            double max = DCArray.Max<double>();
            double lastVal = DCArray[Size - 1];
            double[] DetrendArray = new double[Size];
            for (int i = 0; i < Size; i++)
            {
                DetrendArray[i] = max - i * (max - lastVal) / Size;
            }

            for (int i = 0; i < Size; i++)
            {
                PressureArray[i] = PressureViewArray[i] - DCArray[i];
            }

            PressureViewArray = DataProcessing.GetSmoothArray(PressureArray, SmoothWindowSize);
        }

        public String GetDataString(uint index)
        {
            return RealTimeArray[index].ToString();
        }
    }
}
