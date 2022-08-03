using System.Diagnostics;

namespace TTestApp
{
    sealed class ByteDecomposerBCI : ByteDecomposer
    {
        public override int SamplingFrequency => 250;

        public override int BaudRate => 460800;

        public override int BytesInBlock => 65;

        public override int MaxNoDataCounter => 100;

        private const byte _markerBCI0 = 0xAA;
        private const byte _markerBCI1 = 0x55;
        private const byte _markerBCI2 = 0x66;
        private const byte _markerBCI3 = 0x77;
        private const byte _markerBCI4 = 0xA3;

        private const int _queueForACSize = 20;
        private const int _queueforDCSize = 60;
        public ByteDecomposerBCI(DataArrays data) : base(data, _queueforDCSize, _queueForACSize)
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
                    Debug.WriteLine(ex.Message);
                }
            }
            for (int i = 0; i < bytes; i++)
            {
                switch (_byteNum)
                {
                    case 0:// Marker
                        if (usbport.PortBuf[i] == _markerBCI0)
                        {
                            _byteNum = 1;
                        }
                        break;
                    case 1:
                        if (usbport.PortBuf[i] == _markerBCI1)
                        {
                            _byteNum = 2;
                        }
                        else
                        {
                            _byteNum = 0;
                        }
                        break;
                    case 2:
                        if (usbport.PortBuf[i] == _markerBCI2)
                        {
                            _byteNum = 3;
                        }
                        else
                        {
                            _byteNum = 0;
                        }
                        break;
                    case 3:
                        if (usbport.PortBuf[i] == _markerBCI3)
                        {
                            _byteNum = 4;
                        }
                        else
                        {
                            _byteNum = 0;
                        }
                        break;
                    case 4:
                        if (usbport.PortBuf[i] == _markerBCI4)
                        {
                            _byteNum = 5;
                        }
                        else
                        {
                            _byteNum = 0;
                        }
                        break;
                    case 5:
                        _byteNum = 6; //Циклический номер
                        break;
                    case 6:
                        _byteNum = 7; //Циклический номер
                        break;
                    case 7:
                        _byteNum = 8; //Циклический номер
                        break;
                    case 8:
                        _byteNum = 9; //Циклический номер
                        break;
                    case 9:
                        _byteNum = 10; //Таймштамп
                        break;
                    case 10:
                        _byteNum = 11; //Таймштамп
                        break;
                    case 11:
                        _byteNum = 12; //Таймштамп
                        break;
                    case 12:
                        _byteNum = 13; //Таймштамп
                        break;
                    case 13:
                        _pressureTmp = 0x10000 * (int)usbport.PortBuf[i];
                        _byteNum = 14;
                        break;
                    case 14:
                        _pressureTmp += 0x100 * (int)usbport.PortBuf[i];
                        _byteNum = 15;
                        break;
                    case 15:// 
                        _pressureTmp += (int)usbport.PortBuf[i];
                        if ((_pressureTmp & 0x800000) != 0)
                            _pressureTmp -= 0x1000000;
                        _byteNum = 16;

//                        _pressureTmp -= 1400000;

                        _data.RealTimeArray[MainIndex] = _pressureTmp;
                        if (QueueForDC.Count > 0)
                        {
                            _data.DCArray[MainIndex] = (int)QueueForDC.Average();
                        }

                        QueueForDC.Enqueue(_pressureTmp);
                        if (QueueForDC.Count > _queueforDCSize)
                        {
                            QueueForDC.Dequeue();
                        }

                        QueueForAC.Enqueue(_pressureTmp - (int)QueueForDC.Average());
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
                    default:
                        _byteNum++;
                        if (_byteNum == BytesInBlock - 1)
                        {
                            _byteNum = 0;
                        }
                        break;
                }
            }
            usbport.BytesRead = 0;
            return bytes;
        }
    }
}
