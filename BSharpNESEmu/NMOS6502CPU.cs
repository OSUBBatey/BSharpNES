using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSharpEmu.Enums;

namespace BSharpEmu.CPU
{
    public abstract class NMOS6502CPU : ICPU
    {
        #region RegisterVariables      
        public uint PC { get; protected set; }  //Program Counter 
        //TODO: REMODEL P AS A MODULE         
        protected byte P { get; set; }          //Processor Status Flag Bits rep:(NV-B DIZC) see http://wiki.nesdev.com/w/index.php/CPU_ALL 
        protected byte A { get; set; }          //8-bit Accumulator
        protected byte X { get; set; }          //8-bit Index Register
        protected byte Y { get; set; }          //8-bit Index Register
        protected byte S { get; set; }          //Stack Pointer
        protected uint LFSR { get; set; }       //Not sure what this is yet .. Noise Register for sound?
        protected long CPUCycles { get; set; }  //CPU Cycle Tracker
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

        public virtual void RunCPU()
        {
            throw new NotImplementedException();
        }     
    }
}
