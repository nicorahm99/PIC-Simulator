﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator
{
    public class ROM
    {
        int length;
        private int[] rom = new int[1024];

        public void setRom(List<int> rom)
        {
            this.rom = rom.ToArray();
            length = this.rom.Length;
        }

        public int fetchCommand(int address)
        {
            return rom[address];
        }

        public int getLength()
        {
            return length;
        }
    }
}
