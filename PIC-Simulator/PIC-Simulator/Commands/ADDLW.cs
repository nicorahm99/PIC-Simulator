using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class ADDLW: Command
    {
        public ADDLW(int k, IMemory memory)
        {
            literal = k;
            this.memory = memory;
        }
        public override void execute()
        {
            int wContent = getWReg();
            int result = wContent + literal;
            int fourBitResult = (wContent & 0xf) + (literal & 0xf);
            setCarryFlagIfNeeded(result);
            setDigitCarryFlagIfNeeded(fourBitResult);
            if (isGreaterThan(result, 255))
            {
                result -= 256;
            }

            setZeroFlagIfNeeded(result);
            writeResultToRightDestination(result, true, 0);
        }
        private bool isGreaterThan(int lower, int higher)
        {
            if (lower > higher) return true;
            return false;
        }

    }
}
