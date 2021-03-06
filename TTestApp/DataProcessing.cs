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

        public static void SaveArray(string fname, int[] inputArray)
        {
            var stringsArr = inputArray.Select(s => s.ToString()).ToArray();
            File.WriteAllLines(fname, stringsArr);
        }

        public static int GetPulseFromIndexesArray(int[] indexesArray)
        {
            int secondPerMin = 60;
            double mean = 0;

            for (int i = 1; i < indexesArray.Length; i++) //Внимание! Цикл с 1!
            {
                mean += indexesArray[i] - indexesArray[i - 1];
            }
            //Аналог цикла выше
            mean = indexesArray.Zip(indexesArray.Skip(1), (first, second) => second - first).Sum();

            mean /= indexesArray.Length - 1;
            mean /= ByteDecomposerBCI.SamplingFrequency;
            mean = secondPerMin / mean;
            return (int)mean;
        }

        public static int[] ExpandArray(int[] inputArray, double[] CorrArray, int expandBy)
        {
            int[] resultArray = new int[inputArray.Length + expandBy * 2];
            int intervalForSearch = 50;
            int meanInterval = 0;
            for (int i = 1; i < inputArray.Length; i++)//Внимание! Цикл с 1!
            {
                meanInterval += inputArray[i] - inputArray[i - 1];
            }
            meanInterval /= inputArray.Length - 1;
            
            int index = inputArray[0];
            for (int k = 0; k < expandBy; k++)
            {
                index -= meanInterval;
                if (index < 0)
                {
                    break;
                }
                double max = -1000000;
                int maxIndex = 0;
                for (int i = index - intervalForSearch / 2; i < index + intervalForSearch; i++)
                {
                    if (CorrArray[i] > max)
                    {
                        max = CorrArray[i];
                        maxIndex = i;
                    }
                }
                resultArray[expandBy - 1 - k] = maxIndex;
            }
            Array.Copy(inputArray, 0, resultArray, expandBy, inputArray.Length);
            index = inputArray[inputArray.Length - 1];
            for (int k = 0; k < expandBy; k++)
            {
                index += meanInterval;
                double max = -1000000;
                int maxIndex = 0;
                for (int i = index + intervalForSearch / 2; i < index + intervalForSearch; i++)
                {
                    if (i > CorrArray.Length - 1)
                    {
                        break;
                    }
                    if (CorrArray[i] > max)
                    {
                        max = CorrArray[i];
                        maxIndex = i;
                    }
                }
                resultArray[expandBy + inputArray.Length + k] = maxIndex;
            }
            return resultArray;
        }

        private static bool IsSequential(int[] inputArray, int mean, int num, int index)
        {
            for (int i = 1; i < num; i++)
            {
                if (i + index >= inputArray.Length)
                {
                    return false;
                }
                int interval = inputArray[i + index] - inputArray[i + index - 1];
                if (Math.Abs(interval - mean) > mean / 5)
                {
                    return false;
                }
            }
            return true;
        }

        //Ищет индекс, после которого идут numOfSeq элементов, каждый из которых отличается от предыдущего 
        //не более чем на 20% от медианного значения. Возвращает исходный массив начиная с найденного индекса.
        public static int[]? GetSequentialArray(int[] inputArray)
        {
            int numOfSeq = 7;
            if (inputArray.Length < numOfSeq)
            {
                return null;
            }
            int[] intervalsArray = new int[inputArray.Length - 1];
            for (int i = 1; i < intervalsArray.Length; i++)
            {
                intervalsArray[i] = inputArray[i] - inputArray[i - 1];
            }
            Array.Sort(intervalsArray);
            int medianValue = intervalsArray[intervalsArray.Length / 2];
            int startSeqIndex = 0;
            for (int i = 0; i < inputArray.Length; i++)
            {
                if (IsSequential(inputArray, medianValue, numOfSeq, i))
                {
                    startSeqIndex = i;
                    break;
                }
            }    
            int[] result = inputArray.Skip(startSeqIndex).ToArray();
            return result;
        }

        public static double[] GetCompressedArray(Control panel, double[] inputArray)
        {
            double min = inputArray.Min();
            double[] result = new double[panel.Width];
            CompressionRatio = inputArray.Length / panel.Width;
            for (int i = 0; i < panel.Width; i++)
            {
                result[i] = inputArray[i * CompressionRatio] - min;
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

        public static int GetRange(double[] Data)
        {
            int max = (int)Data.Max();
            int min = (int)Data.Min();
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

        public static double GetDerivative(double[] DataArr, int Ind)
        {
            int DiffShift = 13;
            const int Width = 4;
            if (Ind < Width / 2 + DiffShift)
            {
                return 0;
            }
            if (Ind - Width / 2 + Width > DataArr.Length - 1)
            {
                return 0;
            }
            List<double> L1 = new();
            List<double> L2 = new();
            for (int i = 0; i < Width; i++)
            {
                {
                    L1.Add(DataArr[Ind - Width / 2 + i]);
                    L2.Add(DataArr[Ind - Width / 2 - DiffShift + i]);
                }
            }
            if (L1.Count > 0 && L2.Count > 0)
            {
                double A1 = L1.Average();
                double A2 = L2.Average();
                return (A1 - A2);
            }
            else
            {
                return 0;
            }

        }

        //Корреляционная функция - весь массив, коэффициент корреляции Пирсона
        public static void Corr(double[] inputArray, double[] resultArray, double[] corrPattern)
        {
            //Среднее шаблона
            double patternAver = corrPattern.Average();
            double[] slidingWindow = new double[corrPattern.Length];
            for (int i = 0; i < inputArray.Length - corrPattern.Length; i++)
            {
                for (int j = 0; j < corrPattern.Length; j++)
                {
                    slidingWindow[j] = inputArray[i + j];
                }
                double windowAver = slidingWindow.Average();
                double numerator = 0;
                double denominator1 = 0;
                double denominator2 = 0;
                for (int j = 0; j < corrPattern.Length; j++)
                {
                    numerator    += (slidingWindow[j] - windowAver) * (corrPattern[j] - patternAver);
                    denominator1 += (slidingWindow[j] - windowAver) * (slidingWindow[j] - windowAver);
                    denominator2 += (corrPattern[j]  - patternAver) * (corrPattern[j] - patternAver);
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
