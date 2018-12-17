using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSharpEmu
{
    public abstract class NMOS6502CPU : ICPU
    {
        #region RegisterVariables
        UInt16 PC { get; set; } //Program Counter 
        //TODO: REMODEL P AS A MODULE 
        public byte P { get; set; } //Processor Status Flag Bits rep:(NV-B DIZC) see http://wiki.nesdev.com/w/index.php/CPU_ALL 
        public byte A { get; set; } //8-bit Accumulator
        public byte X { get; set; } //8-bit Index Register
        public byte Y { get; set; } //8-bit Index Register
        public byte S { get; set; } //Stack Pointer
        public uint LFSR { get; set; } //Not sure what this is yet .. Noise Register for sound?
        #endregion

        public virtual void PrintRegisters()
        {
            //TODO: REMOVE OR IMPROVE THIS.. ONLY FOR DEBUG PURPOSES CURRENTLY

            string temp = P.ToString("X");
            Console.WriteLine("Register P:");
            Console.Write("0x");
            Console.WriteLine(temp);

            temp = A.ToString("X");
            Console.WriteLine("Register A:");
            Console.Write("0x");
            Console.WriteLine(temp);

            Console.WriteLine("Register X:");
            temp = X.ToString("X");
            Console.Write("0x");
            Console.WriteLine(temp);

            Console.WriteLine("Register Y:");
            temp = Y.ToString("X");
            Console.Write("0x");
            Console.WriteLine(temp);

            Console.WriteLine("Register S:");
            temp = S.ToString("X");
            Console.Write("0x");
            Console.WriteLine(temp);

            Console.ReadLine();
        }

        public virtual void RunInstruction()
        {
            throw new NotImplementedException();
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
    }
}
