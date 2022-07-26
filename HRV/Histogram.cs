namespace HRV
{
    public class Histogram
    {
        private const int _histoBufferSize = 1000;
        private const int _histoBarWidth = 10;
        private readonly int[] _NNArray;//Массив интервалов в мс
        private readonly int _samplingFrequency;
        private int _moda;
        private int _modaAmplitude;
        private int _rangeOfNN;
        private readonly int _SDNN;
        private readonly int _RMSSD;
        private readonly int _averNN;
        public int[] HistoBuffer = new int[_histoBufferSize];
        public int Moda { get { return _moda; } }                   //ms
        public int ModaAmplitude { get { return _modaAmplitude; } }
        public int RangeOfNN { get { return _rangeOfNN; } }         //ms
        public int SDNN { get { return _SDNN; } }
        public int RMSSD { get { return _RMSSD; } }
        public int AverNN { get { return _averNN; } }

        public Histogram(int[] arrayOfIndexes, int samplingFrequency)
        {
            _samplingFrequency = samplingFrequency;
            
            _NNArray = GetRhythmogram(arrayOfIndexes);
            _averNN = (int)_NNArray.Average();
            //Вычисление SDNN
            var diffSqrArray = _NNArray.Select(x => (x - _averNN) * (x - _averNN)).ToArray();
            _SDNN = (int)Math.Sqrt(diffSqrArray.Sum() / diffSqrArray.Length);
            //Вычисление RMSSD
            var diffSeqNN = _NNArray.Zip(_NNArray.Skip(1), (first, second) => second - first);
            var diffSeqSqrNN = diffSeqNN.Select(x => x * x);
            _RMSSD = (int)Math.Sqrt(diffSeqSqrNN.Sum() / diffSeqSqrNN.Count());
            BuildHisto();
        }

        private int[] GetRhythmogram(int[] arrayOfIndexes)
        {
            int msInSecond = 1000;
            const int LoLimit = 50;   // 240 уд / мин 
            const int HiLimit = 400 ; // 30  уд / мин 
            List<int> result = new();   
            int prevInterval = arrayOfIndexes[1] - arrayOfIndexes[0];
            for (int i = 0; i < arrayOfIndexes.Length - 1; i++)
            {
                int newInterval = arrayOfIndexes[i + 1] - arrayOfIndexes[i];
                if (newInterval < LoLimit || newInterval > HiLimit)
                {
                    continue;
                }
                if (newInterval < prevInterval + prevInterval / 4 && newInterval > prevInterval - prevInterval / 4)
                {
                    result.Add(newInterval * msInSecond / _samplingFrequency);
                }
                prevInterval = newInterval;
            }
            return result.ToArray();
        }
        private void BuildHisto()
        {
            for (int i = 0; i < _NNArray.Length; i++)
            {
                int histoIndex = _NNArray[i] / _histoBarWidth;
                HistoBuffer[histoIndex]++;
            }
            _modaAmplitude = HistoBuffer.Max();
            _moda = _histoBarWidth * Array.IndexOf(HistoBuffer, _modaAmplitude);
            int histoLeft = 0;
            for (int i = 0; i < HistoBuffer.Length; i++)
            {
                if (HistoBuffer[i] != 0)
                {
                    histoLeft = i;
                    break;
                }
            }
            int histoRight = HistoBuffer.Length - 1;
            for (int i = HistoBuffer.Length - 1; i >= 0; i--)
            {
                if (HistoBuffer[i] != 0)
                {
                    histoRight = i;
                    break;
                }
            }
            _rangeOfNN = (histoRight - histoLeft + 1) * _histoBarWidth;
        }
    }
}