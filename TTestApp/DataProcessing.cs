namespace TTestApp
{
    internal static class DataProcessing
    {
        public static int ValueToMmhG(double value)
        {
            double zero = 17;
            double pressure = 143;
            double val = 2961;
            return (int)((value - zero) * pressure / (val - zero));
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
    }
}
