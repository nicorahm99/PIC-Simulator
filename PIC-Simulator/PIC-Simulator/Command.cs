using System.Collections.Specialized;
using System.Dynamic;

namespace PIC_Simulator
{
    public enum CommandNames
    {
        ADDWF,
        ANDWF,
        CLRF,
        CLRW,
        COMF,
        DECF,
        DECFSZ,
        INCF,
        INCFSZ,
        IORWF,
        MOVF,
        MOVWF,
        NOP,
        RLF,
        RRF,
        SUBWF,
        SWAPF,
        XORWF,
        BCF,
        BSF,
        BTFSC,
        BTFSS,
        ADDLW,
        ANDLW,
        CALL,
        CLRWDT,
        GOTO,
        IORLW,
        MOVLW,
        RETFIE,
        RETLW,
        RETURN,
        SLEEP,
        SUBLW,
        XORLW,
        ERROR
    }

    public abstract class Command
    {
        protected Memory memory;
        protected Controller controller;
        protected Stack stack;
        public abstract void execute();

        protected int fileAddress; // eaquals operand f
        public void setFileAddress(int value) { fileAddress = value; }
        public int getFileAddress() { return fileAddress; }

        protected bool destinationSelect; // eaquals operand d
        public void setDestinationSelect(bool value) { destinationSelect = value; }
        public bool getDestinationSelect() { return destinationSelect; }

        protected int bitAddress; // eaquals operand b
        public void setBitAddress(int value) { bitAddress = value; }
        public int getBitAddress() { return bitAddress; }

        protected int literal; // eaquals operand k
        public void setLiteral(int value) { literal = value; }
        public int getLiteral() { return literal; }



        #region shortcut/help functions

        protected int clearBit(int fileAddress, int bitAddress)
        {
            int registerContent = memory.getFile(fileAddress);
            registerContent &= ~(1 << bitAddress);
            return memory.setFile(fileAddress, registerContent);
        }

        protected int setBit(int fileAddress, int bitAddress)
        {
            int registerContent = memory.getFile(fileAddress);
            registerContent |= 1 << bitAddress;
            return memory.setFile(fileAddress, registerContent);
        }

        protected void setZeroFlagTo(int value)
        {
            if (value == 0)
            {
                clearBit(0x03, 2);
            }
            else
            {
                setBit(0x03, 2);
            }
        }

        protected void setZeroFlagIfNeeded(int result)
        {
            if (result == 0)
            {
                setZeroFlagTo(1);
            }
            else
            {
                setZeroFlagTo(0);
            }
        }

        protected void setCarryFlagTo(int value)
        {
            if (value == 0)
            {
                clearBit(0x03, 0);
            }
            else
            {
                setBit(0x03, 0);
            }
        }

        protected void setCarryFlagIfNeeded(int result)
        {
            if (result > 255)
            {
                setCarryFlagTo(1);
            }
            else
            {
                setCarryFlagTo(0);
            }
        }

        protected void setDigitCarryFlagIfNeeded(int result)
        {
            if (result > 15)
            {
                setDigitCarryFlagTo(1);
            }
            else
            {
                setDigitCarryFlagTo(0);
            }
        }

        protected void setCarryFlagsForSub(int result, int fourBitResult)
        {
            if (result <= 255 && result >= 0)
            {
                setCarryFlagTo(1);
            }
            else
            {
                setCarryFlagTo(0);
            }

            if (fourBitResult <= 255 && fourBitResult >= 0)
            {
                setDigitCarryFlagTo(1);
            }
            else
            {
                setDigitCarryFlagTo(0);
            }
        }

        protected void setDigitCarryFlagTo(int value)
        {
            if (value == 0)
            {
                clearBit(0x03, 1);
            }
            else
            {
                setBit(0x03, 1);
            }
        }

        protected int writeResultToRightDestination(int result, bool isResultWrittenToW, int fileAddress)
        {
            if (isResultWrittenToW)
            {
                return memory.setWReg(result);
            }

            return memory.setFile(fileAddress, result);
        }

        protected int getWReg()
        {
            return memory.getWReg();
        }

        protected int getFile(int fileAddress)
        {
            return memory.getFile(fileAddress);
        }

        protected void popStackToPc()
        {
            memory.setFullPC(stack.pop());
        }
        #endregion
    }
}
