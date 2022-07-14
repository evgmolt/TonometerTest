using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTestApp
{
    internal class DataArrays
    {
        public int Size;
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
            Size = size;
            RealTimeArray = new double[Size];
            DCArray = new double[Size];
            PressureArray = new double[Size];
            PressureViewArray = new double[Size];
            CorrelationArray = new double[Size];
            DerivArray = new double[Size];  
            DebugArray = new double[Size];

            string[] lines = File.ReadAllLines("patt250.txt");
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
            for (int i = 0; i < PressureViewArray.Length; i++)
            {
                DerivArray[i] = DataProcessing.GetDerivative(PressureArray, i);
            }

            //DataProcessing.Corr(PressureViewArray, CorrelationArray, corrPattern);
            //for (int i = 0; i < CorrelationArray.Length; i++)
            //{
            //    CorrelationArray[i] = CorrelationArray[i] * 10000;
            //}

            CompressedArray = DataProcessing.GetCompressedArray(panel, RealTimeArray);
        }

        public String GetDataString(uint index)
        {
            return RealTimeArray[index].ToString();
        }
    }
}
