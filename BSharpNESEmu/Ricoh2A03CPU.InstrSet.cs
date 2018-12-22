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
                /* ADC - Add with Carry */
                case 0x61:
                    CPUCycles += 6;
                    goto case (byte)OpCode.ADC;
                case 0x71:
                    CPUCycles += 5;
                    //Add 1 Cycle if Page Boundary Crossed
                    //TODO: Figure out how/when/why to determine and add this
                    goto case (byte)OpCode.ADC;
                case 0x65:
                    CPUCycles += 3;
                    goto case (byte)OpCode.ADC;
                case 0x75:
                    CPUCycles += 4;
                    goto case (byte)OpCode.ADC;
                case 0x69:
                    CPUCycles += 2;
                    goto case (byte)OpCode.ADC;
                case 0x79:
                    CPUCycles += 4;
                    //Add 1 Cycle if Page Boundary Crossed
                    //TODO: Figure out how/when/why to determine and add this
                    goto case (byte)OpCode.ADC;
                case 0x6D:
                    CPUCycles += 4;
                    goto case (byte)OpCode.ADC;
                case 0x7D:
                    CPUCycles += 4;
                    //Add 1 Cycle if Page Boundary Crossed
                    //TODO: Figure out how/when/why to determine and add this
                    goto case (byte)OpCode.ADC;

                /* AND - (bitwise AND with accumulator) */
                case 0x21:
                    CPUCycles += 6;
                    goto case (byte)OpCode.AND;
                case 0x25:
                    CPUCycles += 3;
                    goto case (byte)OpCode.AND;
                case 0x2D:
                    CPUCycles += 4;                    
                    goto case (byte)OpCode.AND;
                case 0x29:
                    CPUCycles += 2;
                    goto case (byte)OpCode.AND;
                case 0x31:
                    CPUCycles += 5;
                    //Add 1 Cycle if Page Boundary Crossed
                    //TODO: Figure out how/when/why to determine and add this
                    goto case (byte)OpCode.AND;
                case 0x35:
                    CPUCycles += 4;
                    goto case (byte)OpCode.AND;
                case 0x39:
                    CPUCycles += 4;
                    //Add 1 Cycle if Page Boundary Crossed
                    //TODO: Figure out how/when/why to determine and add this
                    goto case (byte)OpCode.AND;
                case 0x3D:
                    CPUCycles += 4;
                    //Add 1 Cycle if Page Boundary Crossed
                    //TODO: Figure out how/when/why to determine and add this
                    goto case (byte)OpCode.AND;               

                /* ASL - Arithmetic Shift Left */
                case 0x0A:
                    CPUCycles += 2;
                    goto case (byte)OpCode.ASL;
                case 0x06:
                    CPUCycles += 5;
                    goto case (byte)OpCode.ASL;
                case 0x16:
                    CPUCycles += 6;
                    goto case (byte)OpCode.ASL;
                case 0x0E:
                    CPUCycles += 6;
                    goto case (byte)OpCode.ASL;
                case 0x1E:
                    CPUCycles += 7;
                    goto case (byte)OpCode.ASL;

                /*Branch Cases*/
                case 0x90: //BCC - Branch on Carry Clear
                    Branch(GetCarryFlag()==0x0,input);
                    break;
                case 0xB0: //BCS - Branch on Carry Set
                    Branch(!(GetCarryFlag() == 0x0), input);
                    break;
                case 0xF0: //BEQ - Branch on Equal  
                    Branch(!(GetZeroFlag() == 0x0), input);
                    break;
                case 0x30: //BMI - Branch on Minus
                    Branch(!(GetSignFlag() == 0x0), input);
                    break;
                case 0xD0: //BNE - Branch on not Equal  
                    Branch(GetZeroFlag() == 0x0, input);
                    break;
                case 0x10: //BPL - Branch on Plus
                    Branch(!(GetSignFlag() == 0x0), input);
                    break;
                case 0x50: //BVC - Branch on OverFlow Clear
                    Branch((GetOverFlowFlag() == 0x0), input);
                    break;
                case 0x70: //BVS- Branch on OverFlow Set
                    Branch(!(GetOverFlowFlag() == 0x0), input);
                    break;

                /*BIT - test BITs*/
                case 0x42:
                    CPUCycles += 3;
                    goto case (byte)OpCode.BIT;
                case 0x2C:
                    CPUCycles += 4;
                    goto case (byte)OpCode.BIT;

                /*BRK - Break*/                
                case 0x00:
                    CPUCycles += 7;
                    goto case (byte)OpCode.BRK;

                /*CLC - Clear Carry)*/
                case 0x18:
                    CPUCycles += 2;
                    SetCarryFlag(0);
                    break;

                /*SEC - Set Carry)*/
                case 0x38:
                    CPUCycles += 2;
                    SetCarryFlag(1);
                    break;

                /*CLI - Clear IRQ)*/
                case 0x58:
                    CPUCycles += 2;
                    SetIRQFlag(0);
                    break;

                /*SEI - Set IRQ)*/
                case 0x78:
                    CPUCycles += 2;
                    SetIRQFlag(1);
                    break;

                /*CLV - Clear Overflow)*/
                case 0xB8:
                    CPUCycles += 2;
                    SetOverFlowFlag(false);
                    break;

                /*CLD - Clear Decimal)*/
                case 0xD8:
                    CPUCycles += 2;
                    SetDecimalFlag(0);
                    break;

                /*SED - Set Decimal)*/
                case 0xF8:
                    CPUCycles += 2;
                    SetDecimalFlag(1);
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
                    byte temp = (byte)(A + input + GetCarryFlag());
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

                /*
                 * Bit (test BITs)
                 * Affects Flags: N V Z
                 */
                case (byte)OpCode.BIT:
                   
                    SetSignFlag(input);
                    SetOverFlowFlag((0x40 & input)!=0x0);   /* Copy bit 6 to OVERFLOW flag. */
                    SetZeroFlag((byte)(input & A));
                    break;

                /*
                 * BRK (Break)
                 * Affects Flags : B
                 */
                case (byte)OpCode.BRK:                   
                    PC++;
                    Push((byte)((PC >> 8) & 0xff)); /* Push return address onto the stack. */
                    Push((byte)(PC & 0xff));                  
                    Push((byte)(P | (byte)CPURegisterType.BREAKBIT));
                    SetIRQFlag(1);
                    PC = (uint)(ReadByte(0xFFFE) | (ReadByte(0xFFFF) << 8));
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
                PC += inputAddr; 
            }
        }

        //TODO: Many of these get methods could be removed and replaced by boolean values for the flags
        //TODO: A module that represents the Processor Status should be made to accomplish this 

        #region GetFlag Methods
        //Returns the value of the carry flag
        private byte GetCarryFlag()
        {
            byte output = 0x00;
            if((P & (byte)CPURegisterType.CARRYBIT) == (byte)CPURegisterType.CARRYBIT )
            {
                output = 0x1;
            }
            return output;
        }
        //Returns the value of the zero flag
        private byte GetZeroFlag()
        {
            byte output = 0x00;
            if ((P & (byte)CPURegisterType.ZEROBIT) == (byte)CPURegisterType.ZEROBIT)
            {
                output = 0x1;
            }
            return output;
        }
        //Returns the value of the sign flag
        private byte GetSignFlag()
        {
            byte output = 0x00;
            if ((P & (byte)CPURegisterType.SIGNBIT) == (byte)CPURegisterType.SIGNBIT)
            {
                output = 0x1;
            }
            return output;
        }

        //Returns the value of the sign flag
        private byte GetOverFlowFlag()
        {
            byte output = 0x00;
            if ((P & (byte)CPURegisterType.OVERFLOWBIT) == (byte)CPURegisterType.OVERFLOWBIT)
            {
                output = 0x1;
            }
            return output;
        }
        #endregion

        #region SetFlag Methods

        private void SetZeroFlag(byte condition)
        {
            if (condition == 0x0)
            {
                P = ChangeBit(P, 1, 1);  //Set Zero Bit to 1
            }
            else
            {
                P = ChangeBit(P, 1, 0);  //Set Zero Bit to 0
            }
        }
        private void SetSignFlag(byte temp)
        {
            
            if((temp & 0x80) == 1) //If 7th bit is 1
            {
                P = (byte)(P | (byte)CPURegisterType.SIGNBIT); // Set SignBit to 1
            }
            else
            {                
                P = ChangeBit(P, 6, 0); //Else Set SignBit to 0
            }
        }

        private void SetOverFlowFlag(bool condition)
        {
            if (condition)
            {                
                P = ChangeBit(P, 5, 1); //Set OverFlow Bit to 1
            }
            else
            {
                P = ChangeBit(P, 5, 0); //Else Set OverFlow Bit to 0
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

        private void SetDecimalFlag(byte condition)
        {
            if (condition == 0x0)
            {
                P = ChangeBit(P, 3, 0);  //Set Carry Bit to 0
            }
            else
            {
                P = ChangeBit(P, 3, 1);  //Set Carry Bit to 1
            }
        }

        private void SetIRQFlag(byte condition)
        {
            if (condition == 0x0)
            {
                P = ChangeBit(P, 2, 0);  //Set IRQ Disabled Bit to 0
            }
            else
            {
                P = ChangeBit(P, 2, 1);  //Set IRQ Disabled Bit to 1
            }
        }
        #endregion
        
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

        /*Push to Stack */ 
        private void Push(byte input)
        {
            WriteToMem((uint)(CPURegisterType.STACKLOC + S--));          
        }

        /*Pop from Stack*/
        private byte Pop()
        {
           return ReadByte((uint)(CPURegisterType.STACKLOC + S--));
        }
    }
}
