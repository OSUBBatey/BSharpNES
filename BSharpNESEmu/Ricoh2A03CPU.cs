using System;
/* 
 * A representation of the NES(Nintendo Entertainment System) CPU, which is a specialized NMOS6502 CPU made by the Ricoh company under the model # 2A03.  
 */
namespace BSharpEmu
{
    public class Ricoh2A03CPU
    {
        #region RegisterVariables
        uint PC { get;  set; } //Program Counter 
        //TODO: REMODEL P AS A MODULE 
        byte P { get;  set; } //Processor Status Flag Bits rep:(NV-B DIZC) see http://wiki.nesdev.com/w/index.php/CPU_ALL 
        byte A { get;  set; } //8-bit Accumulator
        byte X { get;  set; } //8-bit Index Register
        byte Y { get;  set; } //8-bit Index Register
        byte S { get;  set; } //Stack Pointer
        uint LFSR { get;  set; } //Not sure what this is yet .. Noise Register for sound?
        #endregion


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

        /*
         *Register Enums
         */
        public enum RegisterEnum
        {
            //Needs Research.. 
            DMA = 0x4014,
            INPUT_PORT1 = 0x4016,
            INPUT_PORT2 = 0x4017,
        }

        /*
         * Interrupts trigger flags based on specific criteria. Remember to do this.
         */
        public enum InterruptEnum
        {
            //Ordered By Priority (high to low)
            RESET = 0xFFFC,
            NMI = 0xFFFA,
            IRQ_BREAK = 0xFFFE,
        }

        public void ResetCPU()
        {
            S -= 3;         //Decrement stack pointer by 3
            P = (byte)(P | 0x04); //Set IRQ Interrupt Disable Flag to on.. leave other flags as is            
        }

        public void RunInstruction()
        {
            Console.WriteLine("Hello!");
            Console.ReadKey();
            //Read an instruction and do something
        }
    }
}