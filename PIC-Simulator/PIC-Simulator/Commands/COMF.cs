using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class COMF: Command
    {
        bool isResultWrittenToW;

        public COMF(bool isResWritToW, int fAddress)
        {
            fileAddress = fAddress;
            isResultWrittenToW = isResWritToW;
        }
        public void execute()
        {
            int result = ~(getFile(fileAddress)) & 0xff;
            setZeroFlagIfNeeded(result);
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
        }
    }
}
