using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTestApp
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
        private double[] corrPattern;
        public int CorrPatternLength;

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

            string[] lines = File.ReadAllLines("pattern200Hz.txt");
            corrPattern = new double[lines.Length];
            CorrPatternLength = lines.Length;
            for (int i = 0; i < lines.Length; i++)
            {
                corrPattern[i] = Convert.ToDouble(lines[i]);
            }
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

        public void CountViewArrays(Control panel, TTestConfig config)
        {
            int SmoothWindowSize = 40;
            for (int i = 0; i < RealTimeArray.Length; i++)
            {
                PressureViewArray[i] = Filter.Median(6, RealTimeArray, i);
            }
            double[] DetrendArray = new double[_size];
            double max = DCArray.Max<double>();
            int maxInd = DCArray.ToList().IndexOf(max);
            double startVal = DCArray[0];
            for (int i = 0; i < maxInd; i++)
            {
                DetrendArray[i] = startVal + i * (max - startVal) / maxInd;
            }

            for (int i = 0; i < maxInd; i++)
            {
                PressureArray[i] = PressureViewArray[i] - DetrendArray[i];
            }

            PressureViewArray = DataProcessing.GetSmoothArray(PressureArray, SmoothWindowSize);

            DataProcessing.Corr(PressureViewArray, CorrelationArray, corrPattern);
            for (int i = 0; i < CorrelationArray.Length; i++)
            {
                CorrelationArray[i] = CorrelationArray[i] * 10;
            }

            CompressedArray = DataProcessing.GetCompressedArray(panel, RealTimeArray);
        }

        public String GetDataString(uint index)
        {
            return RealTimeArray[index].ToString();
        }
    }
}
