using System;
using System.Collections.Generic;
using System.Linq;
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
            
        }

        public void setInterruptFlag(InterruptFlags flag)
        {
            int bitAdress = decodeInterruptFlag(flag);
            int intCon = GUI_Simu.memory.getFile(0xb);
            intCon |= (1 << bitAdress);
            // TODO call interrupt routine ?
            GUI_Simu.memory.setFile(0xb, intCon);
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
    }
}
