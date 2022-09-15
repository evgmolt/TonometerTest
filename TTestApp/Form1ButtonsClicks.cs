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

            FormPatientData formPatientData = new FormPatientData(CurrentPatient);
            formPatientData.SetStateAfterRecord();
            if (formPatientData.ShowDialog() == DialogResult.OK)
            {
                CurrentPatient = formPatientData.patient;
                SaveFile();
            }

            PrepareData();
            BufPanel.Refresh();
        }

        private void butStartRecord_Click(object sender, EventArgs e)
        {
            FormPatientData formPatientData = new FormPatientData(null);
            formPatientData.SetStateBeforeRecord();
            if (formPatientData.ShowDialog() == DialogResult.OK)
            {
                CurrentPatient = formPatientData.patient;
                TextWriter = new StreamWriter(Cfg.DataDir + TmpDataFile);
                Decomposer.PacketCounter = 0;
                Decomposer.MainIndex = 0;
                Decomposer.RecordStarted = true;
                progressBarRecord.Visible = true;
                Detector = new WaveDetector(Decomposer.SamplingFrequency);
                FileNum++;
                PressureMeasStatus = (int)PressureMeasurementStatus.Calibration;
            }
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
        }

        private void SaveFile()
        {
            Cfg.DataFileNum++;
            TTestConfig.SaveConfig(Cfg);
            CurrentFile = Cfg.Prefix + Cfg.DataFileNum.ToString().PadLeft(5, '0') + ".txt";
            if (File.Exists(Cfg.DataDir + CurrentFile))
            {
                File.Delete(Cfg.DataDir + CurrentFile);
            }
            var DataStrings = File.ReadAllLines(Cfg.DataDir + TmpDataFile);
            File.WriteAllLines(Cfg.DataDir + CurrentFile, CurrentPatient.ToArray());
            File.AppendAllLines(Cfg.DataDir + CurrentFile, DataStrings);
            Text = "Pulse wave recorder. File : " + CurrentFile;
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
