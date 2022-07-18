using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TTestApp
{
    class WaveDetector
    {
        private int InsideC;
        public double DetectLevel = 500;
        private const double MinDetectLevel = 500;
        private const int DiffShift = 13;
        private const int LockInterval = 60;
        private int NoWaveInterval1 = 600;
        private int NoWaveInterval2 = 800;
        public double MaxD;
        private const int NNArrSize = 10000;
        private Point[] NNPointArr;
        private int NNPointIndex;
        private int PrevInterval;
        private int[] NNArray;
        private int NNIndex;
        private int NumOfIntForAver;
        private const int MinNumOfIntForAver = 3;
        private const int MaxNumOfIntForAver = 10;
        public List<int> FiltredPoints;
        public int MeanPressureInd = 0;

        public WaveDetector()
        {
            NNPointArr = new Point[NNArrSize];
            NNArray = new int[NNArrSize];
            FiltredPoints = new List<int>();
        }

        public void Reset()
        {
            NNPointIndex = 0;
            NNIndex = 0;
            FiltredPoints.Clear();
            MeanPressureInd = 0;
            DetectLevel = MinDetectLevel;
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
                NumOfIntForAver = 0;
                return 0;
            }
            InsideC++;
            if (InsideC == NoWaveInterval1)
            {
                DetectLevel = DetectLevel / 2;
            }
            if (InsideC > NoWaveInterval2)
            {
                DetectLevel = DetectLevel / 2;
                NumOfIntForAver = 0;
            }
            DetectLevel = Math.Max(DetectLevel, MinDetectLevel);
            if (Ind < DiffShift) return DetectLevel;
            double CurrentValue = DataArr[Ind];
            if (InsideC < LockInterval) return DetectLevel;
            if (CurrentValue > DetectLevel)
            {
                if (CurrentValue > MaxD)
                {
                    MaxD = CurrentValue;
                    MeanPressureInd = Ind;
                }
                if (MaxD > CurrentValue)
                {
                    int tmpNN = 0;
                    NNPointArr[NNPointIndex].X = Ind;
                    NNPointArr[NNPointIndex].Y = (int)MaxD;
                    if (NNPointIndex > 0)
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
