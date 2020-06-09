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
        private int prescaler;
        private int watchdog;
        List<int> breakPoints = new List<int>(); // list contains pcs of all breakpoints

        public Controller()
        {
            init();
        }

        public bool step()
        {
            GUI_Simu.interruptController.checkInterrupts();
            int pc = GUI_Simu.memory.getFullPC();
            bool isInList = breakPoints.IndexOf(pc) != -1;
            if (isInList)
            {
                return true;
            }
            else
            {
                int commandCode = GUI_Simu.rom.fetchCommand(pc);
                Command command = GUI_Simu.decoder.decodeCommand(commandCode);
                GUI_Simu.executer.executeCommand(command);
                GUI_Simu.memory.incPC();
                incTimerWithPrescaler();
                //reset watchdog
                return false;
            }

        }

        public void init()
        {
            watchdog = 0;
            GUI_Simu.memory.setFullPC(0);
        }

        private void incTimer0() // timer 0 overflow sets T0IF
        {
            int timer0 = GUI_Simu.memory.getTMR0();
            if (timer0 < 0xFF) { timer0++; }
            else
            {
                timer0 = 0;
                GUI_Simu.interruptController.setInterruptFlag(InterruptController.InterruptFlags.T0IF);
            }

            GUI_Simu.memory.setTMR0(timer0);
        }

        public void incTimerWithPrescaler()
        {
            if (prescaler < GUI_Simu.prescaler.getTMR0Prescaler() - 1)
            {
                prescaler++;
            }
            else
            {
                prescaler = 0;
                incTimer0();
            }
        }

        public void setBreakPoints(List<int> bPs)
        {
            breakPoints = bPs;
        }

        private void resetWatchdog()
        {
            watchdog = 0;
        }
    }
}
