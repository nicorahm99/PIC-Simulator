﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class IORLW : Command
    {
        public IORLW(int k, IMemory memory)
        {
            literal = k;
            this.memory = memory;
        }
        public override void execute()
        {
            int result = getWReg() | literal;
            setZeroFlagIfNeeded(result);

            writeResultToRightDestination(result, true, 0);
        }
    }
}
