using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class BCF : Command
    {
        public BCF(int fAddress, int bAddress, IMemory memory)
        {
            fileAddress = fAddress;
            bitAddress = bAddress;
            this.memory = memory;
        }
        public override void execute()
        {
            int registerContent = memory.getFile(fileAddress);
            registerContent &= ~(1 << bitAddress);
            memory.setFile(fileAddress, registerContent);
        }
    }
}
