namespace PIC_Simulator
{
    public interface IEEPROM
    {
        void init(IMemory memory);
        void readFromEEPROM();
        void setStateMachineTriggered();
        void writeToEEPROM();
    }
}