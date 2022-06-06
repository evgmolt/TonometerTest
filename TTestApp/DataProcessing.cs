﻿using System;
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

        public static int[] GetCompressedArray(int destSize, int[] inputArray)
        {
            int[] result = new int[destSize];
            CompressionRatio = inputArray.Length / destSize;
            for (int  i = 0; i < destSize; i++)
            {
                int aver = 0;
                for (int j = 0; j < CompressionRatio; j++)
                {
                    aver += inputArray[i * CompressionRatio + j];
                }
                result[i] = aver / CompressionRatio;
            }
            CompressionChanged(null, null);
            return result;
        }

        public static void PrepareData(int[] inData, int[] outData, bool filterOn, double[] coeff)
        {
            int size = inData.Length;
            int aver = 0;
            var tmpBuf = new int[size];
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
                else outData[i] = tmpBuf[i];
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
    }
}
