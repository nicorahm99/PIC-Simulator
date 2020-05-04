using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator
{
    class EEPROM
    {
        private int[] eeprom = new int[256];
        private bool isStateMachineTriggered = false;
        
        public void writeToEEPROM(int value, int address)
        {
            if (address <= 64 && isStateMachineTriggered)
            {
                eeprom[address] = value;
                isStateMachineTriggered = false;
                clearWriteBit();
            }
        }

        public int readFromEEPROM(int address) 
        {
            if (address <= 64)
            {
                return eeprom[address];
            }
            return 0;
        }

        private void clearBit(int bitAddress)
        {
            GUI_Simu.memory.setMemoryBankTo(1);
            int eecon1 = GUI_Simu.memory.getFile(0x08);
            eecon1 &= ~(1 << bitAddress);
            GUI_Simu.memory.setFile(0x08, eecon1);
            GUI_Simu.memory.setMemoryBankTo(0);
        }

        private void clearWriteBit()
        {
            clearBit(1);
        }

        private void clearReadBit()
        {
            clearBit(0);
        }
    }
}
