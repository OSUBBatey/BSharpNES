using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSharpEmu.Enums
{
    /*
     * Register Enums
     */
    public enum RegisterType
    {
        //Needs Research.. 
        DMA = 0x4014,
        INPUT_PORT1 = 0x4016,
        INPUT_PORT2 = 0x4017,
        P = 0x01,
        A = 0x02,
        X = 0x03,
        Y = 0x04,
        S = 0x05,
    }
}
