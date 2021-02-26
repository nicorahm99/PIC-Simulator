﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class INCFSZ : Command
    {
        bool isResultWrittenToW;

        public INCFSZ(bool isResWritToW, int fAddress)
        {
            fileAddress = fAddress;
            isResultWrittenToW = isResWritToW;
        }
        public void execute()
        {
            int fileContent = getFile(fileAddress);
            int result = fileContent + 1;
            if (result > 255)
            {
                result -= 256;
                GUI_Simu.memory.incPC();
                GUI_Simu.controller.incTimer0ByProgram();
            }
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
        }
    }
}
