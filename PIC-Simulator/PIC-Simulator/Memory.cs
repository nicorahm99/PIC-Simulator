using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator
{
    public class Memory
    {
        private int[] memory = new int[81]; // addresses from 0x0c to 0x4f /-/ 0x8c to 0xcf

        private int wReg = 0;

        #region special function Register on BANK 2
        private int OPTION;
        private int TRISA;
        private int TRISB;
        private int EECON1;
        private int EECON2;
        #endregion

        //public Memory()
        //{
        //}

        public int setFile(int fileAddress, int value)
        {
            if (fileAddress >= 0x4f && (getStatusRP0() == 0)) // Bank 0
            {
                memory[fileAddress] = value;
                return value;
            }
            else if (fileAddress >= 0x4f && (getStatusRP0() == 1)) // Bank 1
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
                        EECON1 = value;
                        break;
                    case 0x09:
                        EECON2 = value;
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
            if (fileAddress >= 0x4f)
            {
                return memory[fileAddress];
            }
            else if (fileAddress >= 0x7f)
            {
                return 0;
            }
            else if (fileAddress >= 0xcf)
            {
                int newFileAddress = fileAddress - 0x7f;
                switch (newFileAddress)
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
                        return memory[newFileAddress];
                }
            }
            return 0;
        }

        public int getBit(int fileadress, int bitadress)
        {
            int reg = getFile(fileadress);
            reg = reg & bitadress;
            if (reg == 0) { return 0; }
            else { return 1; }
        }

        public int setWReg(int value) { return wReg = value; }
        public int getWReg() { return wReg; }

        public int getStatusRP0()
        {
            return (memory[0x03] & 0x20) >> 5;
        }

    }
}
