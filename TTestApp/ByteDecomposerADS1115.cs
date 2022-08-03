namespace TTestApp
{
    internal class ByteDecomposerADS1115 : ByteDecomposer
    {
        public override int SamplingFrequency => 200; 
        public override int BaudRate => 115200; 
        public override int BytesInBlock => 3;
        public override int MaxNoDataCounter => 10;

        private const int _queueForDCSize = 60;
        private const int _queueForACSize = 20;

        public ByteDecomposerADS1115(DataArrays data) : base(data, _queueForDCSize, _queueForACSize)
        {
        }

        public override int Decompos(USBserialPort usbport, Stream saveFileStream, StreamWriter txtFileStream)
        {
            int bytes = usbport.BytesRead;
            if (bytes == 0)
            {
                _noDataCounter++;
                if (_noDataCounter > MaxNoDataCounter)
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
                    MessageBox.Show("Save file stream error. " + ex.Message);
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
                    case 1:// pressure1_0
                        _pressureTmp = (int)usbport.PortBuf[i];
                        _byteNum = 2;
                        break;
                    case 2:// E1_1
                        _pressureTmp += 0x100 * (int)usbport.PortBuf[i];
                        _byteNum = 3;

                        QueueForDC.Enqueue(_pressureTmp);
                        if (QueueForDC.Count > _queueForDCSize)
                        {
                            QueueForDC.Dequeue();
                        }

                        _data.RealTimeArray[MainIndex] = _pressureTmp;
                        _data.DCArray[MainIndex] = (int)QueueForDC.Average();

                        QueueForAC.Enqueue(100 + _pressureTmp - (int)QueueForDC.Average());
                        if (QueueForAC.Count > _queueForACSize)
                        {
                            QueueForAC.Dequeue();
                        }

                        _data.PressureViewArray[MainIndex] = (int)QueueForAC.Average();

                        _byteNum = 0;

                        if (RecordStarted)
                        {
                            txtFileStream.WriteLine(_data.GetDataString(MainIndex));
                        }
                        OnDecomposeLineEvent();
                        LineCounter++;
                        MainIndex++;
                        MainIndex &= (DataArrSize - 1);
                        break;
                }
            }
            usbport.BytesRead = 0;
            return bytes;
        }
    }
}
