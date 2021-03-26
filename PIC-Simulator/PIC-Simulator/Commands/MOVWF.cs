using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class MOVWF: Command
    {
        public MOVWF(int fAddress, Memory memory)
        {
            fileAddress = fAddress;
            this.memory = memory;
        }
        public override void execute()
        {
            int fileContent = getWReg();
            writeResultToRightDestination(fileContent, false, fileAddress);
        }
    }
}
