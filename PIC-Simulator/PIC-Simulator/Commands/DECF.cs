using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class DECF: Command
    {
        readonly bool isResultWrittenToW;

        public DECF(bool isResWritToW, int fAddress, IMemory memory)
        {
            fileAddress = fAddress;
            isResultWrittenToW = isResWritToW;
            this.memory = memory;
        }
        public override void execute()
        {
            int result = getFile(fileAddress) - 1;
            if (result < 0)
            {
                result += 256;
            }

            setZeroFlagIfNeeded(result);
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
        }
    }
}
