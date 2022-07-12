using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TTestApp
{
    class ByteDecomposerBCI
    {
        public int Count = 0;

        public const int DataArrSize = 0x100000;
        public const int SamplingFrequency = 250;
        public const int BytesInBlock = 65;
        private const byte _marker0 = 0xAA;
        private const byte _marker1 = 0x55;
        private const byte _marker2 = 0x66;
        private const byte _marker3 = 0x77;
        private const byte _marker4 = 0xA3;


        private DataArrays _data;

        public event EventHandler DecomposeLineEvent;
        public event EventHandler ConnectionBreakdown;

        public uint MainIndex = 0;
        public int LineCounter = 0;

        public bool RecordStarted;
        public bool DeviceTurnedOn;

        private int _pressureTmp;

        private const int _maxNoDataCounter = 10;
        private int _noDataCounter;

        private int _byteNum;

        private const int averSize = 200;
        Queue<int> averQ = new Queue<int>(averSize);

        private const int averViewSize = 60;
        Queue<int> averViewQ = new Queue<int>(averViewSize);

        public ByteDecomposerBCI(DataArrays data)
        {
            _data = data;
            RecordStarted = false;
            DeviceTurnedOn = true;
            MainIndex = 0;
            _noDataCounter = 0;
            _byteNum = 0;
        }

        protected virtual void OnDecomposeLineEvent()
        {
            DecomposeLineEvent?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnConnectionBreakdown()
        {
            ConnectionBreakdown?.Invoke(this, EventArgs.Empty);
        }

        public int Decompos(USBserialPort usbport)
        {
            return Decompos(usbport, null, null);
        }

        public int Decompos(USBserialPort usbport, Stream saveFileStream)
        {
            return Decompos(usbport, saveFileStream, null);
        }

        public int Decompos(USBserialPort usbport, Stream saveFileStream, StreamWriter txtFileStream)
        {
            int bytes = usbport.BytesRead;
            if (bytes == 0)
            {
                _noDataCounter++;
                if (_noDataCounter > _maxNoDataCounter)
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
                        if (usbport.PortBuf[i] == _marker0)
                        {
                            _byteNum = 1;
                        }
                        break;
                    case 1:
                        if (usbport.PortBuf[i] == _marker1)
                        {
                            _byteNum = 2;
                        }
                        else
                        {
                            _byteNum = 0;
                        }
                        break;
                    case 2:
                        if (usbport.PortBuf[i] == _marker2)
                        {
                            _byteNum = 3;
                        }
                        else
                        {
                            _byteNum = 0;
                        }
                        break;
                    case 3:
                        if (usbport.PortBuf[i] == _marker3)
                        {
                            _byteNum = 4;
                        }
                        else
                        {
                            _byteNum = 0;
                        }
                        break;
                    case 4:
                        if (usbport.PortBuf[i] == _marker4)
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
                        if (averQ.Count > 0)
                        {
                            _data.DCArray[MainIndex] = (int)averQ.Average();
                        }

                        averQ.Enqueue(_pressureTmp);
                        if (averQ.Count > averSize)
                        {
                            averQ.Dequeue();
                        }

                        averViewQ.Enqueue(_pressureTmp - (int)averQ.Average());
                        if (averViewQ.Count > averViewSize)
                        {
                            averViewQ.Dequeue();
                        }

                        _data.PressureViewArray[MainIndex] = (int)averViewQ.Average() + 5000;

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
