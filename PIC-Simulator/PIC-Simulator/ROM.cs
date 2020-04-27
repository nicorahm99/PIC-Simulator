using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator
{
    class ROM
    {
        private int[] rom = new int[1024];

        public void setRom(List<int> rom)
        {
            this.rom = rom.ToArray();
        }

        public int fetchCommand(int address)
        {
            return rom[address];
        }
    }
}
