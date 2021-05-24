using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator
{
    public class Stack : IStack
    {
        private static volatile Stack instance;
        public static Stack Instance
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                if (instance == null)
                    instance = new Stack();
                return instance;
            }
        }

        private int optionCounter = 0;
        private List<int> stack = new List<int>();

        public void init()
        {
            optionCounter = 0;
            stack = new List<int>();
        }

        public void push(int pc)
        {
            if (optionCounter < 8)
            {
                stack.Add(pc);
                optionCounter++;
            }
            else if (optionCounter >= 8)
            {
                stack[CountertoIndex()] = pc;
                optionCounter++;
            }
        }

        public int pop()
        {
            if (optionCounter > 0 && optionCounter <= 8)
            {
                int value = stack[(optionCounter - 1)];
                stack.RemoveAt((optionCounter - 1));
                optionCounter--;
                return value;
            }
            else if (optionCounter > 8)
            {
                int value = stack[7];
                stack.RemoveAt(7);
                optionCounter = 7;
                return value;
            }
            else// if optionCounter == 0
            {
                return 8191; //1 1111 1111 1111
            }
        }

        public int peek()
        {
            if (optionCounter > 0 && optionCounter <= 8)
            {
                return stack[optionCounter - 1];
            }
            else if (optionCounter > 8)
            {
                return stack[7];
            }
            else { return 0; }
        }

        public List<int> get()
        {
            return stack;
        }

        private int CountertoIndex()
        {
            if (optionCounter >= 8)
            {
                int help = optionCounter;
                while (help >= 8)
                {
                    help -= 8;
                }
                return (help);
            }
            else { return 9; } // should lead to exception
        }
    }
}
