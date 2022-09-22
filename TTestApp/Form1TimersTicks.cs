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
            labValve1.Text = DevStatus.Valve1IsClosed ? "Valve 1 : closed" : "Valve 1 : opened";
            labValve2.Text = DevStatus.Valve2IsClosed ? "Valve 2 : closed" : "Valve 2 : opened";
            labPump.Text = DevStatus.PumpIsOn ? "Pump : On" : "Pump : Off";
            butValve1Close.Enabled = !DevStatus.Valve1IsClosed;
            butValve1Open.Enabled = DevStatus.Valve1IsClosed;
            butValve2Close.Enabled = !DevStatus.Valve2IsClosed;
            butValve2Open.Enabled = DevStatus.Valve2IsClosed;
            butPumpOn.Enabled = !DevStatus.PumpIsOn;
            butPumpOff.Enabled = DevStatus.PumpIsOn;

            if (Decomposer is null)
            {
                return;
            }
            butStartRecord.Enabled = !ViewMode && !Decomposer.RecordStarted!;
            butStopRecord.Enabled = Decomposer.RecordStarted;
            butSaveFile.Enabled = ViewMode && Decomposer.PacketCounter != 0;
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

        private void timerDetectRate_Tick(object sender, EventArgs e)
        {
            Decomposer.SamplingFrequency = Decomposer.PacketCounter / 10;
            timerDetectRate.Enabled = false;
            labelRate.Text = "Sample rate : " + Decomposer.SamplingFrequency.ToString();
        }
    }
}
