using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class BTFSS : Command
    {
        public BTFSS(int fAddress, int bAddress)
        {
            fileAddress = fAddress;
            bitAddress = bAddress;
        }
        public override void execute()
        {
            int registerContent = GUI_Simu.memory.getFile(fileAddress);
            registerContent &= 1 << bitAddress;
            if (registerContent != 0)
            {
                GUI_Simu.memory.incPC();
                GUI_Simu.controller.incTimer0ByProgram();
            }
        }
    }
}
