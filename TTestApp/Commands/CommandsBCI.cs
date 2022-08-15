namespace TTestApp.Commands
{
    internal static class CommandsBCI
    {
        private const byte Gain = 0x20;//Регистр CH1SET биты 4-6 Value 0-1-2-3-4-5-6   Gain 1-2-4-6-8-12-24

        public const int BytesInCommand = 14;
        public const int numRegNum = 8;
        public const int numRegValue = 9;

        public static byte[] CommandSetReg = { 0x77, 0x66, 0x55, 0xAA, 0x41, 0, 0, 1, 0, 0, 0, 0, 0, 0 };
        public static byte[] CommandSetADC = { 0x77, 0x66, 0x55, 0xAA, 0x42, 0, 0, 3, 1, 1, 0, 0, 0, 0 }; //Включение АЦП 1 и 2, 250 Гц
        private static byte[] RegsValues = { 0x00, 0xB6, 0xC0, 0xE0, 0x00, 0x60, 0x61, 0x61,
                                             0x61, 0x61, 0x61, 0x61, 0x61, 0x00, 0x00, 0x00,
                                             0x00, 0x00, 0x00, 0x00, 0x0F, 0x00, 0x00, 0x00 };

        public static void CountCheckSum(ref byte[] command)
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
            command[10] = (byte)byte3;
            command[11] = (byte)byte2;
            command[12] = (byte)byte1;
            command[13] = (byte)byte0;
        }
        public static byte[] GetSetRegArray()
        {
            List<byte> listResult = new();
            for (byte i = 0; i < RegsValues.Length; i++)
            {
                CommandSetReg[numRegNum] = i;
                CommandSetReg[numRegValue] = RegsValues[i];
                CountCheckSum(ref CommandSetReg);
                for (int k = 0; k < BytesInCommand; k++)
                {
                    listResult.Add(CommandSetReg[k]);
                }
            }
            return listResult.ToArray();
        }

        public static void BCISetup(USBserialPort usbPort)
        {
            CountCheckSum(ref CommandSetADC);
            usbPort.WriteBuf(CommandSetADC);
            var registersSetArray = GetSetRegArray();
            usbPort.WriteBuf(registersSetArray);

            Thread.Sleep(100);

            CommandSetReg[numRegNum] = 3;
            CommandSetReg[numRegValue] = 0xE0;
            CountCheckSum(ref CommandSetReg);
            usbPort.WriteBuf(CommandSetReg);

            Thread.Sleep(100);

            CommandSetReg[numRegNum] = 5;
            CommandSetReg[numRegValue] = Gain;
            CountCheckSum(ref CommandSetReg);
            usbPort.WriteBuf(CommandSetReg);
        }
    }
}
