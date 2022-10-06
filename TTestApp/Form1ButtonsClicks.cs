using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTestApp.Commands;
using TTestApp.Enums;

namespace TTestApp
{
    public partial class Form1
    {
        private void button1_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte((byte)CmdDevice.StartReading);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte((byte)CmdDevice.StopReading);
        }

        private void butPressureMeasAbort_Click(object sender, EventArgs e)
        {
            DevStatus.ValveSlowClosed = false;
            DevStatus.ValveFastClosed = false;
            DevStatus.PumpIsOn = false;
            PumpStatus = PumpingStatus.Ready;
            USBPort.WriteByte((byte)CmdDevice.ValveFastOpen);
            USBPort.WriteByte((byte)CmdDevice.ValveSlowOpen);
            USBPort.WriteByte((byte)CmdDevice.PumpSwitchOff);
            PressureMeasStatus = PressureMeasurementStatus.Ready;
            PumpStatus = PumpingStatus.Ready;
            butStopRecord_Click(butPressureMeasAbort, e);
        }

        private void butValve2Open_Click(object sender, EventArgs e)
        {
            DevStatus.ValveFastClosed = false;
            USBPort.WriteByte((byte)CmdDevice.ValveFastOpen);
        }

        private void butValve2Close_Click(object sender, EventArgs e)
        {
            DevStatus.ValveFastClosed = true;
            USBPort.WriteByte((byte)CmdDevice.ValveFastClose);
        }

        private void butValve1Open_Click(object sender, EventArgs e)
        {
            DevStatus.ValveSlowClosed = false;
            USBPort.WriteByte((byte)CmdDevice.ValveSlowOpen);
        }

        private void butValve1Close_Click(object sender, EventArgs e)
        {
            DevStatus.ValveSlowClosed = true;
            USBPort.WriteByte((byte)CmdDevice.ValveSlowClose);
        }

        private void butPumpOn_Click(object sender, EventArgs e)
        {
            DevStatus.PumpIsOn = true;
            USBPort.WriteByte((byte)CmdDevice.PumpSwitchOn);
        }

        private void butPumpOff_Click(object sender, EventArgs e)
        {
            DevStatus.PumpIsOn = false;
            USBPort.WriteByte((byte)CmdDevice.PumpSwitchOff);
        }

        private void butStopRecord_Click(object sender, EventArgs e)
        {
            labArrythmia.Text = Detector?.Arrythmia.ToString();
//            Detector.OnWaveDetected -= NewWaveDetected;
            Detector = null;
            progressBarRecord.Visible = false;
            Decomposer.OnDecomposePacketEvent -= OnPacketReceived;
            Decomposer.RecordStarted = false;
            TextWriter?.Dispose();

            CurrentFileSize = Decomposer.PacketCounter;
            labRecordSize.Text = "Record size : " + (CurrentFileSize / Decomposer.SamplingFrequency).ToString() + " s";
            UpdateScrollBar(CurrentFileSize);
            PressureMeasStatus = PressureMeasurementStatus.Ready;
            PumpStatus = PumpingStatus.Ready;
            ViewMode = true;
            timerPaint.Enabled = !ViewMode;
            timerRead.Enabled = false;
            PrepareData();
            BufPanel.Refresh();
            controlPanel.Refresh();
        }

        private void butStartRecord_Click(object sender, EventArgs e)
        {
            TextWriter = new StreamWriter(Cfg.DataDir + TmpDataFile);
            Decomposer.PacketCounter = 0;
            Decomposer.MainIndex = 0;
            Decomposer.RecordStarted = true;
            progressBarRecord.Visible = true;
            labMeanPressure.Text = "Mean : ";
            labSys.Text = "Sys : ";
            labDia.Text = "Dia : ";
            labPulse.Text = "Pulse : ";
            Detector = new WaveDetector(Decomposer.SamplingFrequency);
            Detector.OnWaveDetected += NewWaveDetected;

            DevStatus.ValveSlowClosed = true;
            DevStatus.ValveFastClosed = true;
            DevStatus.PumpIsOn = true;
            PumpStatus = PumpingStatus.WaitingForLevel;
            PressureMeasStatus = PressureMeasurementStatus.Calibration;
            USBPort.WriteByte((byte)CmdDevice.StartReading);
        }

        private void butFlow_Click(object sender, EventArgs e)
        {
            ViewMode = !ViewMode;
            timerRead.Enabled = !ViewMode;
            timerPaint.Enabled = !ViewMode;
            if (!ViewMode)
            {
                InitArraysForFlow();
                hScrollBar1.Visible = false;
            }
        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            PrepareData();
            BufPanel.Refresh();
            controlPanel.Refresh();
        }

        private void butSaveFile_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = Cfg.DataDir.ToString();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Cfg.DataDir = Path.GetDirectoryName(saveFileDialog1.FileName) + @"\";
                TTestConfig.SaveConfig(Cfg);
                CurrentFile = Path.GetFileName(saveFileDialog1.FileName);
                if (File.Exists(Cfg.DataDir + CurrentFile))
                {
                    File.Delete(Cfg.DataDir + CurrentFile);
                }
                File.Move(saveFileDialog1.InitialDirectory + TmpDataFile, Cfg.DataDir + CurrentFile);
                Text = "File : " + CurrentFile;
            }
        }

        private void butOpenFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.InitialDirectory = Cfg.DataDir.ToString();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ViewMode = true;
                if (File.Exists(openFileDialog1.FileName))
                {
                    Cfg.DataDir = Path.GetDirectoryName(openFileDialog1.FileName) + @"\";
                    TTestConfig.SaveConfig(Cfg);
                    timerRead.Enabled = false;

                    CurrentFile = Path.GetFileName(openFileDialog1.FileName);
                    ReadFile(Cfg.DataDir + CurrentFile);
                }
            }
            Text = "File : " + CurrentFile;
        }
    }
}
