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

        public ADDWF(bool isResWritToW, int fAddress)
        {
            fileAddress = fAddress;
            isResultWrittenToW = isResWritToW;
        }
        public void execute()
        {
            int wContent = GUI_Simu.memory.getWReg();
            int fileContent = GUI_Simu.memory.getFile(this.fileAddress);
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
