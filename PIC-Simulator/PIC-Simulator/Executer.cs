using System.Deployment.Application;
using System.IO;

namespace PIC_Simulator
{
    class Executer
    {
        public Command executeCommand(Command command)
        {
            switch (command.getCommandName())
            {
                case CommandNames.ADDWF:
                    addWF(command.getDestinationSelect(), command.getFileAddress());
                    break;
                case CommandNames.ANDWF:
                    andWF(command.getDestinationSelect(), command.getFileAddress());
                    break;
                case CommandNames.CLRF:
                    clrF(command.getFileAddress());
                    break;
                case CommandNames.CLRW:
                    clrW();
                    break;
                case CommandNames.COMF:
                    comF(command.getDestinationSelect(), command.getFileAddress());
                    break;
                case CommandNames.DECF:
                    decF(command.getDestinationSelect(), command.getFileAddress());
                    break;
                case CommandNames.DECFSZ:
                    decFsZ(command.getDestinationSelect(), command.getFileAddress());
                    break;
                //case CommandNames.INCF:
                //    incF(command.getDestinationSelect(), command.getFileAddress());
                //    break;
                //case CommandNames.INCFSZ:
                //    incFsZ(command.getDestinationSelect(), command.getFileAddress());
                //    break;
                //case CommandNames.IORWF:
                //    iOrWF(command.getDestinationSelect(), command.getFileAddress());
                //    break;
                //case CommandNames.MOVF:
                //    movF(command.getDestinationSelect(), command.getFileAddress());
                //    break;
                //case CommandNames.MOVWF:
                //    movWF(command.getDestinationSelect(), command.getFileAddress());
                //    break;
                //case CommandNames.NOP:
                //    //TODO Watch cycle-Time and clearance of Carry-/Zero-Flag
                //    break;
                //case CommandNames.RLF:
                //    rlF(command.getDestinationSelect(), command.getFileAddress());
                //    break;
            }
            return command;
        }

        #region command functions
        private int addWF(bool isResultWrittenToW, int fileAddress)
        {
            int result = getWReg() + getFile(fileAddress);
            if (result < 255)
            {
                result -= 256;
                setCarryFlag();
            }
            //TODO DC Flag
            setZeroFlagIfNeeded(result);
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
            return fileAddress;
        }

        private int andWF(bool isResultWrittenToW, int fileAddress)
        {
            int result = getWReg() & getFile(fileAddress);
            setZeroFlagIfNeeded(result);
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
            return fileAddress;
        }

        private int clrF(int fileAddress)
        {
            writeResultToRightDestination(0, false, fileAddress);
            setZeroFlagTo(1);
            return fileAddress;
        }

        private int clrW()
        {
            writeResultToRightDestination(0, true, 0);
            setZeroFlagTo(1);
            return 0;
        }

        private int comF(bool isResultWrittenToW, int fileAddress)
        {
            int result = ~(getFile(fileAddress));
            setZeroFlagIfNeeded(result);
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
            return fileAddress;
        }

        private int decF(bool isResultWrittenToW, int fileAddress)
        {
            int result = getFile(fileAddress) - 1;
            if (result > 0)
            {
                result += 256;
            }
            setZeroFlagIfNeeded(result);
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
            return fileAddress;
        }

        private int decFsZ(bool isResultWrittenToW, int fileAddress)
        {
            int fileContent = getFile(fileAddress);
            if (fileContent > 0)
            {
                int result = fileContent - 1;
                writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
            }
            return fileAddress;
        }
        private int bitSetFile(int fileAddress, int bitAddress)
        {
            int registerContent = Program.memory.getFile(fileAddress);
            registerContent |= 1 << bitAddress;
            return Program.memory.setFile(fileAddress, registerContent);
        }

        private int bitClearFile(int fileAddress, int bitAddress)
        {
            int registerContent = Program.memory.getFile(fileAddress);
            registerContent &= ~(1 << bitAddress);
            return Program.memory.setFile(fileAddress, registerContent);
        }
        #endregion

        #region shortcut/help functions
        private void setZeroFlagTo(int value)
        {
            if (value == 0)
            {
                bitClearFile(0x03, 2);
            }
            else
            {
                bitSetFile(0x03, 2);
            }
        }

        private void setZeroFlagIfNeeded(int result)
        {
            if (result == 0)
            {
                setZeroFlagTo(1);
            }
        }

        private void setCarryFlag()
        {
            bitSetFile(0x03, 0);
        }

        private int writeResultToRightDestination(int result, bool isResultWrittenToW, int fileAddress)
        {
            if (isResultWrittenToW)
            {
                return Program.memory.setWReg(result);
            }
            return Program.memory.setFile(fileAddress, result);
        }

        private int getWReg()
        {
            return Program.memory.getWReg();
        }

        private int getFile(int fileAddress)
        {
            return Program.memory.getFile(fileAddress);
        }
        #endregion
    }
}
