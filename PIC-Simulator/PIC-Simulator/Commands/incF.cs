using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class INCF : Command
    {
        bool isResultWrittenToW;

        public INCF(bool isResWritToW, int fAddress, Memory memory)
        {
            fileAddress = fAddress;
            isResultWrittenToW = isResWritToW;
            this.memory = memory;
        }
        public override void execute()
        {
            int result = getFile(fileAddress) + 1;
            if (result > 255)
            {
                result -= 256;
            }

            setZeroFlagIfNeeded(result);
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
        }
    }
}
