using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class CLRF: Command
    {
        public CLRF(int fAddress, IMemory memory)
        {
            fileAddress = fAddress;
            this.memory = memory;
        }

        public override void execute()
        {
            writeResultToRightDestination(0, false, fileAddress);
            setZeroFlagTo(1);
        }
    }
}
