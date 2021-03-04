using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class CALL: Command
    {
        int targetAdress;
        public CALL(int adress)
        {
            targetAdress = adress;
        }
        public override void execute()
        {
            GUI_Simu.stack.push(GUI_Simu.memory.getFullPC());
            _goto();
        }

        private void _goto()
        {
            if (targetAdress == 0)
            {
                GUI_Simu.memory.setFullPC(1023);
            }
            else
            {
                GUI_Simu.memory.setFullPC(targetAdress - 1);
            }
            GUI_Simu.controller.incTimer0ByProgram();
        }
    }
}
