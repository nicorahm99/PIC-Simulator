using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PIC_Simulator
{
    public class Controller
    {
        public int taktCount = 0;

        public void step()
        {
            taktCount++;
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

        public int get_taktCount()
        {
            return taktCount;
        }

        public void reset_taktCount()
        {
            taktCount = 0;
        }
    }
}
