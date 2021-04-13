using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class RETLW : Command
    {
        public RETLW(int k, IController controller, IMemory memory)
        {
            literal = k;
            this.controller = controller;
            this.memory = memory;
        }
        public override void execute()
        {
            writeResultToRightDestination(literal, true, 0);
            popStackToPc();
            controller.incTimer0ByProgram();
        }
    }
}
