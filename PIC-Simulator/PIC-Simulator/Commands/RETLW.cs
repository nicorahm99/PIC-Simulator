using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class RETLW : Command
    {
        public RETLW(int k)
        {
            literal = k;
        }
        public override void execute()
        {
            writeResultToRightDestination(literal, true, 0);
            popStackToPc();
            GUI_Simu.controller.incTimer0ByProgram();
        }
    }
}
