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

        public void CountViewArray()
        {
            for (int i = 0; i < PressureArray.Length; i++)
            {
                PressureViewArray[i] = (int)(PressureArray[i] * 10000);
            }
        }

        public String GetDataString(uint index)
        {
            //"Dd:mm:yyyy HH:mm:ss:fff"
            return String.Concat(DateTime.Now.ToString(), '\t',
                                 PressureArray[index].ToString());
        }
    }
}
