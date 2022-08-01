namespace TTestApp
{
    internal class Painter
    {
        private readonly Control _control;
        private readonly ByteDecomposerBCI _decomposer;
        private readonly Color[] curveColors = { Color.Red, Color.Blue, Color.Green, Color.Aqua };
        private readonly Color[] visirsColors = { Color.LightGray, Color.Brown, Color.Chocolate };

        public Painter(Control control, ByteDecomposerBCI decomposer)
        {
            _control = control;
            _decomposer = decomposer;
        }   

        public void Paint(
            bool ViewMode,
            int ViewShift,
            List<double[]> data,
            List<int[]> visirs,
            double ScaleY,
            int MaxSize,
            PaintEventArgs e)
        {
            if (data == null)
            {
                return;
            }
            if (data.Count == 0)
            {
                return;
            }
            float tension = 0.1F;
            var R0 = new Rectangle(0, 0, _control.Width - 1, _control.Height - 1);
            var pen0 = new Pen(Color.Black, 1);
            e.Graphics.Clear(Color.White);
            e.Graphics.DrawRectangle(pen0, R0);
            if (!ViewMode)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    var pen = new Pen(curveColors[i], 1);
                    Point[] OutArray = ViewArrayMaker.MakeArray(
                                                            _control,
                                                            data[i],
                                                            _decomposer.MainIndex,
                                                            MaxSize,
                                                            ScaleY);
                    e.Graphics.DrawCurve(pen, OutArray, tension);
                    pen.Dispose();
                }
            }
            else //Режим просмотра
            {
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i] == null) break;
                    var pen = new Pen(curveColors[i], 1);

                    Point[] OutArray = ViewArrayMaker.MakeArrayForView(
                                                                    _control,
                                                                    data[i],
                                                                    ViewShift,
                                                                    MaxSize,
                                                                    ScaleY,
                                                                    1);
                    e.Graphics.DrawCurve(pen, OutArray, tension);
                    pen.Dispose();
                }
                int X1 = ViewShift;
                int X2 = _control.Width + ViewShift;
                for (int i = 0; i < visirs.Count; i++)
                {
                    if (visirs[i] == null) break;
                    var pen = new Pen(visirsColors[i], 1);
                    for (int j = 0; j < visirs[i].Length; j++)
                    {
                        if (visirs[i][j] > X1 && visirs[i][j] < X2)
                        {
                            e.Graphics.DrawLine(pen, visirs[i][j] - ViewShift, 0, visirs[i][j] - ViewShift, _control.Width);
                        }
                    }
                    pen.Dispose();
                }
                int index = 1;
                int timeTickSize = 10;
                while (index * ByteDecomposer.SamplingFrequency < _control.Width)
                {
                    e.Graphics.DrawLine(
                        pen0,
                        index * ByteDecomposer.SamplingFrequency,
                        _control.Height,
                        index * ByteDecomposer.SamplingFrequency,
                        _control.Height - timeTickSize);
                    index++;
                }
            }
            pen0.Dispose();
        }
    }
}
