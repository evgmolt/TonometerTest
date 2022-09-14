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
        private void butPressureMeasStart_Click(object sender, EventArgs e)
        {
            GigaDevStatus.Valve1IsClosed = true;
            GigaDevStatus.Valve2IsClosed = true;
            GigaDevStatus.PumpIsOn = true;
            PumpStatus = (int)PumpingStatus.MaximumSearch;
            USBPort.WriteByte((byte)CmdGigaDevice.Valve1Close);
            USBPort.WriteByte((byte)CmdGigaDevice.Valve2Close);
            USBPort.WriteByte((byte)CmdGigaDevice.PumpSwitchOn);
            PressureMeasStatus = (int)PressureMeasurementStatus.Calibration;
            labMeasInProgress.Visible = true;
        }

        private void butPressureMeasAbort_Click(object sender, EventArgs e)
        {
            GigaDevStatus.Valve1IsClosed = false;
            GigaDevStatus.Valve2IsClosed = false;
            GigaDevStatus.PumpIsOn = false;
            PumpStatus = (int)PumpingStatus.Ready;
            USBPort.WriteByte((byte)CmdGigaDevice.Valve1Open);
            USBPort.WriteByte((byte)CmdGigaDevice.Valve2Open);
            USBPort.WriteByte((byte)CmdGigaDevice.PumpSwitchOff);
            PressureMeasStatus = (int)PressureMeasurementStatus.Ready;
            labMeasInProgress.Visible = false;
        }

        private void butValve1Open_Click(object sender, EventArgs e)
        {
            GigaDevStatus.Valve1IsClosed = false;
            USBPort.WriteByte((byte)CmdGigaDevice.Valve1Open);
        }

        private void butValve1Close_Click(object sender, EventArgs e)
        {
            GigaDevStatus.Valve1IsClosed = true;
            USBPort.WriteByte((byte)CmdGigaDevice.Valve1Close);
        }

        private void butValve2Open_Click(object sender, EventArgs e)
        {
            GigaDevStatus.Valve2IsClosed = false;
            USBPort.WriteByte((byte)CmdGigaDevice.Valve2Open);
        }

        private void butValve2Close_Click(object sender, EventArgs e)
        {
            GigaDevStatus.Valve2IsClosed = true;
            USBPort.WriteByte((byte)CmdGigaDevice.Valve2Close);
        }

        private void butPumpOn_Click(object sender, EventArgs e)
        {
            GigaDevStatus.PumpIsOn = true;
            USBPort.WriteByte((byte)CmdGigaDevice.PumpSwitchOn);
        }

        private void butPumpOff_Click(object sender, EventArgs e)
        {
            GigaDevStatus.PumpIsOn = false;
            USBPort.WriteByte((byte)CmdGigaDevice.PumpSwitchOff);
        }

        private void butValve1PWM_Click(object sender, EventArgs e)
        {
            GigaDevStatus.Valve1PWM = true;
            USBPort.WriteByte((byte)CmdGigaDevice.Valve1PWMOn);
        }
        private void butStopRecord_Click(object sender, EventArgs e)
        {
            labArrythmia.Text = Detector.Arrythmia.ToString();
            Detector.OnWaveDetected -= NewWaveDetected;
            Detector = null;
            progressBarRecord.Visible = false;
            Decomposer.OnDecomposePacketEvent -= OnPacketReceived;
            Decomposer.RecordStarted = false;
            TextWriter?.Dispose();
            ViewMode = true;
            timerPaint.Enabled = !ViewMode;
            timerRead.Enabled = false;

            CurrentFileSize = Decomposer.PacketCounter;
            labRecordSize.Text = "Record size : " + (CurrentFileSize / Decomposer.SamplingFrequency).ToString() + " s";
            UpdateScrollBar(CurrentFileSize);

            PrepareData();
            BufPanel.Refresh();
            controlPanel.Refresh();
            //            ReadFile(Cfg.DataDir + TmpDataFile);
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
            FileNum++;
            timerDetectRate.Enabled = true;
            PressureMeasStatus = (int)PressureMeasurementStatus.Calibration;
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
