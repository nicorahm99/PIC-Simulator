using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class MOVF : Command
    {
        bool isResultWrittenToW;

        public MOVF(bool isResWritToW, int fAddress)
        {
            fileAddress = fAddress;
            isResultWrittenToW = isResWritToW;
        }
        public override void execute()
        {
            int fileContent = getFile(fileAddress);
            setZeroFlagIfNeeded(fileContent);
            writeResultToRightDestination(fileContent, isResultWrittenToW, fileAddress);
        }
    }
}
