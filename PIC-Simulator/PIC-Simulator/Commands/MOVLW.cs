using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class MOVLW : Command
    {
        public MOVLW(int k, IMemory memory)
        {
            literal = k;
            this.memory = memory;
        }
        public override void execute()
        {
            setZeroFlagIfNeeded(literal);
            writeResultToRightDestination(literal, true, 0);
        }
    }
}
