using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTestApp
{
    internal class HRV
    {
        private const int _histoBufferSize = 1000;
        private const int _histoBarWidth = 10;
        private readonly int[] _NNArray;//Массив интервалов в мс
        private int _moda;
        private int _modaAmplitude;
        private int _rangeOfNN;
        public int[] HistoBuffer = new int[_histoBufferSize];
        public int Moda { get { return _moda; } }
        public int ModaAmplitude { get { return _modaAmplitude; } }
        public int RangeOfNN { get { return _rangeOfNN; } } 

        public HRV(int[] arrayOfIndexes)
        {
            int msInSecond = 1000;
            _NNArray = new int[arrayOfIndexes.Length - 1];
            for (int i = 0; i < _NNArray.Length; i++)
            {
                _NNArray[i] = (arrayOfIndexes[i + 1] - arrayOfIndexes[i]) * msInSecond / ByteDecomposerBCI.SamplingFrequency;
            }
            BuildHisto();
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
        }
    }
}
