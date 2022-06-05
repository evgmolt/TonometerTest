using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTestApp
{
    public class DataArrays
    {
        public uint Size;
        public double[] PressureArray;
        public int[] PressureViewArray;
        public int[] PressureCompressedArray;

        public DataArrays(int size)
        {
            Size = (uint)size;
            PressureArray = new double[size];
            PressureViewArray = new int[size];
        }

        public static DataArrays? CreateDataFromLines(string[] lines)
        {
            int skip = 5;
            DataArrays a = new DataArrays(lines.Length - skip);
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


        public void CountViewArray(int destSize, bool filterOn)
        {
            int[] tmpArray = new int[Size];
            for (int i = 0; i < PressureArray.Length; i++)
            {
                tmpArray[i] = (int)(PressureArray[i] * 10000);
            }
            DataProcessing.Process(tmpArray, PressureViewArray, filterOn, Filter.coeff14);
            PressureCompressedArray = DataProcessing.GetCompressedArray(destSize, PressureViewArray);
        }

        public String GetDataString(uint index)
        {
            //"Dd:mm:yyyy HH:mm:ss:fff"
            return String.Concat(DateTime.Now.ToString(), '\t',
                                 PressureArray[index].ToString());
        }
    }
}
