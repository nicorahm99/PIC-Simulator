namespace PIC_Simulator
{
    public interface IPrescaler
    {
        int getPrescaler();
        int getTMR0Prescaler();
        int getWDTPrescaler();
        void init(Memory memory);
    }
}