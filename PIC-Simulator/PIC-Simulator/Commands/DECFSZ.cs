using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class DECFSZ : Command
    {
        bool isResultWrittenToW;

        public DECFSZ(bool isResWritToW, int fAddress)
        {
            fileAddress = fAddress;
            isResultWrittenToW = isResWritToW;
        }
        public override void execute()
        {
            int fileContent = getFile(fileAddress);

            int result = fileContent - 1;
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
            if (result == 0)
            {
                GUI_Simu.memory.incPC();
                GUI_Simu.controller.incTimer0ByProgram();
            }
        }
    }
}
