namespace TTestApp
{
    class WaveDetector
    {
        private int CurrentInterval;
        public double DetectLevel = 5;
        private const double MinDetectLevel = 5;
        private int LockInterval = 60;
        private const int NoWaveInterval1 = 600;
        private const int NoWaveInterval2 = 1000;
        public double MaxD;
        private int PrevIndex;
        private int PrevInterval;
        private int NumOfNN;
        public List<int> FiltredPoints;
        private readonly int _samplingFrequency;
        private const double _detectLevelCoeff = 0.55;
        private int _lastInterval;

        private double CurrentValue;
        public EventHandler<WaveDetectorEventArgs> OnWaveDetected;

        public int Arrhythmia;

        public WaveDetector(int samplingFrequency)
        {
            FiltredPoints = new List<int>();
            _samplingFrequency = samplingFrequency;
        }

        protected virtual void NewWaveDetected()
        {
            WaveDetectorEventArgs args = new()
            {
                WaveCount = FiltredPoints.Count,
                Amplitude = CurrentValue,
                Interval = _lastInterval,
                Arrithmia = Arrhythmia
            };
            OnWaveDetected?.Invoke(this, args);
        }

        public void Reset()
        {
            NumOfNN = 0;
            FiltredPoints.Clear();
            DetectLevel = MinDetectLevel;
            Arrhythmia = 0;
        }

        public double Detect(double[] DataArr, int Ind)
        {
            CurrentInterval++;
            if (CurrentInterval == NoWaveInterval1)
            {
                DetectLevel /= 2;
            }
            if (CurrentInterval > NoWaveInterval2)
            {
                DetectLevel /= 2;
            }
            DetectLevel = Math.Max(DetectLevel, MinDetectLevel);
            if (Ind < DataProcessing.DerivativeShift)
            {
                return DetectLevel;
            }

            if (CurrentInterval < LockInterval)
            {
                return DetectLevel;
            }

            CurrentValue = DataArr[Ind - 1];
            if (CurrentValue > DetectLevel)
            {
                MaxD = Math.Max((int)CurrentValue, MaxD);
                if (MaxD > CurrentValue)
                {
                    int tmpNN = 0;
                    if (PrevIndex > 0)
                    {
                        tmpNN = (Ind - 1) - PrevIndex;
                    }
                    if (Filter25percent(tmpNN))
                    {
                        _lastInterval = tmpNN;
                        FiltredPoints.Add(Ind - 1);
                        LockInterval = tmpNN / 2;
                        NewWaveDetected();
                    }
                    CurrentInterval = 0;
                    PrevIndex = Ind - 1;
                    NumOfNN++;
                    DetectLevel = MaxD * _detectLevelCoeff;
                    MaxD = 0;
                }
            }
            return DetectLevel;
        }

        private bool Filter25percent(int NewInterval)
        {
            const int LoLimit = 75;  //ms - 200 уд / мин 
            const int HiLimit = 500; //ms - 30  уд / мин
            const int MinNumberForArrythmia = 4;
            if (NewInterval < LoLimit) 
            {
                return false;
            }
            if (NewInterval > HiLimit)
            {
                return false;
            }
            int PrevInt = PrevInterval;
            PrevInterval = NewInterval;
            //Аритмию не оцениваем, если число зарегистрированных интервалов < Min
            if (NumOfNN < MinNumberForArrythmia)
            {
                return true;
            }
            if (NewInterval > PrevInt + PrevInt / 4 || NewInterval < PrevInt - PrevInt / 4)
            {
                Arrhythmia++;
                return true;
            }
            return true;
        }
    }
}
