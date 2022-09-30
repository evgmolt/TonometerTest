using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTestApp.Commands
{
    internal struct DeviceStatus
    {
        public bool ValveSlowClosed;
        public bool ValveFastClosed;
        public bool PumpIsOn;
    }
}
