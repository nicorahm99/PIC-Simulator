using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class RRF : Command
    {
        readonly bool isResultWrittenToW;

        public RRF(bool isResWritToW, int fAddress, IMemory memory)
        {
            fileAddress = fAddress;
            isResultWrittenToW = isResWritToW;
            this.memory = memory;
        }
        public override void execute()
        {
            int fileContent = getFile(fileAddress);
            int carryFlag = getFile(0x03) & 1;
            int newCarryFlag = fileContent & 0x1;
            int workingBits = fileContent >> 1;
            int result = (workingBits + (carryFlag << 7)) & 0xff;
            setCarryFlagTo(newCarryFlag);
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
        }
    }
}
