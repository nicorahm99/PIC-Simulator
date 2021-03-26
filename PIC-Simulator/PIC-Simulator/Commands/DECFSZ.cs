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

        public DECFSZ(bool isResWritToW, int fAddress, Memory memory, Controller controller)
        {
            fileAddress = fAddress;
            isResultWrittenToW = isResWritToW;
            this.memory = memory;
            this.controller = controller;
        }
        public override void execute()
        {
            int fileContent = getFile(fileAddress);

            int result = fileContent - 1;
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
            if (result == 0)
            {
                memory.incPC();
                controller.incTimer0ByProgram();
            }
        }
    }
}
