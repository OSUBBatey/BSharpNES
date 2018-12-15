using System;

namespace BSharpEmu
{
    public class ProcessStatusRegister
    {
        byte NegativeFlag { get; set; }
        byte OverflowFlag { get; set; }
        readonly byte Always1Flag = 1;
        byte BreakFlag { get; set; }
        byte DecimalFlag { get; set; }
        byte InterruptFlag { get; set; }
        byte ZeroFlag { get; set; }
        byte CarryFlag { get; set; }

        public ProcessStatusRegister()
        {
           
        }

        
    }
}
