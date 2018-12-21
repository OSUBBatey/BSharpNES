using BSharpEmu.Enums;
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
        //TODO:IMPLEMENT PC COUNTER ADJUSTMENTS AND ACCESS

        private void ExecuteInstruction(byte instr, byte input)
        {
            switch(instr)
            {
                #region Opcode/ClockCycles
                // ADC 
                case 0x61:
                    goto case (byte)OpCode.ADC;
                case 0x71:
                    goto case (byte)OpCode.ADC;
                case 0x65:
                    goto case (byte)OpCode.ADC;
                case 0x75:
                    goto case (byte)OpCode.ADC;
                case 0x69:
                    goto case (byte)OpCode.ADC;
                case 0x79:
                    goto case (byte)OpCode.ADC;
                case 0x6D:
                    goto case (byte)OpCode.ADC;
                case 0x7D:
                    goto case (byte)OpCode.ADC;        
                
                // AND
                case 0x21:
                    goto case (byte)OpCode.AND;
                case 0x25:
                    goto case (byte)OpCode.AND;
                case 0x2D:
                    goto case (byte)OpCode.AND;
                case 0x29:
                    goto case (byte)OpCode.AND;
                case 0x31:
                    goto case (byte)OpCode.AND;
                case 0x35:
                    goto case (byte)OpCode.AND;
                case 0x39:
                    goto case (byte)OpCode.AND;
                case 0x3D:
                    goto case (byte)OpCode.AND;               

                // ASL
                case 0x0A:
                    goto case (byte)OpCode.ASL;
                case 0x06:
                    goto case (byte)OpCode.ASL;
                case 0x16:
                    goto case (byte)OpCode.ASL;
                case 0x0E:
                    goto case (byte)OpCode.ASL;
                case 0x1E:
                    goto case (byte)OpCode.ASL;

                //Branch Cases
                case 0x90: //BCC
                    Branch(!(CarryFlag()==0x0),input);
                    break;
                #endregion


                #region Opcode Logic
             /*
              * ADC - ADD WITH CARRY FUNCTION
              * 
              * Affects Flags: S V Z C
              * 
              */
                case (byte)OpCode.ADC:                 
                    byte temp = (byte)(A + input + CarryFlag());
                    SetZeroFlag((byte)(temp & 0xFF));
                    SetSignFlag(temp);
                    SetOverFlowFlag(!(((byte)((A ^ input) & 0x80) & ((A ^ temp) & 0x80)) == 0));
                    SetCarryFlag(Convert.ToByte(temp > 0xff));
                    A = temp;
                    break;

             /*                  
              * AND (bitwise AND with accumulator)
              * Affects Flags: S Z
              * 
              */
                case (byte)OpCode.AND:
                    input &= A;
                    SetSignFlag(input);
                    SetZeroFlag(input);
                    A = input;
                    break;

             /*
              * ASL (Arithmetic Shift Left)
              * Affects Flags: S Z C
              */
                case (byte)OpCode.ASL:
                    SetCarryFlag((byte)(input & 0x80));
                    input <<= 1;
                    input &= 0xff;
                    SetSignFlag(input);
                    SetZeroFlag(input);
                    //TODO: STORE src in memory loc or accumulator depending on addressing mode.
                    A = input; //Only accumulator store implemented currently
                    break;
             
                #endregion

                default: break;

            }                        
        }

        /*
         * Branching logic. 
         */ 
        private void Branch(bool condition, byte inputAddr)
        {
            if (condition)
            {
                CPUCycles += (PC & 0xFF00) != (inputAddr & 0xFF00) ? 2 : 1;
                PC = inputAddr;
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
            
            if((temp & 0x80) == 1) //If 7th bit is 1
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
