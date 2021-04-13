using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class CLRW: Command
    {
        public CLRW(IMemory memory)
        {
            this.memory = memory;
        }
        public override void execute()
        {
            writeResultToRightDestination(0, true, 0);
            setZeroFlagTo(1);
        }
    }
}
