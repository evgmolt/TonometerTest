﻿namespace TTestApp
{
    internal class DataArrays
    {
        private readonly int _size;
        public double[] RealTimeArray0;
        public double[] RealTimeArray1;
        public double[] RealTimeArray2;
        public double[] RealTimeArray3;
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
            RealTimeArray0 = new double[_size];
            RealTimeArray1 = new double[_size];
            RealTimeArray2 = new double[_size];
            RealTimeArray3 = new double[_size];
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
                    a.RealTimeArray0[i] = Convert.ToInt32(lines[i]);
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
            int SmoothWindowSize = 60;
            int MedianWindowSize = 6;
            for (int i = 0; i < RealTimeArray0.Length; i++)
            {
                PressureViewArray[i] = Filter.Median(MedianWindowSize, RealTimeArray0, i);
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

            CompressedArray = DataProcessing.GetCompressedArray(panel, RealTimeArray0);
        }

        public String GetDataString(uint index)
        {
            return RealTimeArray0[index].ToString();
        }
    }
}
