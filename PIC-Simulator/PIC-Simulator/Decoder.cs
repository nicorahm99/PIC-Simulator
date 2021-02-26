using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator
{
    public class Decoder
    {
        private const int sevenBitMask = 0x3f80; //2)
        private const int sixBitMask = 0x3f00; //1)
        private const int fiveBitMask = 0x3e00; //4)
        private const int fourBitMask = 0x3c00; //3)
        private const int threeBitMask = 0x3800; //5)


        private const int clrwdtCommand = 0x64;
        private const int retfieCommand = 0x9;
        private const int returnCommand = 0x8;
        private const int sleepCommand = 0x63;

        public Decoder(){}

        public Command decodeCommand(int commandCode)
        {
            Command resultCommand = new Command();
            resultCommand.setCommandName(CommandNames.ERROR);

            if (isStaticCommand(commandCode) != CommandNames.ERROR)
            {
                resultCommand.setCommandName(isStaticCommand(commandCode));
                return resultCommand;
            }

            #region 7 Bit masked Commands

            switch (commandCode & sevenBitMask)
            {
                case 0x180:
                    addNameFileToResult(CommandNames.CLRF);
                    break;

                case 0x100:
                    resultCommand.setCommandName(CommandNames.CLRW);
                    break;

                case 0x80:
                    addNameFileToResult(CommandNames.MOVWF);
                    break;
            }
            #endregion

            if (resultCommand.getCommandName() != CommandNames.ERROR) { return resultCommand; }

            #region 6 Bit Masked Commands

            switch (commandCode & sixBitMask)
            {
                case 0x700:
                    addNameDestAddressToResult(CommandNames.ADDWF);
                    break;

                case 0x500:
                    addNameDestAddressToResult(CommandNames.ANDWF);
                    break;

                case 0x900:
                    addNameDestAddressToResult(CommandNames.COMF);
                    break;

                case 0x300:
                    addNameDestAddressToResult(CommandNames.DECF);
                    break;

                case 0xb00:
                    addNameDestAddressToResult(CommandNames.DECFSZ);
                    break;

                case 0xa00:
                    addNameDestAddressToResult(CommandNames.INCF);
                    break;

                case 0xf00:
                    addNameDestAddressToResult(CommandNames.INCFSZ);
                    break;

                case 0x400:
                    addNameDestAddressToResult(CommandNames.IORWF);
                    break;

                case 0x800:
                    addNameDestAddressToResult(CommandNames.MOVF);
                    break;

                case 0xd00:
                    addNameDestAddressToResult(CommandNames.RLF);
                    break;

                case 0xc00:
                    addNameDestAddressToResult(CommandNames.RRF);
                    break;

                case 0x200:
                    addNameDestAddressToResult(CommandNames.SUBWF);
                    break;

                case 0xe00:
                    addNameDestAddressToResult(CommandNames.SWAPF);
                    break;

                case 0x600:
                    addNameDestAddressToResult(CommandNames.XORWF);
                    break;

                case 0x3900:
                    addNameLiteralToResult(CommandNames.ANDLW);
                    break;

                case 0x3800:
                    addNameLiteralToResult(CommandNames.IORLW);
                    break;

                case 0x3a00:
                    addNameLiteralToResult(CommandNames.XORLW);
                    break;
            }
            #endregion

            if (resultCommand.getCommandName() != CommandNames.ERROR) { return resultCommand; }

            #region 5 Bit Masked Commands

            switch (commandCode & fiveBitMask) 
            {
                case 0x3e00:
                    addNameLiteralToResult(CommandNames.ADDLW);
                    break;

                case 0x3c00:
                    addNameLiteralToResult(CommandNames.SUBLW);
                    break;
            }
            #endregion

            if (resultCommand.getCommandName() != CommandNames.ERROR) { return resultCommand; }

            #region 4 Bit Masked Commands
            switch (commandCode & fourBitMask)
            {
                case 0x1000:
                    addNameBitaddrFileaddrToResult(CommandNames.BCF);
                    break;

                case 0x1400:
                    addNameBitaddrFileaddrToResult(CommandNames.BSF);
                    break;

                case 0x1800:
                    addNameBitaddrFileaddrToResult(CommandNames.BTFSC);
                    break;

                case 0x1c00:
                    addNameBitaddrFileaddrToResult(CommandNames.BTFSS);
                    break;

                case 0x3000:
                    addNameLiteralToResult(CommandNames.MOVLW);
                    break;

                case 0x3400:
                    addNameLiteralToResult(CommandNames.RETLW);
                    break;
            }
            #endregion

            if (resultCommand.getCommandName() != CommandNames.ERROR) { return resultCommand; }

            #region 3 Bit Masked Commands
            switch (commandCode & threeBitMask)
            {
                case 0x2000:
                    resultCommand.setCommandName(CommandNames.CALL);
                    resultCommand.setLiteral(extractJumpAddress(commandCode));
                    break;

                case 0x2800:
                    resultCommand.setCommandName(CommandNames.GOTO);
                    resultCommand.setLiteral(extractJumpAddress(commandCode));
                    break;
            }
            #endregion

            return resultCommand;

            #region Help functions
            void addNameDestAddressToResult(CommandNames commandName)
            {
                resultCommand.setCommandName(commandName);
                resultCommand.setDestinationSelect(isResultWrittenToW(commandCode));
                resultCommand.setFileAddress(extractFileAddress(commandCode));
            }

            void addNameBitaddrFileaddrToResult(CommandNames commandName)
            {
                resultCommand.setCommandName(commandName);
                resultCommand.setBitAddress(extractBitAddress(commandCode));
                resultCommand.setFileAddress(extractFileAddress(commandCode));
            }

            void addNameFileToResult(CommandNames commandName)
            {
                resultCommand.setCommandName(commandName);
                resultCommand.setFileAddress(extractFileAddress(commandCode));
            }

            void addNameLiteralToResult(CommandNames commandName)
            {
                resultCommand.setCommandName(commandName);
                resultCommand.setLiteral(extractLiteral(commandCode));
            }
            #endregion
        }

        private CommandNames isStaticCommand(int commandCode)
        {
            CommandNames commandName = CommandNames.ERROR;
            
            switch (commandCode)
            {
                case clrwdtCommand:
                    commandName = CommandNames.CLRWDT;
                    break;
                case retfieCommand:
                    commandName = CommandNames.RETFIE;
                    break;
                case returnCommand:
                    commandName = CommandNames.RETURN;
                    break;
                case sleepCommand:
                    commandName = CommandNames.SLEEP;
                    break;
                case 96:
                case 64:
                case 32:
                case 0:
                    commandName = CommandNames.NOP;
                    break;
            }

            return commandName;
        }
        
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
    }
}