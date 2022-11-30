namespace TTestApp
{
    internal class ByteDecomposer
    {
        public int ZeroLine
        {
            get { return _zeroLine; }
            set { _zeroLine = value; }
        }
        public int SamplingFrequency
        {
            get { return _samplingFrequency; }
            set { _samplingFrequency = value; }
        }

        public event EventHandler<PacketEventArgs> OnDecomposePacketEvent;

        public uint MainIndex = 0;
        public int PacketCounter = 0;
        public bool RecordStarted;

        private const byte _marker1 = 0x19;
        private int _tmpZero;
        private int _samplingFrequency;
        private int _zeroLine;
        private DataArrays _data;
        private int _tmpValue;
        private int _byteNum;

        //Очереди для усреднения скользящим окном
        private Queue<double> _queueForDC;
        private Queue<double> _queueForAC;
        private Queue<int> _queueForZero;
        private const int _sizeQForAC = 6;
        private const int _sizeQForDC = 60;
        private const int _sizeQForZero = 10;

        public ByteDecomposer(DataArrays data)
        {
            RecordStarted = false;
            MainIndex = 0;
            _data = data;
            _byteNum = 0;
            _queueForDC = new Queue<double>(_sizeQForDC);
            _queueForAC = new Queue<double>(_sizeQForAC);
            _queueForZero = new Queue<int>(_sizeQForZero);
            _samplingFrequency = 128;
            _zeroLine = 0;
        }

        public void Calibr()
        {
            ZeroLine = _tmpZero;
        }

        protected virtual void OnDecomposeLineEvent()
        {
            OnDecomposePacketEvent?.Invoke(
                this,
                new PacketEventArgs
                {
                    DCValue = _data.DCArray[MainIndex],
                    RealTimeValue = _data.RealTimeArray[MainIndex],
                    PressureViewValue = _data.PressureViewArray[MainIndex],
                    DerivativeValue = _data.DerivArray[MainIndex],
                    PacketCounter = PacketCounter,
                    MainIndex = MainIndex
                });
        }

        public int Decompos(USBSerialPort usbport, Stream saveFileStream, StreamWriter txtFileStream)
        {
            int bytes = usbport.BytesRead;
            if (bytes == 0)
            {
                return 0;
            }
            if (saveFileStream != null && RecordStarted)
            {
                try
                {
                    saveFileStream.Write(usbport.PortBuf, 0, bytes);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Save file stream error" + ex.Message);
                }
            }
            for (int i = 0; i < bytes; i++)
            {
                switch (_byteNum)
                {
                    case 0:// Marker
                        if (usbport.PortBuf[i] == _marker1)
                        {
                            _byteNum = 1;
                        }
                        break;
                    case 1:
                        _tmpValue = usbport.PortBuf[i];
                        _byteNum = 2;
                        break;
                    case 2:
                        _tmpValue += 0x100 * usbport.PortBuf[i];

                        if ((_tmpValue & 0x8000) != 0)
                        {
                            _tmpValue -= 0x10000;
                        }
                        _queueForZero.Enqueue(_tmpValue);
                        if (_queueForZero.Count > _sizeQForZero)
                        {
                            _queueForZero.Dequeue();
                            _tmpZero = (int)_queueForZero.Average();
                        }

                        _tmpValue -= ZeroLine;
                        //Очередь для выделения постоянной составляющей
                        _queueForDC.Enqueue(_tmpValue);
                        if (_queueForDC.Count > _sizeQForDC)
                        {
                            _queueForDC.Dequeue();
                        }

                        //Массив исходных данных - смещение
                        _data.RealTimeArray[MainIndex] = _tmpValue;
                        //Массив постоянной составляющей
                        _data.DCArray[MainIndex] = _queueForDC.Average();

                        //Очередь - переменная составляющая
                        _queueForAC.Enqueue(_tmpValue - _queueForDC.Average());
                        if (_queueForAC.Count > _sizeQForAC)
                        {
                            _queueForAC.Dequeue();
                        }

                        //Массив переменной составляющей
                        _data.PressureViewArray[MainIndex] = _queueForAC.Average();

                        _byteNum = 0;

                        if (RecordStarted)
                        {
                            txtFileStream.WriteLine(_data.GetDataString(MainIndex));
                        }
                        OnDecomposeLineEvent();
                        PacketCounter++;
                        MainIndex++;
                        MainIndex &= Constants.DataArrSize - 1;
                        break;
                }
            }
            usbport.BytesRead = 0;
            return bytes;
        }
    }
}
