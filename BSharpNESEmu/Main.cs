using System;
namespace BSharpEmu
{
    public class BSharp
    {
        private static int Main(string[] args)
        {
            CPU test = new CPU();
            test.RunInstruction();
            return 0;
        }
    }

}