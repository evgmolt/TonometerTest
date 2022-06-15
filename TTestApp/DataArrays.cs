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
        public int[] RealTimeArray;
        public int[] DCRealTimeArray;
        public double[] PressureArray;
        public double[] PressureViewArray;
        public double[] PressureFiltredArray;
        public double[] PressureFiltredViewArray;
        public double[]? PressureCompressedArray;

        public double[] DetrendArray;

        public DataArrays(int size)
        {
            Size = (uint)size;
            RealTimeArray = new int[size];
            DCRealTimeArray = new int[size];
            PressureArray = new double[size];
            PressureViewArray = new double[size];
            PressureFiltredArray = new double[size];
            PressureFiltredViewArray = new double[size];
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

        public void CountViewArrays(int destSize, TTestConfig config)
        {

            PressureCompressedArray = DataProcessing.GetCompressedArray(destSize, RealTimeArray);
        }

        public String GetDataString(uint index)
        {
            return RealTimeArray[index].ToString();
        }
    }
}
