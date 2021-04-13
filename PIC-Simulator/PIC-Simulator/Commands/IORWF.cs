using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class IORWF : Command
    {
        readonly bool isResultWrittenToW;

        public IORWF(bool isResWritToW, int fAddress, IMemory memory)
        {
            fileAddress = fAddress;
            isResultWrittenToW = isResWritToW;
            this.memory = memory;
        }
        public override void execute()
        {
            int result = getWReg() | getFile(fileAddress);
            setZeroFlagIfNeeded(result);

            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
        }
    }
}
