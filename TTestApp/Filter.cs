using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTestApp
{
    internal static class Filter
    {
        //        Filter type: High Pass
        //Filter model: Bessel
        //Filter order: 2 
        //Sampling Frequency: 1280 Hz
        //Cut Frequency: 5.000000 Hz
        //Coefficents Quantization: float

        //Z domain Zeros
        //z = 1.000000 + j 0.000000 
        //z = 1.000000 + j 0.000000 

        //Z domain Poles
        //z = 0.978894 + j - 0.012015
        //z = 0.978894 + j 0.012015 
        static int NCoef = 2;
        static double[] y = new double[NCoef + 1]; //output samples 
        static double[] x = new double[NCoef + 1]; //input samples 

        internal static double HighPass(double NewSample)
        {
            double[] ACoef = { 0.97913295295553560000, -1.95826590591107120000, 0.97913295295553560000 };
            double[] BCoef = { 1.00000000000000000000, -1.95778812550116580000, 0.95837795232608958000 };

            //shift the old samples 
            for (int n = NCoef; n > 0; n--)
            {
                x[n] = x[n - 1];
                y[n] = y[n - 1];
            }

            //Calculate the new output 
            x[0] = NewSample;
            y[0] = ACoef[0] * x[0];
            for (int n = 1; n <= NCoef; n++)
            { 
                y[0] += ACoef[n] * x[n] - BCoef[n] * y[n];
            }
            return y[0];
        }
    }
}
