namespace PIC_Simulator
{
    public interface IExecuter
    {
        void executeCommand(Command command);
        void init(Memory memory, Stack stack);
        void interruptOccured();
    }
}