using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTestApp
{
    internal class ConverterValueToMmHg
    {
        private double Coeff = CoeffB;
        private const double CoeffA = 59021;
        private const double CoeffB = 55736;
        private const double CoeffTonometer = 43215;
        private const double _pressure = 2210;

        public EventHandler<ConverterValueToMmHgEventArgs> CoeffChanged;

        public void ChangeCoeff(char filePrefix)
        {
            Coeff = filePrefix switch
            {
                'A' => CoeffA,
                'B' => CoeffB,
                _ => CoeffTonometer
            };
            CoeffChanged(this, new ConverterValueToMmHgEventArgs { Coeff = Coeff });
        }
        public int Convert(double value) => (int)(value / (Coeff / _pressure));
    }
}
