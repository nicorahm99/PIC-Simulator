using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class XORLW : Command
    {
        public XORLW(int k)
        {
            literal = k;
        }
        public override void execute()
        {
            int result = literal ^ getWReg();
            setZeroFlagIfNeeded(result);
            writeResultToRightDestination(result, true, 0);
        }
    }
}
