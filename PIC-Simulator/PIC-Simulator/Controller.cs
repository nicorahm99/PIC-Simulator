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
        private int timer0;
        private int watchdog;

        public Controller()
        {
            init();
        }

        public void step()
        {
            GUI_Simu.interruptController.checkInterrupts();
            int pc = GUI_Simu.memory.getFullPC();
            int commandCode = GUI_Simu.rom.fetchCommand(pc);
            Command command = GUI_Simu.decoder.decodeCommand(commandCode);
            GUI_Simu.executer.executeCommand(command);
            GUI_Simu.memory.incPC();
            incTimer0();
//reset watchdog
        }

        //public void sequence()
        //{
        //    while (isActive && (GUI_Simu.memory.getFullPC() < 1024))
        //    {
        //        step();
        //    }
        //}

        public void init()
        {
            timer0 = 0;
            watchdog = 0;
            GUI_Simu.memory.setFullPC(0);
        }

        public int get_timer0()
        {
            return timer0;
        }

        private void incTimer0() // timer 0 overflow sets T0IF
        {
            if (timer0 < 0xFF) { timer0++; }
            else
            {
                timer0 = 0;
                GUI_Simu.interruptController.setInterruptFlag(InterruptController.InterruptFlags.T0IF);
            }
        }

        private void resetWatchdog()
        {
            watchdog = 0;
        }
    }
}
