﻿namespace TTestApp
{
    class WaveDetector
    {
        public List<int> FiltredPoints;

        private int _currentInterval;
        private double _detectLevel = Constants.MinDetectLevel;
        private double _maxD;
        private int _prevIndex;
        private int _prevInterval;
        private int _numOfNN;
        private double _detectLevelCoeff;
        private int _lastInterval;
        private double _currentValue;
        private int _currentIndex;

        public EventHandler<WaveDetectorEventArgs> OnWaveDetected;

        public int Arrhythmia;

        public WaveDetector()
        {
            _detectLevelCoeff = Constants.DetectLevelCoeffSys;
            FiltredPoints = new List<int>();
        }

        protected virtual void NewWaveDetected()
        {
            WaveDetectorEventArgs args = new()
            {
                WaveCount = FiltredPoints.Count,
                Amplitude = _currentValue,
                Interval = _lastInterval,
                ArrhythmiaCount = Arrhythmia,
                Index = _currentIndex
            };
            OnWaveDetected?.Invoke(this, args);
        }

        public void Reset()
        {
            _numOfNN = 0;
            _detectLevel = Constants.MinDetectLevel;
            _detectLevelCoeff = Constants.DetectLevelCoeffSys;
            FiltredPoints.Clear();
            Arrhythmia = 0;
        }

        public double Detect(double[] dataArr, int index, int IndexOfMax)
        {
            if (index == IndexOfMax)
            {
                _detectLevelCoeff = Constants.DetectLevelCoeffDia;
            }
            return Detect(dataArr, index);
        }

        public double Detect(double[] dataArr, int index)
        {
            _currentIndex = index;
            _currentInterval++;
            if (_currentInterval == Constants.NoWaveInterval1)
            {
                _detectLevel /= 2;
            }
            if (_currentInterval == Constants.NoWaveInterval2)
            {
                _detectLevel = Constants.MinDetectLevel;
            }
            if (index < DataProcessing.DerivativeShift)
            {
                return _detectLevel;
            }

            if (_currentInterval < Constants.LockInterval)
            {
                return _detectLevel;
            }

            _currentValue = dataArr[index - 1];
            if (_currentValue > _detectLevel)
            {
                _maxD = Math.Max((int)_currentValue, _maxD);
                if (_maxD > _currentValue)
                {
                    int tmpNN = 0;
                    if (_prevIndex > 0)
                    {
                        tmpNN = (index - 1) - _prevIndex;
                    }
                    if (Filter25percent(tmpNN))
                    {
                        _lastInterval = tmpNN;
                        FiltredPoints.Add(index - 1);
                        Constants.LockInterval = tmpNN / 2;
                        NewWaveDetected();
                    }
                    _currentInterval = 0;
                    _prevIndex = index - 1;
                    _numOfNN++;
                    _detectLevel = _maxD * _detectLevelCoeff;
                    _maxD = 0;
                }
            }
            return _detectLevel;
        }

        private bool Filter25percent(int newInterval)
        {
            const int LoLimit = 75;  //ms - 200 уд / мин 
            const int HiLimit = 500; //ms - 30  уд / мин
            const int MinNumberForArrythmia = 4;
            if (newInterval < LoLimit) 
            {
                return false;
            }
            if (newInterval > HiLimit)
            {
                return false;
            }
            int PrevInt = _prevInterval;
            _prevInterval = newInterval;
            //Аритмию не оцениваем, если число зарегистрированных интервалов < Min
            if (_numOfNN < MinNumberForArrythmia)
            {
                return true;
            }
            if (newInterval > PrevInt + PrevInt / 4 || newInterval < PrevInt - PrevInt / 4)
            {
                Arrhythmia++;
                return true;
            }
            return true;
        }
    }
}
