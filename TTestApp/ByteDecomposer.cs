using System.Diagnostics;

namespace TTestApp
{
    abstract class ByteDecomposer
    {
        public const int DataArrSize = 0x100000;

        public abstract int SamplingFrequency { get; }
        public abstract int BaudRate { get; }
        public abstract int BytesInBlock { get; }
        public abstract int MaxNoDataCounter { get; }

        protected const byte _marker1 = 25;

        protected DataArrays _data;

        public event EventHandler? DecomposeLineEvent;

        public uint MainIndex = 0;
        public int LineCounter = 0;

        public bool RecordStarted;
        public bool DeviceTurnedOn;

        protected int _pressureTmp;

        protected int _noDataCounter;

        protected int _byteNum;

        protected Queue<int> QueueForDC;

        protected Queue<int> QueueForAC;

        public ByteDecomposer(DataArrays data, int sizeQforDC, int sizeQforAC)
        {
            _data = data;
            RecordStarted = false;
            DeviceTurnedOn = true;
            MainIndex = 0;
            _noDataCounter = 0;
            _byteNum = 0;
            QueueForDC = new Queue<int>(sizeQforDC);
            QueueForAC = new Queue<int>(sizeQforAC);
        }

        protected virtual void OnDecomposeLineEvent()
        {
            DecomposeLineEvent?.Invoke(this, EventArgs.Empty);
        }

        public abstract int Decompos(USBserialPort usbport, Stream saveFileStream, StreamWriter txtFileStream);
    }
}
