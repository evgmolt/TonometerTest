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
        public int[] PressureViewArray;
        public double[] PressureFiltredArray;
        public int[] PressureFiltredViewArray;
        public int[]? PressureCompressedArray;
        public int[]? PressureFiltredCompressedArray;

        public DataArrays(int size)
        {
            Size = (uint)size;
            RealTimeArray = new int[size];
            DCRealTimeArray = new int[size];
            PressureArray = new double[size];
            PressureViewArray = new int[size];
            PressureFiltredArray = new double[size];
            PressureFiltredViewArray = new int[size];
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
//            DataProcessing.PrepareData(PressureArray, PressureFiltredArray, config.FilterOn, Filter.coeff10Hz);
            PressureCompressedArray = DataProcessing.GetCompressedArray(destSize, RealTimeArray);
//            PressureFiltredCompressedArray = DataProcessing.GetCompressedArray(destSize, PressureFiltredViewArray);
        }

        public String GetDataString(uint index)
        {
            //"Dd:mm:yyyy HH:mm:ss:fff"
            return RealTimeArray[index].ToString();
        }
    }
}
