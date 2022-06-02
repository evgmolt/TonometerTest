using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTestApp
{
    public class DataArrays
    {
        public int[] PressureArray;
        public int[] PressureViewArray;

        public DataArrays(int size)
        {
            PressureArray = new int[size];
            PressureViewArray = new int[size];
        }

        public String GetDataString(uint index)
        {
            //"Dd:mm:yyyy HH:mm:ss:fff"
            return String.Concat(DateTime.Now.ToString(), '\t',
                                 PressureArray[index].ToString());
        }
    }
}
