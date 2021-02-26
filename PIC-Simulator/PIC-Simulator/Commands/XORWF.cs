using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class XORWF : Command
    {
        bool isResultWrittenToW;

        public XORWF(bool isResWritToW, int fAddress)
        {
            fileAddress = fAddress;
            isResultWrittenToW = isResWritToW;
        }
        public void execute()
        {
            int result = getWReg() ^ getFile(fileAddress);
            setZeroFlagIfNeeded(result);
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
        }
    }
}
