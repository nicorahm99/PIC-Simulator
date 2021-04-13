using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator
{
    public class Prescaler : IPrescaler
    {
        private IMemory memory;

        public void init(Memory memory)
        {
            this.memory = memory;
        }

        public int getPrescaler()
        {
            int optionFile = getOptionFile(); // get the option file

            if ((optionFile & (1 << 3)) != 0)
            {
                return getWDTPrescaler();
            }
            return getTMR0Prescaler();
        }

        public int getWDTPrescaler()
        {
            int optionFile = getOptionFile();
            int prescalerBits = optionFile & 7;

            if ((optionFile & (1 << 3)) != 0)
            {
                return prescalerWDT[prescalerBits];
            }
            return 1;
        }
        public int getTMR0Prescaler()
        {
            int optionFile = getOptionFile();
            int prescalerBits = optionFile & 7;

            if ((optionFile & (1 << 3)) == 0)
            {
                return prescalerTMR0[prescalerBits];
            }

            return 1;
        }

        private readonly Dictionary<int, int> prescalerTMR0 = new Dictionary<int, int>() //prescaler with TMR0 Rate
        {
            {0, 2},
            {1, 4},
            {2, 8},
            {3, 16},
            {4, 32},
            {5, 64},
            {6, 128},
            {7, 256},
        };

        private readonly Dictionary<int, int> prescalerWDT = new Dictionary<int, int>() //prescaler with WDT Rate
        {
            {0, 1},
            {1, 2},
            {2, 4},
            {3, 8},
            {4, 16},
            {5, 32},
            {6, 64},
            {7, 128},
        };

        private int getOptionFile()
        {
            return memory.getOptionRegister();
        }
    }
}
