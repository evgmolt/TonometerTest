using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTestApp
{
    internal static class Constants
    {
        internal static double StopMeasCoeff = 0.5;//Измерение прекращается, если амплитуда производной меньше Coeff*Max
        internal static double MaxAllowablePressure = 180;
        internal static double MinPressure = 120; //Минимальное давление, до которого осуществляется накачка  
        internal static double PressureLevelForLeak = 10;
        internal static double MaxTimeAfterMaxFound = 6; //sec
        internal static double MaxTimeAfterStartPumping = 10; //sec
        internal static int StartSearchMaxLevel = 8; //Значение производной
        internal static int StopPumpingLevel = 6;    //Значение производной

        //Детектор
        internal static int LockInterval = 60;
        internal static int NoWaveInterval1 = 600;
        internal static int NoWaveInterval2 = 1200;
        internal static double MinDetectLevel = 5;
        internal static double DetectLevelCoeffSys = 0.7;
        internal static double DetectLevelCoeffDia = 0.55;
    }
}
