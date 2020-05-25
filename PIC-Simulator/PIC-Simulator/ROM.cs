using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator
{
    public class ROM
    {
        private int[] rom;

        public ROM() { init(); }
        public void init() { rom = new int[1024]; }

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
