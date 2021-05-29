using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator
{
    public class EEPROM : IEEPROM
    {
        private IMemory memory;

        private int[] eeprom = new int[256];
        private bool isStateMachineTriggered = false;

        public void init(IMemory memory)
        {
            this.memory = memory;
        }

        public void writeToEEPROM()
        {
            int currentMemoryBank = memory.getCurrentMemoryBank();
            memory.setMemoryBankTo(0);
            int address = memory.getFile(0x09);
            int value = memory.getFile(0x08);
            memory.setMemoryBankTo(currentMemoryBank);
            if (address < 64 && isStateMachineTriggered)
            {
                eeprom[address] = value;
                isStateMachineTriggered = false;

                Task.Factory.StartNew(() => clearWriteBitSetInetrruptFlag());
            }

        }

        public void readFromEEPROM()
        {
            int currentMemoryBank = memory.getCurrentMemoryBank();
            memory.setMemoryBankTo(0);
            int address = memory.getFile(0x09);
            if (address < 64)
            {
                memory.setFile(0x08, eeprom[address]);
                clearReadBitSetInetrruptFlag();
            }
            memory.setMemoryBankTo(currentMemoryBank);
        }

        public void setStateMachineTriggered()
        {
            isStateMachineTriggered = true;
        }

        private void clearBit(int bitAddress)
        {
            int currentMemoryBank = memory.getCurrentMemoryBank();
            memory.setMemoryBankTo(1);
            int eecon1 = memory.getFile(0x08);
            eecon1 &= ~(1 << bitAddress);
            eecon1 += 0x10;
            memory.setFile(0x08, eecon1);
            memory.setMemoryBankTo(currentMemoryBank);
        }

#pragma warning disable CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt.
        private async Task<int> clearWriteBitSetInetrruptFlag()
#pragma warning restore CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt.
        {
            Task.Delay(1000).Wait();
            clearBit(1);
            int currentMemoryBank = memory.getCurrentMemoryBank();
            memory.setMemoryBankTo(1);
            memory.setBit(8, 4);
            memory.setMemoryBankTo(currentMemoryBank);
            return 0;
        }

        private void clearReadBitSetInetrruptFlag()
        {
            clearBit(0);
        }

        public int[] getEeprom()
        {
            return eeprom;
        }
    }
}
