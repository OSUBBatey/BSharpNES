using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSharpEmu
{
    public class MemoryRep
    {
        protected byte[] MemorySpace;
        public MemoryRep(int size)
        {
            MemorySpace = new byte[size];
        }

        public byte ReadFromMemory()
        {
            //TODO: NEW METHOD NAME? Program read logic
            return 0;
        }

        public byte WriteToMemory()
        {
            //TODO: NEW METHOD NAME? Program write logic
            return 0;
        }
    }
}
