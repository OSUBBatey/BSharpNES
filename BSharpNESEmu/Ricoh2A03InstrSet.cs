using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSharpEmu
{
    /*
     * Opcode Definitions for the NMOS 6502 relevant to NES Emulation.
     * Opcode operations adapted from the definitions found at :
     * http://nesdev.com/6502.txt , under the heading "INSTRUCTION OPERATION"
     * and http://www.6502.org/tutorials/6502opcodes.html
     */
    class Ricoh2A03InstrSet
    {
        
        public Ricoh2A03InstrSet(ICPU CPU)
        {

        }
        //Current Build is agnostic to address mode currently... may keep it that way once i understand more
        //TODO: RESEARCH ADDRESSING MODES AND IMPLEMENT AS/WHERE NEEDED

        //TODO: REORDER OPCODES BY FREQUENCY OF USE .. CURRENTLY DONE ALPHABETICALLY



    }
}
