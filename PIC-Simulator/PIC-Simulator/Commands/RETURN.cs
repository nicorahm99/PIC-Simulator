using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class RETURN: Command
    {
        public RETURN(Controller controller, Memory memory) 
        { 
            this.controller = controller;
            this.memory = memory;
        }
        public override void execute()
        {
            popStackToPc();
            controller.incTimer0ByProgram();
        }
    }
}
