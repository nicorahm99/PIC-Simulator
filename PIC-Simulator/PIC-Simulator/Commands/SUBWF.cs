using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class SUBWF : Command
    {
        bool isResultWrittenToW;

        public SUBWF(bool isResWritToW, int fAddress, Memory memory)
        {
            fileAddress = fAddress;
            isResultWrittenToW = isResWritToW;
            this.memory = memory;
        }
        public override void execute()
        {
            int wContent = getWReg();
            int fileContent = getFile(fileAddress);
            int result = fileContent - wContent;
            int fourBitResult = (fileContent & 0xf) - (wContent & 0xf);
            setCarryFlagsForSub(result, fourBitResult);
            if (result < 0)
            {
                result += 256;
            }

            setZeroFlagIfNeeded(result);
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
        }
    }
}
