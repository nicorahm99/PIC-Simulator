﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class BSF: Command
    {
        public BSF(int fAddress, int bAddress)
        {
            fileAddress = fAddress;
            bitAddress = bAddress;
        }
        public override void execute()
        {
            int registerContent = GUI_Simu.memory.getFile(fileAddress);
            registerContent |= 1 << bitAddress;
            GUI_Simu.memory.setFile(fileAddress, registerContent);
        }
    }
}
