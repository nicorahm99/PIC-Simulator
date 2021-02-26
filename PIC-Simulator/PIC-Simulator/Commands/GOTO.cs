using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class GOTO: Command
    {
        int targetAddress;
        public GOTO(int address)
        {
            targetAddress = address;
        }
        public void execute()
        {
            if (targetAddress == 0)
            {
                GUI_Simu.memory.setFullPC(1023);
            }
            else
            {
                GUI_Simu.memory.setFullPC(targetAddress - 1);
            }
            GUI_Simu.controller.incTimer0ByProgram();
        }
    }
}
