namespace TTestApp
{
    internal class DataArrays
    {
        private readonly int _size;
        public double[] RealTimeArray;
        public double[] DCArray;
        public double[] PressureViewArray;
        public double[] EnvelopeArray;
        public double[] CompressedArray;
        public double[] DerivArray;
        public double[] DebugArray;
        public double[] DiffArray;

        public int Size { get { return _size; } }
        public DataArrays(int size)
        {
            _size = size;
            RealTimeArray = new double[_size];
            DCArray = new double[_size];
            PressureViewArray = new double[_size];
            EnvelopeArray = new double[_size];
            DerivArray = new double[_size];  
            DebugArray = new double[_size];
            DiffArray = new double[_size];
        }

        public static DataArrays? CreateDataFromLines(string[] lines)
        {
            DataArrays a = new(lines.Length);
            try
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    a.RealTimeArray[i] = Convert.ToInt32(lines[i]);
                }
                return a;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void CountViewArrays(Control panel)
        {
            int DCArrayWindow = 60;
            int ACArrayWindow = 6;
            for (int i = 0; i < _size; i++)
            {
                if (i < DCArrayWindow)
                {
                    continue;
                }
                double DCLevel = 0;
                for (int j = 0; j < DCArrayWindow; j++)
                {
                    DCLevel += RealTimeArray[i - j];
                }
                DCLevel /= DCArrayWindow;
                DCArray[i] = DCLevel;
                double ACLevel = 0;
                for (int j = 0; j < ACArrayWindow; j++)
                {
                    ACLevel += RealTimeArray[i - j];
                }
                ACLevel /= ACArrayWindow;
                PressureViewArray[i] = ACLevel - DCLevel;
            }


            //DCArray = DataProcessing.GetSmoothArray(RealTimeArray, DCArrayWindow);
            //PressureViewArray = DataProcessing.GetSmoothArray(RealTimeArray, ACArrayWindow);

            //for (int i = 0; i < Size; i++)
            //{
            //    PressureViewArray[i] = PressureViewArray[i] - DCArray[i];
            //}

            for (uint i = 0; i < PressureViewArray.Length; i++)
            {
                DerivArray[i] = DataProcessing.GetDerivative(PressureViewArray, i);
            }

            int DiffWinSize = 6;

            CompressedArray = DataProcessing.GetCompressedArray(panel, RealTimeArray);
        }

        public String GetDataString(uint index)
        {
            return RealTimeArray[index].ToString();
        }
    }
}
