using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class SUBLW : Command
    {
        public SUBLW(int k, Memory memory)
        {
            literal = k;
            this.memory = memory;
        }
        public override void execute()
        {
            int wContent = getWReg();
            int result = literal - wContent;
            int fourBitResult = (literal & 0xf) - (wContent & 0xf);
            setCarryFlagsForSub(result, fourBitResult);
            if (isLessThan(result, 0))
            {
                result += 256;
            }
            setZeroFlagIfNeeded(result);
            writeResultToRightDestination(result, true, 0);
        }

        private bool isLessThan(int higher, int lower)
        {
            if (higher < lower) return true;
            return false;
        }
    }
}
