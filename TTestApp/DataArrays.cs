﻿namespace TTestApp
{
    internal class DataArrays
    {
        private readonly int _size;
        public double[] RealTimeArray;
        public double[] DCArray;
        public double[] PressureArray;
        public double[] PressureViewArray;
        public double[] CorrelationArray;
        public double[] CompressedArray;
        public double[] DerivArray;
        public double[] DebugArray;

        public int Size { get { return _size; } }
        public DataArrays(int size)
        {
            _size = size;
            RealTimeArray = new double[_size];
            DCArray = new double[_size];
            PressureArray = new double[_size];
            PressureViewArray = new double[_size];
            CorrelationArray = new double[_size];
            DerivArray = new double[_size];  
            DebugArray = new double[_size];
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
            int SmoothWindowSize = 40;
            int MedianWindowSize = 6;
            for (int i = 0; i < RealTimeArray.Length; i++)
            {
                PressureViewArray[i] = Filter.Median(MedianWindowSize, RealTimeArray, i);
            }
            double max = DCArray.Max<double>();
            int maxInd = DCArray.ToList().IndexOf(max);
            double startVal = DCArray[0];
            double[] DetrendArray = new double[maxInd];
            for (int i = 0; i < maxInd; i++)
            {
                DetrendArray[i] = startVal + i * (max - startVal) / maxInd;
            }

            for (int i = 0; i < maxInd; i++)
            {
                PressureArray[i] = PressureViewArray[i] - DetrendArray[i];
            }

            PressureViewArray = DataProcessing.GetSmoothArray(PressureArray, SmoothWindowSize);
            for (uint i = 0; i < PressureViewArray.Length; i++)
            {
                DerivArray[i] = DataProcessing.GetDerivative(PressureArray, i);
            }

            CompressedArray = DataProcessing.GetCompressedArray(panel, RealTimeArray);
        }

        public String GetDataString(uint index)
        {
            return RealTimeArray[index].ToString();
        }
    }
}
