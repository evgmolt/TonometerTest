using Microsoft.Win32;
using System.IO.Ports;

namespace TTestApp
{
    public interface IMessageHandler
    {
        event Action<Message> WindowsMessage;
    }

    public class USBserialPort: IMessageHandler
    {
        private const int _USBTimerInterval = 25;
        public SerialPort PortHandle;
        public string[] PortNames;
        public byte[] PortBuf;
        public int CurrentPort;
        public int BytesRead;
        public Boolean ReadEnabled;
        readonly System.Threading.Timer ReadTimer;

        private readonly int _portBufSize = 10000;
        private readonly int _baudRate;
        private readonly string _connectString;

        public event Action<Exception> ConnectionFailure;
        public event Action ConnectionOk;
        public event Action<Message> WindowsMessage;

        
        public USBserialPort(IMessageHandler messageHandler, int baudrate)
        {
            _baudRate = baudrate;
            _connectString = "USBSER";
            messageHandler.WindowsMessage += OnMessage;
            ReadEnabled = false;
            PortBuf = new byte[_portBufSize];
            ReadTimer = new System.Threading.Timer(ReadPort, null, 0, Timeout.Infinite);
        }

        private void ReadPort(object state)
        {
            if (BytesRead > 0) return;
            if (!ReadEnabled) return;
            if (PortHandle == null) return;
            if (PortHandle.IsOpen)
            {
                if (PortHandle.BytesToRead > 0)
                {
                    int bytesToRead = PortHandle.BytesToRead;
                    if (bytesToRead > PortBuf.Length)
                    {
                        bytesToRead = PortBuf.Length;
                    }
                    try
                    {
                        BytesRead = PortHandle.Read(PortBuf, 0, bytesToRead);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public bool WriteBuf(byte[] buf)
        {
            try
            {
                if (PortHandle != null)
                {
                    PortHandle.Write(buf, 0, buf.Length);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool WriteByte(byte b)
        {
            byte[] buf = { b }; // new byte[1];
            try
            {
                if (PortHandle != null)
                {
                    PortHandle.Write(buf, 0, 1);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
       
        public void Connect()
        {
            PortNames = GetPortsNames();
            if (PortNames == null) return;
            for (int i = 0; i < PortNames.Length; i++)
            {
                PortHandle = new SerialPort(PortNames[i], _baudRate)
                {
                    DataBits = 8,
                    Parity = Parity.None,
                    StopBits = StopBits.One
                };
                try
                {
                    PortHandle.Open();
                    ReadEnabled = true;
                    ReadTimer.Change(0, _USBTimerInterval);
                    CurrentPort = i;
                    ConnectionOk?.Invoke();
                    break;
                }
                catch (Exception e)
                {
                    ConnectionFailure?.Invoke(e);
                }
            }
        }

        private string[] GetPortsNames()
        {
            RegistryKey r_hklm = Registry.LocalMachine;
            RegistryKey r_hard = r_hklm.OpenSubKey("HARDWARE");
            RegistryKey r_device = r_hard.OpenSubKey("DEVICEMAP");
            RegistryKey r_port = r_device.OpenSubKey("SERIALCOMM");
            if (r_port == null) 
            {
                return null;
            }
            string[] portvalues = r_port.GetValueNames();
            List<string> portNames = new List<string>();
            int Ind = 0;
            for (int i = 0; i < portvalues.Length; i++)
            {
                if (portvalues[i].IndexOf(_connectString) >= 0)
                {
                    portNames.Add((string)r_port.GetValue(portvalues[i]));
                    Ind++;
                }
            }
            return portNames.ToArray();
        }

        private void OnMessage(Message m)
        {
            if (PortHandle == null)
            {
                BytesRead = 0;
                Connect();
            }
            else
            if (!PortHandle.IsOpen)
            {
                BytesRead = 0;
                Connect();
            }
        }
    }
}
