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
        public int[] _HistoBuffer = new int[_histoBufferSize];

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
                _HistoBuffer[histoIndex]++;
            }
        }
    }
}
