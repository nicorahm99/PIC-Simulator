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

    public class Command
    {
        public Command() { }

        private CommandNames commandName; // eaquals Mnemonic

        public void setCommandName(CommandNames value) { commandName = value; }
        public CommandNames getCommandName() { return commandName; }

        private int fileAdress; // eaquals operand f

        public void setFileAdress(int value) { fileAdress = value; }
        public int getFileAdress() { return fileAdress; }

        private bool destinationSelect; // eaquals operand d

        public void setDestinationSelect(bool value) { destinationSelect = value; }
        public bool getDestinationSelect() { return destinationSelect; }

        private int bitAddress; // eaquals operand b

        public void setBitAddress(int value) { bitAddress = value; }
        public int getBitAddress() { return bitAddress; }

        private int literal; // eaquals operand k

        public void setLiteral(int value) { literal = value; }
        public int getLiteral() { return literal; }
    }
}
