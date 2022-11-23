namespace TTestApp
{
    internal static class DataProcessing
    {
        public static int DerivativeShift = 13;
        public static int DerivativeAverageWidth = 4;

        public static int[] GetSubArray(int[] inputArray, int start, int stop)
        {
            if (start < 0)
            {
                start = 0;
            }
            if (stop > inputArray.Length - 1)
            {
                stop = inputArray.Length - 1;
            }
            int[] subArray = new int[stop - start];
            for (int i = 0; i < stop - start; i++)
            {
                subArray[i] = inputArray[start + i];
            }
            return subArray;
        }

        public static int[] GetArrayOfWaveIndexes(double[] valuesArray, int[] indexesArray)
        {
            int[] indexes = new int[indexesArray.Length];
            for (int i = 0; i < indexesArray.Length; i++)
            {
                indexes[i] = DataProcessing.GetMaxIndexInRegion(valuesArray, indexesArray[i]);
            }
            return indexes;
        }

        public static int GetMaxIndexInRegion(double[] sourceArray, int index)
        {
            int range = 50;
            double max = -1000;
            int maxIndex = 0;
            for (int i = 0; i < range; i++)
            {
                int arrayIndex = index - range / 2 + i;
                if (arrayIndex < 0) continue;
                if (arrayIndex > sourceArray.Length - 1) continue;
                if (sourceArray[arrayIndex] > max)
                {
                    max = sourceArray[arrayIndex];
                    maxIndex = arrayIndex;
                }
            }
            return maxIndex;
        }

        //public static int GetMaxIndexInRegion(double[] sourceArray, int index)
        //{
        //    int range = 60;
        //    double[] regionArray = new double[range];
        //    range = 2 * Math.Min(range / 2, sourceArray.Length - index);
        //    Array.Copy(sourceArray, index - range / 2, regionArray, 0, range);
        //    double max = regionArray.Max();
        //    int maxIndex = Array.IndexOf(regionArray, max);
        //    int result = index - range / 2 + maxIndex;
        //    return (result >= sourceArray.Length - 1)? sourceArray.Length - 1 : result;
        //}

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

        public static int GetPulseFromIndexesArray(int[] arrayOfIndexes, int samplingFreq)
        {
            double secondsPerMin = 60;
            double mean = 0;
            int[] intervals = new int[arrayOfIndexes.Length - 1];
            for (int i = 1; i < arrayOfIndexes.Length; i++) //Внимание! Цикл с 1!
            {
                mean += arrayOfIndexes[i] - arrayOfIndexes[i - 1];
                intervals[i - 1] = arrayOfIndexes[i] - arrayOfIndexes[i - 1];
            }
            mean /= arrayOfIndexes.Length - 1; 
            mean /= samplingFreq; // Длительность интервала в cекундах
            mean = secondsPerMin / mean; // Ударов в минуту
            return (int)Math.Round(mean);
        }

        public static double GetDerivative(double[] dataArr, uint Ind)
        {
            if (Ind < DerivativeAverageWidth + DerivativeShift)
            {
                return 0;
            }
            if (Ind - DerivativeAverageWidth + DerivativeAverageWidth > dataArr.Length - 1)
            {
                return 0;
            }
            List<double> L1 = new();
            List<double> L2 = new();
            for (int i = 0; i < DerivativeAverageWidth; i++)
            {
                L1.Add(dataArr[Ind - DerivativeAverageWidth + i]);
                L2.Add(dataArr[Ind - DerivativeAverageWidth - DerivativeShift + i]);
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

        //public static double GetDerivative(double[] dataArr, uint Ind)
        //{
        //    if (Ind < DerivativeAverageWidth / 2 + DerivativeShift)
        //    {
        //        return 0;
        //    }
        //    if (Ind - DerivativeAverageWidth / 2 + DerivativeAverageWidth > dataArr.Length - 1)
        //    {
        //        return 0;
        //    }
        //    List<double> L1 = new();
        //    List<double> L2 = new();
        //    for (int i = 0; i < DerivativeAverageWidth; i++)
        //    {
        //        L1.Add(dataArr[Ind - DerivativeAverageWidth / 2 + i]);
        //        L2.Add(dataArr[Ind - DerivativeAverageWidth / 2 - DerivativeShift + i]);
        //    }
        //    if (L1.Count > 0 && L2.Count > 0)
        //    {
        //        double A1 = L1.Average();
        //        double A2 = L2.Average();
        //        return (A1 - A2);
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

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

        internal static void RemoveArtifacts(double[] arrValues)
        {
            int level = 8;
            for (int i = 1; i < arrValues.Length - 1; i++)
            {
                if (Math.Abs(arrValues[i] - arrValues[i - 1]) > level)
                {
                    arrValues[i] = (arrValues[i - 1] + arrValues[i + 1]) / 2;
                }
            }
        }
    }
}
