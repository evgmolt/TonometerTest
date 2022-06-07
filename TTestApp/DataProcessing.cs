using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTestApp
{
    internal static class DataProcessing
    {
        public static int CompressionRatio;
        public static event EventHandler CompressionChanged;

        public static void SaveArray(string fname, int[] array)
        {
            var stringsArr = array.Select(s => s.ToString()).ToArray();
            File.WriteAllLines(fname, stringsArr);
        }
        public static int[] GetCompressedArray(int destSize, int[] inputArray)
        {
            int[] result = new int[destSize];
            CompressionRatio = inputArray.Length / destSize;
            for (int i = 0; i < destSize; i++)
            {
                result[i] = inputArray[i * CompressionRatio];
            }
            CompressionChanged(null, null);
            return result;
        }

        public static void PrepareData(double[] inData, double[] outData, bool filterOn, double[] coeff)
        {
            int size = inData.Length;
            double aver = 0;
            var tmpBuf = new double[size];
            for (int i = 0; i < size; i++)
            {
                aver += inData[i];
            }
            aver /= size;
            for (int i = 0; i < size; i++)
            {
                tmpBuf[i] = inData[i] - aver;
            }
            for (int i = 0; i < size; i++)
            {
                if (filterOn)
                {
                    outData[i] = Filter.FilterForView(coeff, tmpBuf, i);
                }
                else
                {
                    outData[i] = tmpBuf[i];
                }
            }
        }

        public static int GetRange(int[] Data)
        {
            int min = 10000000;
            int max = 0;
            foreach (int elem in Data)
            {
                min = Math.Min(min, elem);
                max = Math.Max(max, elem);
            }
            return (max - min);
        }

        public static double[] Smooth(double[] inputArray, int windowSize)
        {
            int size = inputArray.Length;
            double[] outputArray = new double[size];
            int i, j, z, k1, k2, hw;
            double tmp;
            if (windowSize % 2 == 0)
            {
                windowSize++;
            }
            hw = (windowSize - 1) / 2;
            outputArray[0] = inputArray[0];

            for (i = 1; i < size; i++)
            {
                tmp = 0;
                if (i < hw)
                {
                    k1 = 0;
                    k2 = 2 * i;
                    z = k2 + 1;
                }
                else if ((i + hw) > (size - 1))
                {
                    k1 = i - size + i + 1;
                    k2 = size - 1;
                    z = k2 - k1 + 1;
                }
                else
                {
                    k1 = i - hw;
                    k2 = i + hw;
                    z = windowSize;
                }

                for (j = k1; j <= k2; j++)
                {
                    tmp = tmp + inputArray[j];
                }
                outputArray[i] = tmp / z;
            }
            return outputArray;
        }
    }
}
