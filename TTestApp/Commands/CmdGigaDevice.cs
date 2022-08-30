using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTestApp.Commands
{
    //Управление макетом GigaDevice осуществляется передачей ему команд от компьютера.
    //В ответ на каждую команду макет GigaDevice посылает пакет длиной 3 байта, содержащий значение давления.
    //Параметры интерфейса: скорость 115200, 8 бит, контроля четности нет, 1 стоп бит.
    internal enum CmdGigaDevice : byte
    {
        //Управление компрессором
        PumpSwitchOn   = 0x10, 
        PumpSwitchOff  = 0x11,
        PumpToPressure = 0x12,  //Накачать до давления (второй байт - требуемое давление)

        //Управление клапанами
        Valve1Close  = 0x20,
        Valve1Open   = 0x21,
        Valve1PWMOn  = 0x22,    //Включение ШИМ. Второй байт - скважность
        Valve1PWMOff = 0x23,    //Выключение ШИМ. 
        Valve2Close  = 0x24,
        Valve2Open   = 0x25,

        //Запрос значения давления
        GetValue     = 0x30
    }
}
