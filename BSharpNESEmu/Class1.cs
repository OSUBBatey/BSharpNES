using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSharpEmu
{
    class CPUMemoryMap : IMemoryMap
    {
        //Memory Address Range (0x0000 to 0x07FF)
        //TODO: MAY MOVE INTERNAL MEMORY OUT OF MAP AND USE MAP STRICTLY FOR ADDRESS TRANSLATION
        protected MemoryRep InternalRAM = new MemoryRep(0x800);
        /*
         * Remember to setup memory mirroring through enums or something         
         * Mirrors are exact same size as Internal RAM and Range from: 
         * 0x0800 to 0x0FFF  
         * 0x1000 to 0x17FF
         * 0x1800 to 0x1FFF 
         */

         //TODO: PPU REGISTERS OR REFERENCE TO THEM 0x2000 to 0x2007
         //ALSO MIRRORS every 8 bytes from 0x2008 to 0x3FFF

        //TODO: APU I/O REGISTERS 0x4000 to 0x4017

        //TODO: 0x4018 to 0x401F  CPU TEST MODE .. SEE NESDEV WIKI

        //TODO: CARTRIDGE SPACE 

    }
}
