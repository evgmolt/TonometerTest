using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTestApp.Commands
{
    internal struct SF3Status
    {
        public bool Valve1IsClosed;
        public bool Valve1PWM;
        public bool Valve2IsClosed;
        public bool PumpIsOn;
    }
}
