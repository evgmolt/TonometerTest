namespace TTestApp
{
    class WaveDetector
    {
        private int CurrentInterval;
        public double DetectLevel = 100;
        private const double MinDetectLevel = 100;
        private const int DiffShift = 13;
        private int LockInterval = 60;
        private const int NoWaveInterval1 = 600;
        private const int NoWaveInterval2 = 1000;
        public double MaxD;
        private const int NNArrSize = 10000;
        private Point[] NNPointArr;
        private int NNPointIndex;
        private int PrevInterval;
        private int[] NNArray;
        private int NNIndex;
        private int NumOfIntervalsForAver;
        private const int MinNumOfIntervalsForAver = 3;
        private const int MaxNumOfIntervalsForAver = 10;
        public List<int> FiltredPoints;
        private readonly int _samplingFrequency;

        private double DataValue;
        public event EventHandler<WaveDetectorEventArgs>? OnWaveDetected;

        public WaveDetector(int samplingFrequency)
        {
            NNPointArr = new Point[NNArrSize];
            NNArray = new int[NNArrSize];
            FiltredPoints = new List<int>();
            _samplingFrequency = samplingFrequency;
        }

        protected virtual void WaveDetected()
        {
            WaveDetectorEventArgs args = new WaveDetectorEventArgs();
            args.WaveCount = FiltredPoints.Count();
            args.DerivValue = DataValue;
            OnWaveDetected?.Invoke(this, args);
        }
        public void Reset()
        {
            NNPointIndex = 0;
            NNIndex = 0;
            FiltredPoints.Clear();
            DetectLevel = MinDetectLevel;
        }

        public int GetCurrentPulse()
        {
            if (NNIndex == 0)
            {
                return 0;
            }
            if (NNIndex < NumOfIntervalsForAver)
            {
                NumOfIntervalsForAver = NNIndex;
            }
            int sum = 0;
            for (int i = 0; i<NumOfIntervalsForAver; i++)
            {
                sum += NNArray[(NNIndex - 1 - i) ];
            }
            sum /= NumOfIntervalsForAver;
            double d = sum;
            d /= _samplingFrequency; 
            d = 60 / d;
            return (int)Math.Round(d);
        }

        public double Detect(int OverflowState, double[] DataArr, int Ind)
        {
            if (OverflowState !=0)
            {
                NumOfIntervalsForAver = 0;
                return 0;
            }
            CurrentInterval++;
            if (CurrentInterval == NoWaveInterval1)
            {
                DetectLevel /= 2;
            }
            if (CurrentInterval > NoWaveInterval2)
            {
                DetectLevel /= 2;
                NumOfIntervalsForAver = 0;
            }
            DetectLevel = Math.Max(DetectLevel, MinDetectLevel);
            if (Ind < DiffShift) return DetectLevel;
            double CurrentValue = DataArr[Ind - 1];
            if (CurrentInterval < LockInterval) return DetectLevel;
            if (CurrentValue > DetectLevel)
            {
                MaxD = Math.Max((int)CurrentValue, MaxD);
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
                        NumOfIntervalsForAver++;
                        NumOfIntervalsForAver = Math.Min(NumOfIntervalsForAver, MaxNumOfIntervalsForAver);
                        FiltredPoints.Add(Ind);
                        LockInterval = tmpNN / 2;
                        DataValue = CurrentValue;
                        WaveDetected();
                    }
                    CurrentInterval = 0;
                    NNPointIndex++;
                    DetectLevel = MaxD / 2;
                    MaxD = 0;
                }
            }
            return DetectLevel;
        }

        private bool Filter25percent(int NewInterval)
        {
            const int LoLimit = 50;  //ms - 240 уд / мин 
            const int HiLimit = 400; //ms - 30  уд / мин 
            if (NewInterval < LoLimit) return false;
            if (NewInterval > HiLimit) return false;
            return true;
            int PrevInt = PrevInterval;
            PrevInterval = NewInterval;
            if (NewInterval > PrevInt + PrevInt / 2) return false;
            if (NewInterval < PrevInt - PrevInt / 2) return false;
            return true;
        }
    }
}
