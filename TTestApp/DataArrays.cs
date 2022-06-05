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

        public DataArrays(int size)
        {
            Size = (uint)size;
            PressureArray = new double[size];
            PressureViewArray = new int[size];
        }

        public void CountViewArray(bool filterOn)
        {
            int[] tmpArray = new int[Size];
            for (int i = 0; i < PressureArray.Length; i++)
            {
                tmpArray[i] = (int)(PressureArray[i] * 10000);
            }
            DataProcessing.Process(tmpArray, PressureViewArray, filterOn, Filter.coeff14);
        }

        public String GetDataString(uint index)
        {
            //"Dd:mm:yyyy HH:mm:ss:fff"
            return String.Concat(DateTime.Now.ToString(), '\t',
                                 PressureArray[index].ToString());
        }
    }
}
