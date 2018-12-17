using System;
/* 
 * A representation of the NES(Nintendo Entertainment System) CPU, which is a specialized NMOS6502 CPU made by the Ricoh company under the model # 2A03.  
 */
namespace BSharpEmu
{
    public class Ricoh2A03CPU:NMOS6502CPU
    {
        public Ricoh2A03CPU()
        {
            //Initial(Start-Up) State 
            P = 0x34;
            A = 0;
            X = 0;
            Y = 0;
            S = 0xFD;
            /*Need a way to access memory here and set values for 
             * $4017 = $00 (frame irq enabled)
             * $4015 = $00 (all channels disabled)
             * $4000-$400F = $00 (not sure about $4010-$4013)
             */
            LFSR = 0x0000; // 1st time it is clocked from all zero, shifts in a 1
        }

        public void ResetCPU()
        {
            S -= 3;         //Decrement stack pointer by 3
            P = (byte)(P | 0x04); //Set IRQ Interrupt Disable Flag to on.. leave other flags as is            
        }

        public override void RunInstruction()
        {          
            //Read an instruction and do something
        }      
    }
}