using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTestApp
{
    internal class PacketEventArgs : EventArgs
    {
        public uint MainIndex { get; set; }
        public double RealTimeValue { get; set; }
        public double PressureViewValue { get; set; }
        public double DCValue { get; set; }
        public double DerivValue { get; set; }
    }
}
