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
        public GOTO(int address, Memory memory, Controller controller)
        {
            targetAddress = address;
            this.memory = memory;
            this.controller = controller;
        }
        public override void execute()
        {
            if (targetAddress == 0)
            {
                memory.setFullPC(1023);
            }
            else
            {
                memory.setFullPC(targetAddress - 1);
            }
            controller.incTimer0ByProgram();
        }
    }
}
