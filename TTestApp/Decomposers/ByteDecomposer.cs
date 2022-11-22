using System.Diagnostics;

namespace TTestApp.Decomposers
{
    internal class ByteDecomposer
    {
        public const int DataArrSize = 0x100000;
        public int SamplingFrequency = 240;
        public int BaudRate = 115200;
        private const byte marker1 = 0x19;

        protected DataArrays Data;

        public event EventHandler<PacketEventArgs>? OnDecomposePacketEvent;

        public uint MainIndex = 0;
        public int PacketCounter = 0;
        public bool RecordStarted;

        protected int tmpValue1;
        protected int tmpValue2;

        protected int byteNum;

        public ByteDecomposer(DataArrays data)
        {
            Data = data;
            RecordStarted = false;
            MainIndex = 0;
            byteNum = 0;
        }

        protected virtual void OnDecomposeLineEvent()
        {
            OnDecomposePacketEvent?.Invoke(
                this,
                new PacketEventArgs
                {
                    RealTimeValue = Data.RealTimeArray[MainIndex],
                    EnvelopeValue = Data.EnvelopeArray[MainIndex],
                    PacketCounter = PacketCounter,
                    MainIndex = MainIndex
                });
        }

        public int Decompos(USBserialPort usbport, StreamWriter saveFileStream)
        {
            return Decompos(usbport, null, saveFileStream);
        }

        public int Decompos(USBserialPort usbport, Stream saveFileStream, StreamWriter txtFileStream)
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
                switch (byteNum)
                {
                    case 0:// Marker
                        if (usbport.PortBuf[i] == marker1)
                        {
                            byteNum = 1;
                        }
                        break;
                    case 1:
                        tmpValue1 = usbport.PortBuf[i];
                        byteNum = 2;
                        break;
                    case 2:
                        tmpValue1 += 0x100 * usbport.PortBuf[i];
                        if ((tmpValue1 & 0x8000) != 0)
                        {
                            tmpValue1 -= 0x10000;
                        }

                        Data.RealTimeArray[MainIndex] = tmpValue1;
                        byteNum = 3;
                        break;
                    case 3:
                        tmpValue2 = usbport.PortBuf[i];
                        byteNum = 4;
                        break;
                    case 4:
                        tmpValue2 += 0x100 * usbport.PortBuf[i];
                        if ((tmpValue2 & 0x8000) != 0)
                        {
                            tmpValue2 -= 0x10000;
                        }

                        Data.EnvelopeArray[MainIndex] = tmpValue2;
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
