namespace PIC_Simulator
{
    public interface IDecoder
    {
        Command decodeCommand(int commandCode);
        void init(Memory memory, Controller controller, Stack stack);
    }
}