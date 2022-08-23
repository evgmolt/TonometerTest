using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTestApp
{
    internal class WaveDetectorEventArgs : EventArgs
    {
        public int WaveCount { get; set; }
        public double DerivValue { get; set; }
    }
}
