using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class RETURN: Command
    {
        public RETURN() { return; }
        public override void execute()
        {
            popStackToPc();
            GUI_Simu.controller.incTimer0ByProgram();
        }
    }
}
