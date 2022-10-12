using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTestApp.Commands;

namespace TTestApp
{
    public partial class Form1
    {
        private void timerStatus_Tick(object sender, EventArgs e)
        {
            labValve1.Text = DevStatus.ValveSlowClosed ? "Valve 1 (Slow) : closed" : "Valve 1 (Slow) : opened";
            labValve2.Text = DevStatus.ValveFastClosed ? "Valve 2 (Fast) : closed" : "Valve 2 (Fast) : opened";
            labPump.Text = DevStatus.PumpIsOn ? "Pump : On" : "Pump : Off";
            butValveSlowClose.Enabled = !DevStatus.ValveSlowClosed;
            butValveSlowOpen.Enabled = DevStatus.ValveSlowClosed;
            butValveFastClose.Enabled = !DevStatus.ValveFastClosed;
            butValveFastOpen.Enabled = DevStatus.ValveFastClosed;
            butValvesOpen.Enabled = DevStatus.ValveFastClosed || DevStatus.ValveSlowClosed;
            butValvesClose.Enabled = !DevStatus.ValveFastClosed || !DevStatus.ValveSlowClosed;

            butPumpOn.Enabled = !DevStatus.PumpIsOn;
            butPumpOff.Enabled = DevStatus.PumpIsOn;

            if (Decomposer is null)
            {
                return;
            }
            butStartMeas.Enabled = !ViewMode && !Decomposer.RecordStarted!;
            butStopRecord.Enabled = Decomposer.RecordStarted;
            butSaveFile.Enabled = ViewMode && Decomposer.PacketCounter != 0;
            butFlow.Text = ViewMode ? "Start stream" : "Stop stream";
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
