namespace TTestApp
{
    internal class PacketEventArgs : EventArgs
    {
        public uint MainIndex { get; set; }
        public int PacketCounter { get; set; }
        public double RealTimeValue { get; set; }
    }
}
