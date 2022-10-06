namespace TTestApp
{
    internal static class DataProcessing
    {
        public static int DerivativeShift = 13;
        public static int DerivativeAverageWidth = 4;

        public static int ValueToMmHg(double value)
        {
            double zero = 17;
            double pressure = 220;
            double val = 4082;
            return (int)((value - zero) * pressure / (val - zero));
        }

        //Для BCI
        //public static int ValueToMmHg(double value)
        //{
        //    double zero = 465;
        //    double pressure = 142;
        //    double val = 2503287;
        //    return (int)((value - zero) * pressure / (val - zero));
        //}


        public static int GetMaxIndexInRegion(double[] sourceArray, int index)
        {
            int range = 60;
            double[] regionArray = new double[range];
            Array.Copy(sourceArray, index - range / 2, regionArray, 0, range);
            double max = regionArray.Max();
            int maxIndex = Array.IndexOf(regionArray, max);
            return index - range / 2 + maxIndex;
        }

        public static void SaveArray(string fname, int[] inputArray)
        {
            var stringsArr = inputArray.Select(s => s.ToString()).ToArray();
            File.WriteAllLines(fname, stringsArr);
        }
        public static void SaveArray(string fname, double[] inputArray)
        {
            var stringsArr = inputArray.Select(s => Math.Round(s).ToString()).ToArray();
            File.WriteAllLines(fname, stringsArr);
        }
        public static void SaveArray(string fname, PointF[] inputArray)
        {
            var stringsArr = inputArray.Select(s => s.X.ToString() + " " + s.Y.ToString()).ToArray();
            File.WriteAllLines(fname, stringsArr);
        }

        public static int GetPulseFromIndexesArray(int[] arrayOfIndexes, int SamplingFreq)
        {
            double secondPerMin = 60;
            double mean = 0;
            
            for (int i = 1; i < arrayOfIndexes.Length; i++) //Внимание! Цикл с 1!
            {
                mean += arrayOfIndexes[i] - arrayOfIndexes[i - 1];
            }
            mean /= arrayOfIndexes.Length - 1;
            
            //Аналог цикла и деления выше
//            mean = arrayOfIndexes.Zip(arrayOfIndexes.Skip(1), (first, second) => second - first).Average();

            mean /= SamplingFreq;
            mean = secondPerMin / mean;
            return (int)Math.Round(mean);
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

        public static double GetDerivative(double[] DataArr, uint Ind)
        {
            if (Ind < DerivativeAverageWidth / 2 + DerivativeShift)
            {
                return 0;
            }
            if (Ind - DerivativeAverageWidth / 2 + DerivativeAverageWidth > DataArr.Length - 1)
            {
                return 0;
            }
            List<double> L1 = new();
            List<double> L2 = new();
            for (int i = 0; i < DerivativeAverageWidth; i++)
            {
                {
                    L1.Add(DataArr[Ind - DerivativeAverageWidth / 2 + i]);
                    L2.Add(DataArr[Ind - DerivativeAverageWidth / 2 - DerivativeShift + i]);
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
