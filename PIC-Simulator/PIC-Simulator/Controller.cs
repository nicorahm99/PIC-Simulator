using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace PIC_Simulator
{
    public class Controller
    {
        private ROM rom;
        private InterruptController interruptController;
        private Memory memory;
        private Decoder decoder;
        private Executer executer;
        private Prescaler prescaler;



        private int prescaleCounter;
        List<int> breakPoints = new List<int>(); // list contains pcs of all breakpoints

        public void init(ROM rom, InterruptController interruptController, Memory memory, Decoder decoder, Executer executer, Prescaler prescaler)
        {
            this.rom = rom;
            this.interruptController = interruptController;
            this.memory = memory;
            this.decoder = decoder;
            this.executer = executer;
            this.prescaler = prescaler;

            reset();
        }

        public bool step()
        {
            interruptController.checkInterrupts();
            int pc = memory.getFullPC();
            bool isInList = breakPoints.IndexOf(pc) != -1; //returns true if pc is in breakpoint-list
            if (isInList)
            {
                return true;
            }

            int commandCode = rom.fetchCommand(pc);
            Command command = decoder.decodeCommand(commandCode);
            executer.executeCommand(command);
            memory.incPC();
            incTimer0ByProgram();
            //reset watchdog
            return false;
        }

        public void reset()
        {
            memory.setFullPC(0);
        }

        private void incTimer0() // timer 0 overflow sets T0IF
        {
            int timer0 = memory.getTMR0();
            if (timer0 < 0xFF)
            {
                timer0++;
            }
            else
            {
                timer0 = 0;
                interruptController.setInterruptFlag(InterruptController.InterruptFlags.T0IF);
            }

            memory.setTMR0(timer0);
        }

        public void incTimer0ByExternalInput(bool isFallingEdge)
        {
            int optionRegister = memory.getOptionRegister();
            if ((optionRegister & 1 << 5) != 0)
            {
                if ((optionRegister & 1 << 4) != 0 && isFallingEdge || (optionRegister & 1 << 4) == 0 && !isFallingEdge)
                {
                    incTimer0RegardingPrescaler();
                }
            }
        }

        public void incTimer0RegardingPrescaler()
        {
            if (prescaleCounter < prescaler.getTMR0Prescaler() - 1)
            {
                prescaleCounter++;
            }
            else
            {
                prescaleCounter = 0;
                incTimer0();
            }
        }

        public void incTimer0ByProgram()
        {
            if ((memory.getOptionRegister() & 1 << 5) == 0)
            {
                incTimer0RegardingPrescaler();
            }
        }

        public void setBreakPoints(List<int> bPs)
        {
            breakPoints = bPs;
        }

        public void resetPrescaleCounter()
        {
            prescaleCounter = 0;
        }
    }
}