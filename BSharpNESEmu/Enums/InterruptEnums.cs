using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSharpEmu.Enums
{
    /*
     * Interrupts trigger flags based on specific criteria. Remember to do this.
     */
    public enum InterruptType
    {
        //Ordered By Priority (high to low)
        RESET = 0xFFFC,
        NMI = 0xFFFA,
        IRQ_BREAK = 0xFFFE,
    }
}
