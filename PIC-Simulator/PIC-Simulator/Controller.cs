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
        public int timer0 = 0;
        public int watchdog = 0;

        public void step()
        {
            incTimer0();
            int index = GUI_Simu.memory.getFullPC();
            Command command = GUI_Simu.decoder.decodeCommand(GUI_Simu.rom.fetchCommand(GUI_Simu.memory.getFullPC()));
            GUI_Simu.memory.incPC();
            GUI_Simu.executer.executeCommand(command);
            //reset watchdog
        }

        //public void sequence()
        //{
        //    while (isActive && (GUI_Simu.memory.getFullPC() < 1024))
        //    {
        //        step();
        //    }
        //}

        public void reset()
        {
            timer0 = 0;
            watchdog = 0;
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
                //GUI_Simu.memory.set T0IF (Intcon -> 2 --> 0x0B, 2)
            }
        }

        private void resetWatchdog()
        {
            watchdog = 0;
        }
    }
}
