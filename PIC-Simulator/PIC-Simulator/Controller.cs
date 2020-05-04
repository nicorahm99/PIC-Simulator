using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PIC_Simulator
{
    public class Controller
    {
        public bool isActive = false;
        public int taktCount = 0;
        public void step()
        {
            taktCount++;
            Command command = GUI_Simu.decoder.decodeCommand(GUI_Simu.rom.fetchCommand(GUI_Simu.memory.getFullPC()));
            GUI_Simu.memory.incPC();
            GUI_Simu.executer.executeCommand(command);
            //reset watchdog
            //inc laufzeit
        }

        public void sequence()
        {
            while (isActive && (GUI_Simu.memory.getFullPC() < 1024))
            {
                step();
            }
        }
    }
}
