using System.Collections.Generic;

namespace PIC_Simulator
{
    public interface IController
    {
        void incTimer0ByExternalInput(bool isFallingEdge);
        void incTimer0ByProgram();
        void incTimer0RegardingPrescaler();
        void init(ROM rom, InterruptController interruptController, Memory memory, Decoder decoder, Executer executer, Prescaler prescaler);
        void reset();
        void resetPrescaleCounter();
        void setBreakPoints(List<int> bPs);
        bool step();
    }
}