using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class ANDLW: Command
    {
        public ANDLW(int k, Memory memory)
        {
            literal = k;
            this.memory = memory;
        }
        public override void execute()
        {
            int result = getWReg() & literal;
            setZeroFlagIfNeeded(result);
            writeResultToRightDestination(result, true, 0);
        }
    }
}
