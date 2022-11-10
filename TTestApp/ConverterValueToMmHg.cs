using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTestApp
{
    internal class ConverterValueToMmHg
    {
        public double Coeff = CoeffB;
        public const double CoeffA = 59021;
        public const double CoeffB = 55736;
        public const double CoeffTonometer = 43215;
        private const double _pressure = 2210;
        public int Convert(double value) => (int)(value / (Coeff / _pressure));
    }
}
