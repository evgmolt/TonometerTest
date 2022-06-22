using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTestApp
{
    internal class DataArrays
    {
        public uint Size;
        public double[] RealTimeArray;
        public double[] DCArray;
        public double[] PressureArray;
        public double[] PressureViewArray;
        public double[] CorrelationArray;
        public double[] PressureCompressedArray;
        public double[] DebugArray;

        public DataArrays(int size)
        {
            Size = (uint)size;
            RealTimeArray = new double[size];
            DCArray = new double[size];
            PressureArray = new double[size];
            PressureViewArray = new double[size];
            CorrelationArray = new double[size];
            DebugArray = new double[size];
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

        public void CreateDetrendArray(int size)
        {
            int SmoothWindowSize = 40;
            for (int i = 0; i < RealTimeArray.Length; i++)
            {
                PressureViewArray[i] = Filter.Median(6, RealTimeArray, i);
            }
            double[] DetrendArray = new double[size];
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
            //            DataA.PressureArray = DataProcessing.Diff(DataA.PressureViewArray);
        }

        public void CountViewArrays(int destSize, TTestConfig config)
        {
            string[] lines = File.ReadAllLines("file.txt");
            double[] corr = new double[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                corr[i] = Convert.ToDouble(lines[i]);
            }
            DataProcessing.Corr(PressureViewArray, CorrelationArray, corr);
            for (int i = 0; i < CorrelationArray.Length; i++)
            {
                CorrelationArray[i] = CorrelationArray[i] * 10;
            }
            PressureCompressedArray = DataProcessing.GetCompressedArray(destSize, RealTimeArray);
        }

        public String GetDataString(uint index)
        {
            return RealTimeArray[index].ToString();
        }
    }
}
