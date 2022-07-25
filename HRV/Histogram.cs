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
        private int _SDNN;
        public int[] HistoBuffer = new int[_histoBufferSize];
        public int Moda { get { return _moda; } }
        public int ModaAmplitude { get { return _modaAmplitude; } }
        public int RangeOfNN { get { return _rangeOfNN; } }
        public int SDNN { get { return _SDNN; } }

        public Histogram(int[] arrayOfIndexes, int samplingFrequency)
        {
            _samplingFrequency = samplingFrequency;
            
            _NNArray = PrepareData(arrayOfIndexes);
            int aver = (int)_NNArray.Average();
            var diffSqrArray = _NNArray.Select(x => (x - aver) * (x - aver)).ToArray();
            _SDNN = (int)Math.Sqrt(diffSqrArray.Sum() / diffSqrArray.Length);
            BuildHisto();
        }

        private int[] PrepareData(int[] arrayOfIndexes)
        {
            int msInSecond = 1000;
            const int LoLimit = 50; //ms - 240 уд / мин 
            const int HiLimit = 400 ; //ms - 30  уд / мин 
            List<int> result = new List<int>();   
            int prevInterval = arrayOfIndexes[1] - arrayOfIndexes[0]; ;
            for (int i = 0; i < arrayOfIndexes.Length - 1; i++)
            {
                int newInterval = arrayOfIndexes[i + 1] - arrayOfIndexes[i];
                prevInterval = newInterval;
                if (newInterval < LoLimit || 
                    newInterval > HiLimit || 
                    newInterval > prevInterval + prevInterval / 4 || 
                    newInterval < prevInterval - prevInterval / 4)
                {
                    continue;
                }
                result.Add(newInterval * msInSecond / _samplingFrequency);
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
            _moda = Array.IndexOf(HistoBuffer, _modaAmplitude);
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
            _rangeOfNN = histoRight - histoLeft + 1;
        }

    }
}