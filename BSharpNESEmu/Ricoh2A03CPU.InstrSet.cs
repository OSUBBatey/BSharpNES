using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSharpEmu.CPU
{
    /*
     * Opcode Definitions for the NMOS 6502 relevant to NES Emulation.
     * Opcode operations adapted from the definitions found at :
     * http://nesdev.com/6502.txt , under the heading "INSTRUCTION OPERATION"
     * and http://www.6502.org/tutorials/6502opcodes.html
     */
    public partial class Ricoh2A03CPU
    {
        //TODO: REORDER OPCODES BY FREQUENCY OF USE .. CURRENTLY DONE ALPHABETICALLY
        //Current Build is agnostic to address mode currently... may keep it that way once i understand more
        //TODO: RESEARCH ADDRESSING MODES AND IMPLEMENT AS/WHERE NEEDED

        private void ExecuteInstruction(byte instr, byte input)
        {
            switch(instr)
            {
               
                case 0x61:
                case 0x71:
                case 0x65:
                case 0x75:
                case 0x69:
                case 0x79:
                case 0x6D:
                case 0x7D:
                /*
                 * ADC - ADD WITH CARRY FUNCTION
                 * 
                 * Affects Flags: S V Z C
                 * 
                 */
                    byte temp = (byte)(A + input + CarryFlag());
                    SetZeroFlag((byte)(temp&0xFF));
                    SetSignFlag(temp);
                    SetOverFlowFlag(!(((byte)((A ^ input) & 0x80) & ((A ^ temp) & 0x80))==0));
                    SetCarryFlag(Convert.ToByte(temp > 0xff));
                    A = temp;
                    //TODO:IMPLEMENT PC COUNTER ADJUSTMENTS AND ACCESS
                    break;

                default: break;
            }                        
        }

        private void SetZeroFlag(byte condition)
        {
            if (condition == 0x0)
            {
                P = ChangeBit(P, 1, 0);  //Set Zero Bit to 0
            }
            else
            {
                P = ChangeBit(P, 1, 1);  //Set Zero Bit to 1
            }
        }

        //Returns the value of the carry flag
        private byte CarryFlag()
        {
            byte output = 0x00;
            if((P & 0x1) == 0x1 )
            {
                output = 0x1;
            }
            return output;
        }
      
        private void SetSignFlag(byte temp)
        {
            
            if((temp & 0x40) == 1) //If 7th bit is 1
            {
                P = (byte)(P | 0x20); // Set SignBit to 1
            }
            else
            {                
                P = ChangeBit(P, 5, 0); //Else Set SignBit to 0
            }
        }

        private void SetOverFlowFlag(bool condition)
        {
            if (condition)
            {                
                P = ChangeBit(P, 4, 1); //Set OverFlow Bit to 1
            }
            else
            {
                P = ChangeBit(P, 4, 0); //Else Set OverFlow Bit to 0
            }
        }

        private void SetCarryFlag(byte condition)
        {
            if(condition == 0x0) 
            {
                P = ChangeBit(P, 0, 0);  //Set Carry Bit to 0
            }
            else
            {
                P = ChangeBit(P, 0, 1);  //Set Carry Bit to 1
            }
        }

        /*
         * Changes the value of a bit in a byte. 
         * Adapted from https://www.geeksforgeeks.org/modify-bit-given-position/
         */
        private byte ChangeBit(byte src, int pos, int state)
        {
            byte mask = (byte)(1 << pos);
            byte output = (byte)((src & ~mask) | ((state << pos) & mask));
            return output;
        }

    }
}
