namespace PIC_Simulator
{
    public interface IMemory
    {
        void clearBit(int reg, int bit);
        int getBit(int fileadress, int bitadress);
        int getCurrentMemoryBank();
        int getFile(int fileAddress);
        int getFullPC();
        int getOptionRegister();
        int getStatusRP0();
        int getTMR0();
        int getWReg();
        void incPC();
        void init(Controller controller, InterruptController interruptController, EEPROM eeprom);
        bool requestAccess(int reg, int bit);
        void reset();
        void setBit(int reg, int bit);
        int setFile(int fileAddress, int value);
        void setFullPC(int value);
        void setMemoryBankTo(int value);
        void setTMR0(int timer0);
        int setWReg(int value);
    }
}