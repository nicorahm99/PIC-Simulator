using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator
{
    public class InterruptController : IInterruptController
    {
        private IMemory memory;
        private IExecuter executer;

        public void init(Memory memory, Executer executer)
        {
            this.memory = memory;
            this.executer = executer;
        }
        public enum InterruptFlags
        {
            T0IF,
            INTF,
            RBIF
        }

        public void onRB0Changed(bool isFallingEdge)
        {
            int optionRegister = memory.getOptionRegister();

            if ((optionRegister & 2 ^ 6) == 0 && isFallingEdge)
            {
                setInterruptFlag(InterruptFlags.INTF);
            }
            else if ((optionRegister & 2 ^ 6) != 0 && !isFallingEdge)
            {
                setInterruptFlag(InterruptFlags.INTF);
            }
        }

        public void onRB4TO7Changed()
        {
            setInterruptFlag(InterruptFlags.RBIF);
        }

        public void setInterruptFlag(InterruptFlags flag)
        {
            int bitAdress = decodeInterruptFlag(flag);
            if (isInterruptEnabled(bitAdress))
            {
                int intCon = memory.getFile(0xb);
                intCon |= (1 << bitAdress);
                memory.setFile(0xb, intCon);
            }
        }

        private bool isInterruptEnabled(int bitAdress)
        {
            bitAdress += 3; //bitadress of Enabled flag
            int intCon = memory.getFile(0xb);

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
            int intCon = memory.getFile(0xb);
            if ((intCon & 0x7) != 0 && (intCon & 1 << 7) != 0)
            {
                executer.interruptOccured();
            }
        }
    }
}