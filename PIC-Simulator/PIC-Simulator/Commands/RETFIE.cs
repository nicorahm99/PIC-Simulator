using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class RETFIE: Command
    {
        public RETFIE() { return; }
        public void execute()
        {
            setBit(0xb, 7);
            popStackToPc();
            GUI_Simu.controller.incTimer0ByProgram();
        }
    }
}
