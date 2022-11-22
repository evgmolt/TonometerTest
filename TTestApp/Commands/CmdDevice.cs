using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTestApp.Commands
{
    //Управление макетом осуществляется передачей ему команд от компьютера.
    //В ответ на каждую команду макет посылает пакет длиной 3 байта, содержащий значение давления.
    //Параметры интерфейса: скорость 115200, 8 бит, контроля четности нет, 1 стоп бит.
    internal enum CmdDevice : byte
    {
        //Управление компрессором
        PumpSwitchOn   = 0x10, 
        PumpSwitchOff  = 0x11,
        PumpToPressure = 0x12,  //Накачать до давления (второй байт - требуемое давление)

        //Управление клапанами
        ValveSlowClose  = 0x20,
        ValveSlowOpen   = 0x21,
        ValveFastClose  = 0x24,
        ValveFastOpen   = 0x25,

        StartReading    = 0x30,
        StopReading     = 0x31
    }
}
