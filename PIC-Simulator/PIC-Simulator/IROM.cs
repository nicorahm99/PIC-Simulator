using System.Collections.Generic;

namespace PIC_Simulator
{
    public interface IROM
    {
        int fetchCommand(int address);
        void init();
        void setRom(List<int> rom);
    }
}