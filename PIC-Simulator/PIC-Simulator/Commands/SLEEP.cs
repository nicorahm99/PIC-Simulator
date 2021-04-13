using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Commands
{
    class SLEEP: Command
    {
        public SLEEP(IMemory memory) 
        {
            this.memory = memory;
        }
        public override void execute()
        {
            //00h → WDT,
            //0 → WDT prescaler,
            int currentMemoryBank = memory.getCurrentMemoryBank();
            memory.setMemoryBankTo(1);
            clearBit(0x1, 0);
            clearBit(0x1, 1);
            clearBit(0x1, 2);
            memory.setMemoryBankTo(currentMemoryBank);
            //1 → TO,
            setBit(0x3, 4);
            //0 → PD
            clearBit(0x3, 3);
        }
    }
}
