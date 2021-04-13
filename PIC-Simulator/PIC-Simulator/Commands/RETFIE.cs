using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class RETFIE: Command
    {
        public RETFIE(IController controller, IMemory memory) 
        {
            this.controller = controller;
            this.memory = memory;
        }
        public override void execute()
        {
            setBit(0xb, 7);
            popStackToPc();
            controller.incTimer0ByProgram();
        }
    }
}
