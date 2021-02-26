using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class BCF : Command
    {
        public BCF(int fAddress, int bAddress)
        {
            fileAddress = fAddress;
            bitAddress = bAddress;
        }
        public void execute()
        {
            int registerContent = GUI_Simu.memory.getFile(fileAddress);
            registerContent &= ~(1 << bitAddress);
            GUI_Simu.memory.setFile(fileAddress, registerContent);
        }
    }
}
