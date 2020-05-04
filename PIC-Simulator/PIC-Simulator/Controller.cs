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
        public void takt()
        {
            taktCount++;
            Command command = GUI_Simu.decoder.decodeCommand(GUI_Simu.rom.fetchCommand(GUI_Simu.memory.getFullPC);
            GUI_Simu.memory.incPC();
        }

        public void execute()
        {
            for (int i = 0; i < GUI_Simu.rom.getLength(); i++)
            {
                takt();
            }
        }
    }
}
