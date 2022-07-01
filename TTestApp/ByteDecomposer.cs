using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TTestApp
{
    class ByteDecomposer
    {
        public const int DataArrSize = 0x100000;
        public const int SamplingFrequency = 200;
        public const int BytesInBlock = 3;
        private const byte _marker1 = 25;
        const double filterCoeff = 0.01;

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

        private const int averSize = 60;
        Queue<int> averQ = new Queue<int>(averSize);

        private const int averViewSize = 20;
        Queue<int> averViewQ = new Queue<int>(averViewSize);

        public ByteDecomposer(DataArrays data)
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
            if (bytes==0)
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

                        averQ.Enqueue(_pressureTmp);
                        if (averQ.Count > averSize)
                        {
                            averQ.Dequeue();
                        }

                        _data.RealTimeArray[MainIndex] = _pressureTmp;
                        _data.DCArray[MainIndex] = (int)averQ.Average();

                        averViewQ.Enqueue(100 + _pressureTmp - (int)averQ.Average());
                        if (averViewQ.Count > averViewSize)
                        {
                            averViewQ.Dequeue();
                        }

                        _data.PressureViewArray[MainIndex] = (int)averViewQ.Average();

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
