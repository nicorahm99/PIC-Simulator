namespace PIC_Simulator
{
    public interface IInterruptController
    {
        void checkInterrupts();
        void init(Memory memory, Executer executer);
        void onRB0Changed(bool isFallingEdge);
        void onRB4TO7Changed();
        void setInterruptFlag(InterruptController.InterruptFlags flag);
    }
}