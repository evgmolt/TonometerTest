using System.Diagnostics;

namespace TTestApp
{
    sealed class ByteDecomposerBCI : ByteDecomposer
    {
        public override int SamplingFrequency => 250;

        public override int BaudRate => 460800;

        public override int BytesInPacket => 65;

        public override int MaxNoDataCounter => 100;

        private const byte _markerBCI0 = 0xAA;
        private const byte _markerBCI1 = 0x55;
        private const byte _markerBCI2 = 0x66;
        private const byte _markerBCI3 = 0x77;
        private const byte _markerBCI4 = 0xA3;

        //Размер очередей для усреднения скользящим окном
        //AC - для сигнала
        //DC - для получения постоянной составляющей
        private const int _queueForACSize = 6;
        private const int _queueForDCSize = 100;
        public ByteDecomposerBCI(DataArrays data) : base(data, _queueForDCSize, _queueForACSize)
        {
        }

        public override int Decompos(USBserialPort usbport, Stream saveFileStream, StreamWriter txtFileStream)
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
                    MessageBox.Show("Save file stream error. " + ex.Message);
                    Debug.WriteLine(ex.Message);
                }
            }
            for (int i = 0; i < bytes; i++)
            {
                switch (byteNum)
                {
                    case 0:// MarkerBCI
                        if (usbport.PortBuf[i] == _markerBCI0)
                        {
                            byteNum = 1;
                        }
                        break;
                    case 1:
                        if (usbport.PortBuf[i] == _markerBCI1)
                        {
                            byteNum = 2;
                        }
                        else
                        {
                            byteNum = 0;
                        }
                        break;
                    case 2:
                        if (usbport.PortBuf[i] == _markerBCI2)
                        {
                            byteNum = 3;
                        }
                        else
                        {
                            byteNum = 0;
                        }
                        break;
                    case 3:
                        if (usbport.PortBuf[i] == _markerBCI3)
                        {
                            byteNum = 4;
                        }
                        else
                        {
                            byteNum = 0;
                        }
                        break;
                    case 4:
                        if (usbport.PortBuf[i] == _markerBCI4)
                        {
                            byteNum = 5;
                        }
                        else
                        {
                            byteNum = 0;
                        }
                        break;
                    case 5:
                        byteNum = 6; //Циклический номер
                        break;
                    case 6:
                        byteNum = 7; //Циклический номер
                        break;
                    case 7:
                        byteNum = 8; //Циклический номер
                        break;
                    case 8:
                        byteNum = 9; //Циклический номер
                        break;
                    case 9:
                        byteNum = 10; //Таймштамп
                        break;
                    case 10:
                        byteNum = 11; //Таймштамп
                        break;
                    case 11:
                        byteNum = 12; //Таймштамп
                        break;
                    case 12:
                        byteNum = 13; //Таймштамп
                        break;
                    case 13:
                        tmpValue = 0x10000 * (int)usbport.PortBuf[i];
                        byteNum = 14;
                        break;
                    case 14:
                        tmpValue += 0x100 * (int)usbport.PortBuf[i];
                        byteNum = 15;
                        break;
                    case 15:// 
                        tmpValue += (int)usbport.PortBuf[i];
                        if ((tmpValue & 0x800000) != 0)
                            tmpValue -= 0x1000000;
                        byteNum = 16;

                        tmpValue -= 273500;

                        data.RealTimeArray[MainIndex] = tmpValue;
                        QueueForDC.Enqueue(tmpValue);
                        if (QueueForDC.Count > _queueForDCSize)
                        {
                            QueueForDC.Dequeue();
                            data.DCArray[MainIndex] = (int)QueueForDC.Average();
                        }

                        QueueForAC.Enqueue(tmpValue - (int)QueueForDC.Average());
                        if (QueueForAC.Count > _queueForACSize)
                        {
                            QueueForAC.Dequeue();
                        }

                        data.PressureViewArray[MainIndex] = (int)QueueForAC.Average();

                        byteNum = 0;

                        if (RecordStarted)
                        {
                            txtFileStream.WriteLine(data.GetDataString(MainIndex));
                        }
                        OnDecomposeLineEvent();
                        PacketCounter++;
                        MainIndex++;
                        MainIndex &= (DataArrSize - 1);
                        break;
                    default:
                        byteNum++;
                        if (byteNum == BytesInPacket - 1)
                        {
                            byteNum = 0;
                        }
                        break;
                }
            }
            usbport.BytesRead = 0;
            return bytes;
        }
    }
}
