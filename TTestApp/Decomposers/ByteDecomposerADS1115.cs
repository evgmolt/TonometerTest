namespace TTestApp.Decomposers
{
    internal class ByteDecomposerADS1115 : ByteDecomposer
    {
        public override int SamplingFrequency => 200;
        public override int BaudRate => 115200;
        public override int BytesInPacket => 3;
        public override int MaxNoDataCounter => 10;
        public override int StartSearchMaxLevel => throw new NotImplementedException();
        public override int StopPumpingLevel => throw new NotImplementedException();

        private const int _queueForACSize = 6;
        private const int _queueForDCSize = 60;

        public ByteDecomposerADS1115(DataArrays data) : base(data, _queueForDCSize, _queueForACSize)
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

                        QueueForDC.Enqueue(tmpValue);
                        if (QueueForDC.Count > _queueForDCSize)
                        {
                            QueueForDC.Dequeue();
                        }

                        Data.RealTimeArray[MainIndex] = tmpValue - 400;
                        Data.DCArray[MainIndex] = (int)QueueForDC.Average();

                        QueueForAC.Enqueue(tmpValue - (int)QueueForDC.Average());
                        if (QueueForAC.Count > _queueForACSize)
                        {
                            QueueForAC.Dequeue();
                        }

                        Data.PressureViewArray[MainIndex] = (int)QueueForAC.Average();

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
