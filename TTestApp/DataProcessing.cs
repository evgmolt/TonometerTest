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
        public static event EventHandler? CompressionChanged;

        public static void SaveArray(string fname, int[] array)
        {
            var stringsArr = array.Select(s => s.ToString()).ToArray();
            File.WriteAllLines(fname, stringsArr);
        }

        public static double[] GetCompressedArray(int destSize, int[] inputArray)
        {
            double[] result = new double[destSize];
            CompressionRatio = inputArray.Length / destSize;
            for (int i = 0; i < destSize; i++)
            {
                result[i] = inputArray[i * CompressionRatio];
            }
            if (CompressionChanged != null)
            {
                CompressionChanged.Invoke(null, EventArgs.Empty);
            }
            return result;
        }

        public static double[] GetSmoothArray(double[] inputArray, int windowSize)
        {
            double[] result = new double[inputArray.Length];
            for (int i = 0; i < result.Length - windowSize; i++)
            {
                double aver = 0;
                for (int j = 0; j < windowSize; j++)
                {
                    aver += inputArray[i + j];
                }
                result[i] = aver /= windowSize;
            }
            return result;
        }

        public static int[] GetSmoothArray(int[] inputArray, int windowSize)
        {
            int[] result = new int[inputArray.Length];
            for (int i = 0; i < result.Length - windowSize; i++)
            {
                double aver = 0;
                for (int j = 0; j < windowSize; j++)
                {
                    aver += inputArray[i + j];
                }
                result[i] = (int)(aver /= windowSize);
            }
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

        public static int GetRange(double[] Data)
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

        //Вычисление производной
        public static double[] Diff(double[] Data)
        {
            int DiffShift = 13;
            double[] result = new double[Data.Length];
            for(int i = DiffShift; i < Data.Length - 1; i++)
            {
                result[i] = (Data[i] - Data[i - DiffShift]);
            }
            return result;
        }

        ////Корреляционная функция - весь массив
        //public static double[] Corr(double[] inputArray, double[] corrPattern)
        //{
        //    double[] result = new double[inputArray.Length - corrPattern.Length];
        //    for (int i = 0; i < inputArray.Length - corrPattern.Length; i++)
        //    {
        //        double val = 0;
        //        for(int j = 0; j < corrPattern.Length; j++)
        //        {
        //            val += inputArray[i + j] * corrPattern[j];
        //        }
        //        result[i] = val / (corrPattern.Length * 10);
        //    }
        //    return result;
        //}

        //Корреляционная функция - весь массив, коэффициент корреляции Пирсона
        public static void Corr(double[] inputArray, double[] resultArray, double[] corrPattern)
        {
            //Среднее шаблона
            double patternAver = corrPattern.Average();
            double[] window = new double[corrPattern.Length];
            for (int i = 0; i < inputArray.Length - corrPattern.Length; i++)
            {
                for (int j = 0; j < corrPattern.Length; j++)
                {
                    window[j] = inputArray[i + j];
                }
                double windowAver = window.Average();
                double numerator = 0;
                double denominator1 = 0;
                double denominator2 = 0;
                for (int j = 0; j < corrPattern.Length; j++)
                {
                    numerator += (window[j] - windowAver) * (corrPattern[j] - patternAver);
                    denominator1 += (window[j] - windowAver) * (window[j] - windowAver);
                    denominator2 += (corrPattern[j] - patternAver) * (corrPattern[j] - patternAver);
                }
                if (denominator1 == 0)
                {
                    resultArray[i] = 0;
                }
                else
                {
                    resultArray[i] = (double)(numerator / Math.Sqrt(denominator1 * denominator2));
                }
            }
            return;
        }

        //Скользящее среднее - весь массив
        public static int[] Smooth(int[] inputArray, int windowSize)
        {
            int size = inputArray.Length;
            int[] outputArray = new int[size];
            int i, j, z, k1, k2, hw;
            int tmp;
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
                outputArray[i] = tmp / z + 50;
            }
            return outputArray;
        }
    }
}
