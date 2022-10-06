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
            PressureMeasStatus = (int)PressureMeasurementStatus.Ready;
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
            formPatientData.Dispose();
            BufPanel.Refresh();
        }

        private void butStartRecord_Click(object sender, EventArgs e)
        {
            FormPatientData formPatientData = new(null);
            formPatientData.SetStateBeforeRecord();
            if (formPatientData.ShowDialog() == DialogResult.OK)
            {
                CurrentPatient = formPatientData.patient;
                TextWriter = new StreamWriter(Cfg.DataDir + TmpDataFile);
                Decomposer.PacketCounter = 0;
                Decomposer.MainIndex = 0;
                progressBarRecord.Visible = true;
                Detector = new WaveDetector(Decomposer.SamplingFrequency);
                FileNum++;
                PressureMeasStatus = (int)PressureMeasurementStatus.Calibration;
            }
            formPatientData.Dispose();
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
    }
}
