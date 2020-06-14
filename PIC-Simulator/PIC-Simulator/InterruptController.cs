using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator
{
    public class InterruptController
    {
        public enum InterruptFlags
        {
            T0IF,
            INTF,
            RBIF
        }
        public void setRB0Pin(int address)
        {
            throw new NotImplementedException();
        }

        public void setInterruptFlag(InterruptFlags flag)
        {            
            int bitAdress = decodeInterruptFlag(flag);
            if (isInterruptEnabled(bitAdress))
            {
                int intCon = GUI_Simu.memory.getFile(0xb);
                intCon |= (1 << bitAdress);
                GUI_Simu.memory.setFile(0xb, intCon);
            }
        }

        private bool isInterruptEnabled(int bitAdress)
        {
            bitAdress += 3; //bitadress of Enabled flag
            int intCon = GUI_Simu.memory.getFile(0xb);
            
            if ((intCon & (1 << bitAdress)) != 0)
            {
                return true;
            }

            return false;
        }

        private int decodeInterruptFlag(InterruptFlags flag)
        {
            switch (flag)
            {
                case InterruptFlags.T0IF: return 2;

                case InterruptFlags.INTF: return 1;

                case InterruptFlags.RBIF: return 0;

                default: return 0;
            }
        }

        public void checkInterrupts()
        {
            int intCon = GUI_Simu.memory.getFile(0xb);
            if ((intCon & 0x7) != 0 && (intCon & 1 << 7) != 0)
            {
                GUI_Simu.executer.interruptOccured();
            }
        }
    }
}
