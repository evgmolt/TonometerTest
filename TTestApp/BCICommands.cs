using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTestApp
{
    internal static class BCICommands
    {
        private const int BytesInCommand = 14;
        private const int numHeader = 4;
        private const int numADCNum = 7;
        private const int numRegNum = 8;
        private const int numRegValue = 9;

        public static int[] CommandSetReg  = { 0x77, 0x66, 0x55, 0xAA, 0x41, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static int[] CommandADC     = { 0x77, 0x66, 0x55, 0xAA, 0x42, 0, 0, 3, 1, 1, 0, 0, 0, 0 }; //Включение АЦП 1 и 2, 250 Гц
        private static byte[] DefaultsRegsValues = { 0xFF, 0x96, 0xC0, 0x60, 0x00, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0F, 0x00, 0x00, 0x00 };

        public static void CountCheckSum(ref int[] command)
        {
            int sum = 0;
            for (int i = 0; i < command.Length - 4; i++)
            {
                sum += command[i];
            }
            var byte0 = sum & 0xFF;
            sum >>= 8;
            var byte1 = sum & 0xFF;
            sum >>= 8;
            var byte2 = sum & 0xFF;
            sum >>= 8;
            var byte3 = sum;
            command[10] = byte3;
            command[11] = byte2;
            command[12] = byte1;
            command[13] = byte0;
        }
    }
}
