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
        public double[] PressureArray;
        public int[] PressureViewArray;
        public double[] PressureFiltredArray;
        public int[] PressureFiltredViewArray;
        public int[]? PressureCompressedArray;
        public int[]? PressureFiltredCompressedArray;
        public int[]? PressureSmoothArray;

        public DataArrays(int size)
        {
            Size = (uint)size;
            PressureArray = new double[size];
            PressureViewArray = new int[size];
            PressureFiltredArray = new double[size];
            PressureFiltredViewArray = new int[size];
            PressureSmoothArray = new int[size];
        }

        public static DataArrays? CreateDataFromLines(string[] lines)
        {
            int skip = 5;
            DataArrays a = new(lines.Length - skip);
            try
            {
                for (int i = skip; i < lines.Length; i++)
                {
                    string[] s = lines[i].Split('\t');
                    a.PressureArray[i - skip] = Convert.ToDouble(s[1]);
                }
                return a;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void CountViewArray(int destSize, TTestConfig config)
        {
            DataProcessing.PrepareData(PressureArray, PressureFiltredArray, config.FilterOn, Filter.coeff10Hz);
            for (int i = 0; i < PressureArray.Length; i++)
            {
                PressureViewArray[i] = (int)(PressureArray[i] * 100000);
                PressureFiltredViewArray[i] = (int)(PressureFiltredArray[i] * 100000);
            }
            PressureCompressedArray = DataProcessing.GetCompressedArray(destSize, PressureViewArray);
            PressureFiltredCompressedArray = DataProcessing.GetCompressedArray(destSize, PressureFiltredViewArray);
//            PressureSmoothArray = DataProcessing.Smooth(PressureFiltredCompressedArray, 10);
        }

        public String GetDataString(uint index)
        {
            //"Dd:mm:yyyy HH:mm:ss:fff"
            return String.Concat(DateTime.Now.ToString(), '\t',
                                 PressureArray[index].ToString());
        }
    }
}
