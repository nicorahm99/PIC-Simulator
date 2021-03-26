using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class ADDWF: Command
    {
        bool isResultWrittenToW;

        public ADDWF(bool isResWritToW, int fAddress, Memory memory)
        {
            fileAddress = fAddress;
            isResultWrittenToW = isResWritToW;
            this.memory = memory;
        }
        public override void execute()
        {
            int wContent = memory.getWReg();
            int fileContent = memory.getFile(this.fileAddress);
            int result = wContent + fileContent;
            int fourBitResult = (wContent & 0xf) + (fileContent & 0xf);
            setCarryFlagIfNeeded(result);
            setDigitCarryFlagIfNeeded(fourBitResult);
            if (result > 255)
            {
                result -= 256;
            }

            setZeroFlagIfNeeded(result);
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
        }
    }
}
