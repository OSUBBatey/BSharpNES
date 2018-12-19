using System;
using BSharpEmu.CPU;

namespace BSharpEmu
{
    public class BSharp
    {
        private static int Main(string[] args)
        {
            Ricoh2A03CPU test = new Ricoh2A03CPU();
            test.PrintRegisters();           
            test.RunCPU();
            test.PrintRegisters();
            test.ResetCPU();
            test.PrintRegisters();
            return 0;
        }
    }

}