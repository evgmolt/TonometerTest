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
        public double DetectLevel = 0.2;
        private const double MinDetectLevel = 0.05;
        private const int DiffShift = 8;
        private const int LockInterval = 48;
        private int NoWaveInterval1 = 400;
        private int NoWaveInterval2 = 480;
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
        public double[] LevelArr;

        public WaveDetector()
        {
            NNPointArr = new Point[NNArrSize];
            NNArray = new int[NNArrSize];
            DiffArr = new double[ByteDecomposer.DataArrSize];
            LevelArr = new double[ByteDecomposer.DataArrSize];
        }

        private int GetCurrentPulse()
        {
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

        public void Detect(int OverflowState, double[] OriginalDataArr, double[] DataArr, int Ind)
        {
            if (OverflowState !=0)
            {
                PulseRate = 0;
                NumOfIntForAver = 0;
                return;
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
            LevelArr[Ind] = DetectLevel;
            if (Ind < DiffShift) return;
            double Deriv = DataArr[Ind] - DataArr[Ind - DiffShift];
            DiffArr[Ind] = Deriv;
            if (InsideC < LockInterval) return;
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
                            if (NumOfIntForAver > MinNumOfIntForAver)
                                PulseRate = GetCurrentPulse();
                            else PulseRate = 0;
                            double Max = 0;
                            double Min = 0;
                            int count = 0;
                            double sum = 0;
                            for (int i = NNPointArr[(NNPointIndex - 1) & (NNArrSize - 1)].X; i < NNPointArr[NNPointIndex].X; i++)
                            {
                                Max = Math.Max(Max, DataArr[i]);
                                Min = Math.Min(Min, DataArr[i]);
                                sum = sum + OriginalDataArr[i];
                                count++;
                            }
                            double constLevel = sum / count;
                            double tmp = Max - Min;
                            tmp = tmp / constLevel;
                        }
                        InsideC = 0;
                        NNPointIndex++;
                        NNPointIndex = NNPointIndex & (NNArrSize - 1);
                        DetectLevel = MaxD / 2;
                        MaxD = 0;
                    }
            }
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
