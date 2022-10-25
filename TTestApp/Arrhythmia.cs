using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTestApp
{
    internal static class Arrhythmia
    {
        private const int numOfIntervals = 10;
        private const double level = 0.06;
        internal static bool AtrialFibrillation(int[] arrayOfIndex) //Мерцательная аритмия
        {
            int[] arrayOfIndexes = { 0, 2, 4, 6, 8, 10, 14, 16, 18, 21, 23, 25 };
            int length = arrayOfIndexes.Length;
            if (numOfIntervals > length - 1)
            {
                return false;
            }
            List<int> intervals = new();
            for (int i = 0; i < numOfIntervals; i++)
            {
                intervals.Add(arrayOfIndexes[length - i - 1] - arrayOfIndexes[length - i - 2]);
            }
            double aver = intervals.Average();
            double TwentyFivePercent = aver / 4;
            intervals.RemoveAll(x => Math.Abs(x - aver) > TwentyFivePercent);
            double SKO = Math.Sqrt(intervals.Select(x => (x - aver) * (x - aver)).Sum() / numOfIntervals);
            double res = SKO / aver;
            return res > level;
        }
    }
}
