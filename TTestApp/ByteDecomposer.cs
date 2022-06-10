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
        public const int DataArrSizeForView = 4000;

        public DataArrays Data;
        public event EventHandler DecomposeLineEvent;
        public event EventHandler ConnectionBreakdown;
        public const int SamplingFrequency = 200;
        public const int BytesInBlock = 27;


        public uint MainIndex = 0;
        public int LineCounter = 0;
        public int TotalBytes;

        public bool RecordStarted;
        public bool DeviceTurnedOn;

        private const byte _marker1 = 25;

        private int _pressureTmp;


        private const int _maxNoDataCounter = 10;
        private int _noDataCounter;

        private int _byteNum;

        private const int MaxDelayCounter = 300;
        const double filterCoeff = 0.005;

        private bool FilterOn = true;

        private const int averSize = 50;
        Queue<int> averQ = new Queue<int>(averSize);

        public ByteDecomposer(DataArrays data)
        {
            Data = data;
            RecordStarted = false;
            DeviceTurnedOn = true;
            TotalBytes = 0;
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
            if (saveFileStream != null & RecordStarted)
            {
                try
                {
                    saveFileStream.Write(usbport.PortBuf, 0, bytes);
                    TotalBytes += bytes;
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

                        Data.RealTimeArray[MainIndex] = _pressureTmp;
                        Data.DCRealTimeArray[MainIndex] = (int)averQ.Average();
                        Data.PressureViewArray[MainIndex] = Filter.FilterForRun(Filter.coeff50, Data.RealTimeArray, MainIndex);

                        _byteNum = 0;

                        if (RecordStarted)
                        {
                            txtFileStream.WriteLine(Data.GetDataString(MainIndex));
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
