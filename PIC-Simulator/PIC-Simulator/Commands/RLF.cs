using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class RLF : Command
    {
        readonly bool isResultWrittenToW;

        public RLF(bool isResWritToW, int fAddress, IMemory memory)
        {
            fileAddress = fAddress;
            isResultWrittenToW = isResWritToW;
            this.memory = memory;
        }
        public override void execute()
        {
            int fileContent = getFile(fileAddress);
            int carryFlag = getFile(0x03) & 1;
            int workingBits = fileContent << 1;
            workingBits += carryFlag;
            if ((workingBits & 0x100) != 0)
            {
                carryFlag = 1;
            }
            else
            {
                carryFlag = 0;
            }

            int result = workingBits & 0xff;
            setCarryFlagTo(carryFlag);
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
        }
    }
}
