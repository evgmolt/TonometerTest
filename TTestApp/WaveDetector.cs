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
        public double DetectLevel = 20;
        private const double MinDetectLevel = 15;
        private const int DiffShift = 13;
        private const int LockInterval = 60;
        private int NoWaveInterval1 = 600;
        private int NoWaveInterval2 = 800;
        private double MaxD;
        private const int NNArrSize = 1000;
        public Point[] NNPointArr;
        private int NNPointIndex;
        private int PrevInterval;
        private int[] NNArray;
        private int NNIndex;
        private int NumOfIntForAver;
        private const int MinNumOfIntForAver = 3;
        private const int MaxNumOfIntForAver = 10;
        public double[] DiffArr;

        public WaveDetector()
        {
            NNPointArr = new Point[NNArrSize];
            NNArray = new int[NNArrSize];
            DiffArr = new double[ByteDecomposer.DataArrSize];
        }

        public void Reset()
        {
            NNPointIndex = 0;
            NNIndex = 0;
        }
        public int GetCurrentPulse(int NumNNForAver)
        {
            if (NNIndex == 0)
            {
                return 0;
            }
            if (NumNNForAver != 0)
            {
                NumOfIntForAver = NumNNForAver;
                if (NNIndex < NumOfIntForAver)
                {
                    NumOfIntForAver = NNIndex;
                }
            }
            int sum = 0;
            for (int i = 0; i<NumOfIntForAver; i++)
            {
                sum = sum + NNArray[(NNIndex - 1 - i) ];
            }
            sum = sum / NumOfIntForAver;
            double d = sum;
            d = d / ByteDecomposer.SamplingFrequency; 
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
                        NNPointArr[NNPointIndex].X = (int)Ind;
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
                            //if (NumOfIntForAver > MinNumOfIntForAver)
                            //    PulseRate = GetCurrentPulse(0);
                            //else PulseRate = 0;
                        }
                        InsideC = 0;
                        NNPointIndex++;
                        DetectLevel = MaxD - MaxD / 5;
                        MaxD = 0;
                    }
            }
            return DetectLevel;
        }

        private int GetDerivative(int[] DataArr, uint Ind)
        {
            const int Width = 4;
            List<int> L1 = new List<int>();
            List<int> L2 = new List<int>();
            for (int i=0; i<Width; i++)
            {
                L1.Add(DataArr[(Ind - Width /2 + i) & (ByteDecomposer.DataArrSize - 1)]);
                L2.Add(DataArr[(Ind - DiffShift - Width/2 + i) & (ByteDecomposer.DataArrSize - 1)]);
            }
            double A1 = L1.Average();
            double A2 = L2.Average();
            return (int)(A1 - A2);
        }

        private bool Filter25percent(int NewInterval)
        {
            const int LoLimit = 30; //ms - 250 уд / мин 
            const int HiLimit = 300; //ms - 25  уд / мин 
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
