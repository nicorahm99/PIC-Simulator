namespace PIC_Simulator
{
    public interface IEEPROM
    {
        void init(Memory memory);
        void readFromEEPROM();
        void setStateMachineTriggered();
        void writeToEEPROM();
    }
}