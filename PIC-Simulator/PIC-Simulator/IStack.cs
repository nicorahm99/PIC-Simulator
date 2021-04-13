using System.Collections.Generic;

namespace PIC_Simulator
{
    public interface IStack
    {
        List<int> get();
        void init();
        int peek();
        int pop();
        void push(int pc);
    }
}