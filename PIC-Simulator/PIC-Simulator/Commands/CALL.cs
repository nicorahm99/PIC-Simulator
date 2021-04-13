using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class CALL: Command
    {
        readonly int targetAdress;
        public CALL(int adress, IStack stack, IMemory memory, IController controller)
        {
            targetAdress = adress;
            this.stack = stack;
            this.memory = memory;
            this.controller = controller;
        }
        public override void execute()
        {
            stack.push(memory.getFullPC());
            _goto();
        }

        private void _goto()
        {
            if (targetAdress == 0)
            {
                memory.setFullPC(1023);
            }
            else
            {
                memory.setFullPC(targetAdress - 1);
            }
            controller.incTimer0ByProgram();
        }
    }
}
