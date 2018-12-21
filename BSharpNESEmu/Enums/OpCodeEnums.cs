using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSharpEmu.Enums
{
    /*
     *Opcode Matrix Enums for 6502
     * Used Map @ https://www.masswerk.at/6502/6502_instruction_set.html to 
     * determine unused byte codes for enums
     */
    public enum OpCode: byte
    {
        ADC = 0x03,
        AND = 0x13,
        ASL = 0x23,
        BIT = 0x33,
       
    }
}
