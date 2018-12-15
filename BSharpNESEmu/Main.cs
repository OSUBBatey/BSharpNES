using System;
namespace BSharpEmu
{
    public class BSharp
    {
        private static int Main(string[] args)
        {
            Ricoh2A03CPU test = new Ricoh2A03CPU();
            test.RunInstruction();
            return 0;
        }
    }

}