namespace TTestApp
{
    internal class WaveDetectorEventArgs : EventArgs
    {
        public int WaveCount { get; set; }
        public int Interval { get; set; }
        public double Amplitude { get; set; }
        public int ArrhythmiaCount { get; set; }
    }
}
