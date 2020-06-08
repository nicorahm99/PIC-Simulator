using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator
{
    public class EEPROM
    {
        // TODO set WRERR Flag
        
        private int[] eeprom = new int[256];
        private bool isStateMachineTriggered = false;
        
        public void writeToEEPROM()
        {
            int address = GUI_Simu.memory.getFile(0x09);
            int value = GUI_Simu.memory.getFile(0x08);
            if (address <= 64 && isStateMachineTriggered)
            {
                eeprom[address] = value;
                isStateMachineTriggered = false;
                clearWriteBitSetInetrruptFlag();
            }
        }

        public void readFromEEPROM() 
        {
            int address = GUI_Simu.memory.getFile(0x09);
            if (address <= 64)
            {
                GUI_Simu.memory.setFile(0x08, eeprom[address]);
                clearReadBitSetInetrruptFlag();
            }
        }

        public void setStateMachineTriggered()
        {
            isStateMachineTriggered = true;
        }

        private void clearBit(int bitAddress)
        {
            int currentMemoryBank = GUI_Simu.memory.getCurrentMemoryBank();
            GUI_Simu.memory.setMemoryBankTo(1);
            int eecon1 = GUI_Simu.memory.getFile(0x08);
            eecon1 &= ~(1 << bitAddress);
            eecon1 += 0x10;
            GUI_Simu.memory.setFile(0x08, eecon1);
            GUI_Simu.memory.setMemoryBankTo(currentMemoryBank);
        }

        private void clearWriteBitSetInetrruptFlag()
        {
            clearBit(1);
        }

        private void clearReadBitSetInetrruptFlag()
        {
            clearBit(0);
        }
    }
}
