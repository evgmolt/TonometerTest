using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TTestApp
{
    class WaveDetector
    {
        public int PulseRate;
        private int InsideC;
        public double DetectLevel = 7;
        private const double MinDetectLevel = 3;
        private const int DiffShift = 13;
        private const int LockInterval = 60;
        private int NoWaveInterval1 = 600;
        private int NoWaveInterval2 = 800;
        private double MaxD;
        private const int NNArrSize = 10000;
        public Point[] NNPointArr;
        private int NNPointIndex;
        private int PrevInterval;
        private int[] NNArray;
        private int NNIndex;
        private int NumOfIntForAver;
        private const int MinNumOfIntForAver = 3;
        private const int MaxNumOfIntForAver = 10;
        public double[] DiffArr;
        public List<int> FiltredPoints;

        public WaveDetector()
        {
            NNPointArr = new Point[NNArrSize];
            NNArray = new int[NNArrSize];
            DiffArr = new double[ByteDecomposer.DataArrSize];
            FiltredPoints = new List<int>();
        }

        public void Reset()
        {
            NNPointIndex = 0;
            NNIndex = 0;
            FiltredPoints.Clear();
        }

        public int GetCurrentPulse()
        {
            if (NNIndex == 0)
            {
                return 0;
            }
            if (NNIndex < NumOfIntForAver)
            {
                NumOfIntForAver = NNIndex;
            }
            int sum = 0;
            for (int i = 0; i<NumOfIntForAver; i++)
            {
                sum += NNArray[(NNIndex - 1 - i) ];
            }
            sum /= NumOfIntForAver;
            double d = sum;
            d /= ByteDecomposer.SamplingFrequency; 
            d = 60 / d;
            return (int)Math.Round(d);
        }

        public double Detect(int OverflowState, double[] DataArr, int Ind)
        {
            if (OverflowState !=0)
            {
                PulseRate = 0;
                NumOfIntForAver = 0;
                return 0;
            }
            InsideC++;
            if (InsideC == NoWaveInterval1)
                DetectLevel = DetectLevel / 2;
            if (InsideC > NoWaveInterval2)
            {
                DetectLevel = DetectLevel / 2;
                PulseRate = 0;
                NumOfIntForAver = 0;
            }
            DetectLevel = Math.Max(DetectLevel, MinDetectLevel);
            if (Ind < DiffShift) return DetectLevel;
//            double Deriv = DataArr[Ind] - DataArr[Ind - DiffShift];
            double Deriv = DataArr[Ind];
            DiffArr[Ind] = Deriv;
            if (InsideC < LockInterval) return DetectLevel;
            if (Deriv > DetectLevel)
            {
                MaxD = Math.Max(MaxD, Deriv);
                if (MaxD > Deriv)
                {
                    int tmpNN = 0;
                    NNPointArr[NNPointIndex].X = Ind;
                    NNPointArr[NNPointIndex].Y = (int)MaxD;
                    if (NNPointIndex > 1)
                    {
                        tmpNN = NNPointArr[NNPointIndex].X - NNPointArr[NNPointIndex - 1].X;
                    }
                    if (Filter25percent(tmpNN))
                    {
                        NNArray[NNIndex] = tmpNN;
                        NNIndex++;
                        NumOfIntForAver++;
                        NumOfIntForAver = Math.Min(NumOfIntForAver, MaxNumOfIntForAver);
                        FiltredPoints.Add(Ind);
                    }
                    InsideC = 0;
                    NNPointIndex++;
                    DetectLevel = MaxD / 2;
                    MaxD = 0;
                }
            }
            return DetectLevel;
        }

        private bool Filter25percent(int NewInterval)
        {
            const int LoLimit = 50; //ms - 240 уд / мин 
            const int HiLimit = 400; //ms - 30  уд / мин 
            if (NewInterval < LoLimit) return false;
            if (NewInterval > HiLimit) return false;
            int PrevInt = PrevInterval;
            PrevInterval = NewInterval;
            if (NewInterval > PrevInt + PrevInt / 4) return false;
            if (NewInterval < PrevInt - PrevInt / 4) return false;
            return true;
        }
    }
}
