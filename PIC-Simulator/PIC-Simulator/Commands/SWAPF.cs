using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class SWAPF : Command
    {
        bool isResultWrittenToW;

        public SWAPF(bool isResWritToW, int fAddress)
        {
            fileAddress = fAddress;
            isResultWrittenToW = isResWritToW;
        }
        public override void execute()
        {
            int fileContent = getFile(fileAddress);
            int leftPart = (fileContent & 0xf0) >> 4;
            fileContent = fileContent << 4;
            int result = (fileContent + leftPart) & 0xff;
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
        }
    }
}
