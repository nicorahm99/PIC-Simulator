using PIC_Simulator.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator
{
    public class Decoder : IDecoder
    {
        private IMemory memory;
        private IController controller;
        private IStack stack;

        private const int sevenBitMask = 0x3f80; //2)
        private const int sixBitMask = 0x3f00; //1)
        private const int fiveBitMask = 0x3e00; //4)
        private const int fourBitMask = 0x3c00; //3)
        private const int threeBitMask = 0x3800; //5)


        private const int clrwdtCommand = 0x64;
        private const int retfieCommand = 0x9;
        private const int returnCommand = 0x8;
        private const int sleepCommand = 0x63;

        Command command;

        public void init(Memory memory, Controller controller, Stack stack)
        {
            this.memory = memory;
            this.controller = controller;
            this.stack = stack;
        }

        public Command decodeCommand(int commandCode)
        {
            if (isStaticCommand(commandCode)) { return command; }

            if (is7BitMasked(commandCode)) { return command; }

            if (is6BitMasked(commandCode)) { return command; }

            if (is5BitMasked(commandCode)) { return command; }

            if (is4BitMasked(commandCode)) { return command; }

            if (is3BitMasked(commandCode)) { return command; }

            return command; //should never be reached --> command undefined
        }


        #region Generate Command Instance
        private bool isStaticCommand(int commandCode)
        {

            switch (commandCode)
            {
                case clrwdtCommand:
                    command = new CLRWDT();
                    return true;
                case retfieCommand:
                    command = new RETFIE(controller, memory);
                    return true;
                case returnCommand:
                    command = new RETURN(controller, memory);
                    return true;
                case sleepCommand:
                    command = new SLEEP(memory);
                    return true;
                case 96:
                case 64:
                case 32:
                case 0:
                    command = new NOP();
                    return true;
            }
            return false;
        }

        private bool is7BitMasked(int commandCode)
        {
            int fileAddress;
            switch (commandCode & sevenBitMask)
            {
                case 0x180:
                    fileAddress = extractFileAddress(commandCode);
                    command = new CLRF(fileAddress, memory);
                    return true;

                case 0x100:
                    command = new CLRW(memory);
                    return true;

                case 0x80:
                    fileAddress = extractFileAddress(commandCode);
                    command = new MOVWF(fileAddress, memory);
                    return true;
            }
            return false;
        }

        private bool is6BitMasked(int commandCode)
        {
            bool isWrittenIntoW = isResultWrittenToW(commandCode);
            int fileAddress = extractFileAddress(commandCode);
            int literal = extractLiteral(commandCode);
            switch (commandCode & sixBitMask)
            {
                case 0x700:
                    command = new ADDWF(isWrittenIntoW, fileAddress, memory);
                    return true;

                case 0x500:
                    command = new ANDWF(isWrittenIntoW, fileAddress, memory);
                    return true;

                case 0x900:
                    command = new COMF(isWrittenIntoW, fileAddress, memory);
                    return true;

                case 0x300:
                    command = new DECF(isWrittenIntoW, fileAddress, memory);
                    return true;

                case 0xb00:
                    command = new DECFSZ(isWrittenIntoW, fileAddress, memory, controller);
                    return true;

                case 0xa00:
                    command = new INCF(isWrittenIntoW, fileAddress, memory);
                    return true; 

                case 0xf00:
                    command = new INCFSZ(isWrittenIntoW, fileAddress, memory, controller);
                    return true; 

                case 0x400:
                    command = new IORWF(isWrittenIntoW, fileAddress, memory);
                    return true; 

                case 0x800:
                    command = new MOVF(isWrittenIntoW, fileAddress, memory);
                    return true; 

                case 0xd00:
                    command = new RLF(isWrittenIntoW, fileAddress, memory);
                    return true; 

                case 0xc00:
                    command = new RRF(isWrittenIntoW, fileAddress, memory);
                    return true; 

                case 0x200:
                    command = new SUBWF(isWrittenIntoW, fileAddress, memory);
                    return true; 

                case 0xe00:
                    command = new SWAPF(isWrittenIntoW, fileAddress, memory);
                    return true; 

                case 0x600:
                    command = new XORWF(isWrittenIntoW, fileAddress, memory);
                    return true; 

                case 0x3900:
                    command = new ANDLW(literal, memory);
                    return true; 

                case 0x3800:
                    command = new IORLW(literal, memory);
                    return true; 

                case 0x3a00:
                    command = new XORLW(literal, memory);
                    return true; 
            }
            return false;
        }

        private bool is5BitMasked(int commandCode)
        {
            int literal = extractLiteral(commandCode);
            switch (commandCode & fiveBitMask)
            {
                case 0x3e00:
                    command = new ADDLW(literal, memory);
                    return true;

                case 0x3c00:
                    command = new SUBLW(literal, memory);
                    return true;
            }
            return false;
        }

        private bool is4BitMasked(int commandCode)
        {
            int bitAddress = extractBitAddress(commandCode);
            int fileAddress = extractFileAddress(commandCode);
            int literal = extractLiteral(commandCode);
            switch (commandCode & fourBitMask)
            {
                case 0x1000:
                    command = new BCF(fileAddress, bitAddress, memory);
                    return true;

                case 0x1400:
                    command = new BSF(fileAddress, bitAddress, memory);
                    return true;

                case 0x1800:
                    command = new BTFSC(fileAddress, bitAddress, memory, controller);
                    return true;

                case 0x1c00:
                    command = new BTFSS(fileAddress, bitAddress, memory, controller);
                    return true;

                case 0x3000:
                    command = new MOVLW(literal, memory);
                    return true;

                case 0x3400:
                    command = new RETLW(literal, controller, memory);
                    return true;
            }
            return false;
        }

        private bool is3BitMasked(int commandCode)
        {
            int jumpAddress = extractJumpAddress(commandCode);
            switch (commandCode & threeBitMask)
            {
                case 0x2000:
                    command = new CALL(jumpAddress, stack, memory, controller);
                    return true;

                case 0x2800:
                    command = new GOTO(jumpAddress, memory, controller);
                    return true;
            }
            return false;
        }
        #endregion


        #region Help functions
        private int extractFileAddress(int commandCode)
        {
            return commandCode & 0x7f;
        }

        private int extractBitAddress(int commandCode)
        {
            return (commandCode & 0x380) >> 7;
        }

        private int extractJumpAddress(int commandCode)
        {
            return commandCode & 0x7ff;
        }

        private bool isResultWrittenToW(int commandCode)
        {
            return (commandCode & 0x80).Equals(0);
        }

        private int extractLiteral(int commandCode)
        {
            return commandCode & 0xff;
        }
        #endregion
    }
}