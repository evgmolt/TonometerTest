namespace TTestApp
{
    internal class ByteDecomposer
    {
        protected int _samplingFrequency;
        protected int _zeroLine;

        public const int DataArrSize = 0x100000;

        public int tmpZero;

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

        protected const byte marker1 = 0x19;

        protected DataArrays Data;

        public event EventHandler<PacketEventArgs> OnDecomposePacketEvent;

        public uint MainIndex = 0;
        public int PacketCounter = 0;

        public bool RecordStarted;
        public bool DeviceTurnedOn;

        protected int tmpValue;

        protected int noDataCounter;

        protected int byteNum;

        protected bool RateDetection = true;

        //Очереди для усреднения скользящим окном
        protected Queue<double> QueueForDC;
        protected Queue<double> QueueForAC;
        protected Queue<int> QueueForZero;
        public const int _queueForACSize = 6;
        public const int _queueForDCSize = 60;


        protected int sizeQForZero = 10;
        public ByteDecomposer(DataArrays data)
        {
            Data = data;
            RecordStarted = false;
            DeviceTurnedOn = true;
            MainIndex = 0;
            noDataCounter = 0;
            byteNum = 0;
            QueueForDC = new Queue<double>(_queueForDCSize);
            QueueForAC = new Queue<double>(_queueForACSize);
            QueueForZero = new Queue<int>(sizeQForZero);
            _samplingFrequency = 240;
            _zeroLine = 0;
        }

        protected virtual void OnDecomposeLineEvent()
        {
            OnDecomposePacketEvent?.Invoke(
                this,
                new PacketEventArgs
                {
                    DCValue = Data.DCArray[MainIndex],
                    RealTimeValue = Data.RealTimeArray[MainIndex],
                    PressureViewValue = Data.PressureViewArray[MainIndex],
                    DerivativeValue = Data.DerivArray[MainIndex],
                    PacketCounter = PacketCounter,
                    MainIndex = MainIndex
                });
        }
        private int MaxNoDataCounter = 10;

        public int Decompos(USBSerialPort usbport, Stream saveFileStream, StreamWriter txtFileStream)
        {
            int bytes = usbport.BytesRead;
            if (bytes == 0)
            {
                noDataCounter++;
                if (noDataCounter > MaxNoDataCounter)
                {
                    DeviceTurnedOn = false;
                }
                return 0;
            }
            DeviceTurnedOn = true;
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
                switch (byteNum)
                {
                    case 0:// Marker
                        if (usbport.PortBuf[i] == marker1)
                        {
                            byteNum = 1;
                        }
                        break;
                    case 1:
                        tmpValue = usbport.PortBuf[i];
                        byteNum = 2;
                        break;
                    case 2:
                        tmpValue += 0x100 * usbport.PortBuf[i];
                        if ((tmpValue & 0x8000) != 0)
                        {
                            tmpValue -= 0x10000;
                        }
                        QueueForZero.Enqueue(tmpValue);
                        if (QueueForZero.Count > sizeQForZero)
                        {
                            QueueForZero.Dequeue();
                            tmpZero = (int)QueueForZero.Average();
                        }

                        tmpValue -= ZeroLine;
                        //Очередь для выделения постоянной составляющей
                        QueueForDC.Enqueue(tmpValue);
                        if (QueueForDC.Count > _queueForDCSize)
                        {
                            QueueForDC.Dequeue();
                        }

                        //Массив исходных данных - смещение
                        Data.RealTimeArray[MainIndex] = tmpValue;
                        //Массив постоянной составляющей
                        Data.DCArray[MainIndex] = QueueForDC.Average();

                        //Очередь - переменная составляющая
                        QueueForAC.Enqueue(tmpValue - QueueForDC.Average());
                        if (QueueForAC.Count > _queueForACSize)
                        {
                            QueueForAC.Dequeue();
                        }

                        //Массив переменной составляющей
                        Data.PressureViewArray[MainIndex] = QueueForAC.Average();

                        byteNum = 0;

                        if (RecordStarted)
                        {
                            txtFileStream.WriteLine(Data.GetDataString(MainIndex));
                        }
                        OnDecomposeLineEvent();
                        PacketCounter++;
                        MainIndex++;
                        MainIndex &= DataArrSize - 1;
                        break;
                }
            }
            usbport.BytesRead = 0;
            return bytes;
        }
    }
}
