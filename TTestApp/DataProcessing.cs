namespace TTestApp
{
    internal static class DataProcessing
    {
        public static int ValueToMmhG(double value)
        {
            double pressure = value / 19.87;
            return (int)pressure;
        }
    }
}
