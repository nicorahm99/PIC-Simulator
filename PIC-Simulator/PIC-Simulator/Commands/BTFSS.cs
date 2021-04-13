using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class BTFSS : Command
    {
        public BTFSS(int fAddress, int bAddress, IMemory memory, IController controller)
        {
            fileAddress = fAddress;
            bitAddress = bAddress;
            this.memory = memory;
            this.controller = controller;
        }
        public override void execute()
        {
            int registerContent = memory.getFile(fileAddress);
            registerContent &= 1 << bitAddress;
            if (registerContent != 0)
            {
                memory.incPC();
                controller.incTimer0ByProgram();
            }
        }
    }
}
