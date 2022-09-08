using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTestApp
{
    public partial class Form1
    {
        private void timerStatus_Tick(object sender, EventArgs e)
        {
            labValve1.Text = GigaDevStatus.Valve1IsClosed ? "Valve 1 : closed" : "Valve 1 : opened";
            labValve2.Text = GigaDevStatus.Valve2IsClosed ? "Valve 2 : closed" : "Valve 2 : opened";
            labPump.Text = GigaDevStatus.PumpIsOn ? "Pump : On" : "Pump : Off";
            butValve1Close.Enabled = !GigaDevStatus.Valve1IsClosed;
            butValve1Open.Enabled = GigaDevStatus.Valve1IsClosed;
            butValve2Close.Enabled = !GigaDevStatus.Valve2IsClosed;
            butValve2Open.Enabled = GigaDevStatus.Valve2IsClosed;
            butPumpOn.Enabled = !GigaDevStatus.PumpIsOn;
            butPumpOff.Enabled = GigaDevStatus.PumpIsOn;

            if (Decomposer is null)
            {
                return;
            }
            butStartRecord.Enabled = !ViewMode && !Decomposer.RecordStarted!;
            butStopRecord.Enabled = Decomposer.RecordStarted;
            butFlow.Text = ViewMode ? "Start stream" : "Stop stream";
            panelView.Enabled = ViewMode;
            //            labDeviceIsOff.Visible = !decomposer.DeviceTurnedOn;
            if (USBPort == null)
            {
                labPort.Text = "Disconnected";
                ViewMode = true;
                return;
            }
            if (USBPort.PortHandle == null)
            {
                labPort.Text = "Disconnected";
                ViewMode = true;
                return;
            }
            if (USBPort.PortHandle.IsOpen)
            {
                labPort.Text = "Connected to " + USBPort.PortNames[USBPort.CurrentPort];
            }
            else
            {
                labPort.Text = "Disconnected";
            }
        }

        private void timerRead_Tick(object sender, EventArgs e)
        {
            if (USBPort?.PortHandle?.IsOpen == true)
            {
                Decomposer?.Decompos(USBPort, TextWriter);
            }
        }

        private void timerPaint_Tick(object sender, EventArgs e)
        {
            BufPanel.Refresh();
        }


    }
}
