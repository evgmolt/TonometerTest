using TTestApp.Decomposers;

namespace TTestApp
{
    public static class Filter
    {

        public static double Median(int windowSize, double[] inArr, int ind)
        {
            if (ind > inArr.Length - windowSize)
            {
                return 0;
            }
            double[] tmpArr = new double[windowSize];
            for (int i = 0; i < windowSize; i++)
            {
                tmpArr[i] = inArr[ind + i];
            }
            Array.Sort(tmpArr);
            if (tmpArr.Length %2 == 0)
            {
                return (tmpArr[tmpArr.Length / 2] + tmpArr[tmpArr.Length / 2 - 1] ) / 2; 
            }
            else 
            {
                return tmpArr[tmpArr.Length / 2];
            }
        }
    }
}
