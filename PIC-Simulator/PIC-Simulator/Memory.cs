using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator
{
    public class Memory
    {
        public Memory() { init(); }
        private int[] memory; // addresses from 0x0c to 0x4f /-/ 0x8c to 0xcf


        private int wReg;

        private Stack<int> stack = new Stack<int>();

        #region overloaded special function Register on BANK 1
        private int OPTION;
        private int TRISA;
        private int TRISB;
        private int EECON1;
        private int EECON2;
        #endregion

        public void init()
        {
            memory = new int[81];
            wReg = 0;
            OPTION = 0;
            TRISA = 0;
            TRISB = 0;
            EECON1 = 0;
            EECON2 = 0;
            setFile(0x3, 0x18);
            setMemoryBankTo(1);
            setFile(0x1, 0xff);
            setFile(0x5, 0x1f);
            setFile(0x6, 0xff);
            setMemoryBankTo(0);
        }

        #region file access
        public int setFile(int fileAddress, int value)
        {
            int memoryBank = getStatusRP0();

            if (fileAddress == 0)
            {
                memory[memory[4]] = value;
                return value;
            }
            if (fileAddress <= 0x4f && (memoryBank == 0)) // Bank 0
            {
                if (fileAddress == 1)
                {
                    setTMR0(value);
                    return value;
                }

                memory[fileAddress] = value;
                return value;
            }
            if (fileAddress <= 0x4f && (memoryBank == 1)) // Bank 1
            {
                switch (fileAddress)
                {
                    case 0x01:
                        OPTION = value;
                        break;
                    case 0x05:
                        TRISA = value;
                        break;
                    case 0x06:
                        TRISB = value;
                        break;
                    case 0x08:
                        setEECON1(value);
                        break;
                    case 0x09:
                        setEECON2(value);
                        break;
                    default:
                        memory[fileAddress] = value;
                        break;
                }
                return value;
            }
            return 0;
        }

        public int getFile(int fileAddress)
        {
            if (fileAddress == 0)
            {
                return memory[memory[4]];
            }
            if (fileAddress <= 0x4f && (getStatusRP0() == 0)) // Bank 0
            {
                return memory[fileAddress];
            }
            if (fileAddress <= 0x4f && (getStatusRP0() == 1)) // Bank 1
            {
                switch (fileAddress)
                {
                    case 0x01:
                        return OPTION;
                    case 0x05:
                        return TRISA;
                    case 0x06:
                        return TRISB;
                    case 0x08:
                        return EECON1;
                    case 0x09:
                        return EECON2;
                    default:
                        return memory[fileAddress];
                }
            }
            return 0;
        }
        #endregion

        #region bit access
        public int getBit(int fileadress, int bitadress) // Bitaddress like : 0-7
        {
            int reg = getFile(fileadress);
            int result = reg & (0x1 << bitadress);
            if (result == 0) { return 0; }
            else { return 1; }
        }

        public void setBit(int reg, int bit)
        {
            if (getBit(reg, bit) == 0)
            {
                if (reg == 5 && bit == 4)
                {
                    GUI_Simu.controller.incTimer0ByExternalInput(false);
                }
                if (reg == 6 && bit == 0)
                {
                    GUI_Simu.interruptController.onRB0Changed(false);
                }

                if (reg == 6 && (bit == 4 || bit == 5 || bit == 6 || bit == 7))
                {
                    GUI_Simu.interruptController.onRB4TO7Changed();
                }
                int file = getFile(reg);
                int newFile = file + Convert.ToInt16(Math.Pow(2, bit));
                setFile(reg, newFile);
            }
        }

        public void clearBit(int reg, int bit)
        {
            if (getBit(reg, bit) == 1)
            {
                if (reg == 5 && bit == 4)
                {
                    GUI_Simu.controller.incTimer0ByExternalInput(true);
                }
                if (reg == 6 && bit == 0)
                {
                    GUI_Simu.interruptController.onRB0Changed(true);
                }
                if (reg == 6 && (bit == 4 || bit == 5 || bit == 6 || bit == 7))
                {
                    GUI_Simu.interruptController.onRB4TO7Changed();
                }
                int file = getFile(reg);
                int newfile = file - Convert.ToInt16(Math.Pow(2, bit));
                setFile(reg, newfile);
            } 
        }
        #endregion


        #region help functions

        public int setWReg(int value) { return wReg = value; }
        public int getWReg() { return wReg; }

        public int getStatusRP0()
        {
            int val = memory[0x3] & 0x20;
            if ((memory[0x3] & 0x20) > 0) { return 1; }
            return 0;
        }

        //public int readStack()
        //{
        //    return stack.Peek();
        //}

        //public void pushStack(int item)
        //{
        //    stack.Push(item);
        //}

        //public int popStack()
        //{
        //    int item = stack.Peek();
        //    stack.Pop();
        //    return item;
        //}

        public void incPC()
        {
            int pc = getFile(0x02);
            pc++;
            if (pc > 255)
            {
                pc = 0;
                incPCLatch();
            }
            setFile(0x02, pc);
        }

        private void incPCLatch()
        {
            int pcLatch = getFile(0x0a);
            pcLatch++;
            if (pcLatch > 3)
            {
                pcLatch = 0;
            }
            setFile(0x0a, pcLatch);
        }

        public int getFullPC()
        {
            return ((getFile(0x0a) & 0x3) << 8) | getFile(0x2);
        }

        public void setFullPC(int value)
        {
            setFile(0x02, (value & 0xff));
            int pcLatch = ((value & 0x300) >> 8);
            setFile(0x0a, pcLatch);
        }

        public int getOptionRegister()
        {
            return OPTION;
        }

        public int getCurrentMemoryBank()
        {
            if ((getFile(0x3) & 1 << 5) == 0)
            {
                return 0;
            }
            return 1;
        }

        public void setMemoryBankTo(int value)
        {
            int statusRegister = 0x03;
            int registerContent = getFile(statusRegister);
            if (value == 0)
            {
                registerContent &= ~(1 << 5);
            }
            else
            {
                registerContent |= 1 << 5;
            }
            setFile(statusRegister, registerContent);
        }

        private void setEECON1(int value)
        {
            if ((value & 0x2) != 0 && (value & 0x4) != 0)
            {
                GUI_Simu.eeprom.writeToEEPROM();
            }
            else if ((value & 0x1) != 0)
            {
                GUI_Simu.eeprom.readFromEEPROM();
            }
            EECON1 = value;
        }

        private void setEECON2(int value)
        {
            if (EECON2 == 0x55 && value == 0xaa)
            {
                GUI_Simu.eeprom.setStateMachineTriggered();
            }
            EECON2 = value;
        }

        public bool requestAccess(int reg, int bit)
        {
            bool access;
            if (reg == 0x05 || reg == 0x06) // TrisA TrisB Request
            {
                if (getBit(reg, bit) == 1) { access = true; }
                else { access = false; }
            }

            else { access = false; }

            return access;
        } 
        
        public void setTMR0(int timer0)
        {
           memory[1] = timer0;
           GUI_Simu.controller.resetPrescaleCounter();
        }
        #endregion


        public int getTMR0()
        {
            return memory[1];
        }
    }
}
