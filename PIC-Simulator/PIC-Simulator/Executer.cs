using System.Deployment.Application;
using System.Dynamic;
using System.IO;

namespace PIC_Simulator
{
    public class Executer
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
                case CommandNames.INCF:
                    incF(command.getDestinationSelect(), command.getFileAddress());
                    break;
                case CommandNames.INCFSZ:
                    incFsZ(command.getDestinationSelect(), command.getFileAddress());
                    break;
                case CommandNames.IORWF:
                    iOrWF(command.getDestinationSelect(), command.getFileAddress());
                    break;
                case CommandNames.MOVF:
                    movF(command.getDestinationSelect(), command.getFileAddress());
                    break;
                case CommandNames.MOVWF:
                    movWF(command.getFileAddress());
                    break;
                case CommandNames.NOP:
                    //TODO Watch cycle-Time and clearance of Carry-/Zero-Flag
                    break;
                case CommandNames.RLF:
                    rlF(command.getDestinationSelect(), command.getFileAddress());
                    break;
                case CommandNames.RRF:
                        rrF(command.getDestinationSelect(), command.getFileAddress());
                    break;
                case CommandNames.SUBWF:
                    subWF(command.getDestinationSelect(), command.getFileAddress());
                    break;
                case CommandNames.SWAPF:
                    swapF(command.getDestinationSelect(), command.getFileAddress());
                    break;
                case CommandNames.XORWF:
                    xOrWF(command.getDestinationSelect(), command.getFileAddress());
                    break;
            }
            return command;
        }

        #region command functions
        private int addWF(bool isResultWrittenToW, int fileAddress)
        {
            int wContent = getWReg();
            int fileContent = getFile(fileAddress);
            int result = wContent + fileContent;
            int fourBitResult = (wContent & 0xf) + (fileContent & 0xf);
            setCarryFlagIfNeeded(result);
            if (result < 255)
            {
                result -= 256;
            }
            if (fourBitResult < 15)
            {
                setDigitCarryFlag();
            }
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

        private int incF(bool isResultWrittenToW, int fileAddress)
        {
            int result = getFile(fileAddress) + 1;
            if (result > 255)
            {
                result -= 256;
            }
            setZeroFlagIfNeeded(result);
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
            return fileAddress;
        }

        private int incFsZ(bool isResultWrittenToW, int fileAddress)
        {
            int fileContent = getFile(fileAddress);
            if (fileContent != 0)
            {
                int result = fileContent + 1;
                if (result > 255)
                {
                    result -= 256;
                }
                writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
            }
            return fileAddress;
        }

        private int iOrWF(bool isResultWrittenToW, int fileAddress)
        {
            int result = getWReg() | getFile(fileAddress);
            // Inverted Zeroflag Setting
            if (result == 0)
            {
                setZeroFlagTo(0);
            }
            else
            {
                setZeroFlagTo(1);
            }
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
            return fileAddress;
        }

        private int movF(bool isResultWrittenToW, int fileAddress)
        {
            int fileContent = getFile(fileAddress);
            setZeroFlagIfNeeded(fileContent);
            writeResultToRightDestination(fileContent, isResultWrittenToW, fileAddress);
            return fileAddress;
        }

        private int movWF(int fileAddress)
        {
            int fileContent = getWReg();
            writeResultToRightDestination(fileContent, false, fileAddress);
            return fileAddress;
        }

        private int rlF(bool isResultWrittenToW, int fileAddress)
        {
            int fileContent = getFile(fileAddress);
            int carryFlag = getFile(0x03) & 1;
            int workingBits = fileContent << 1;
            workingBits += carryFlag;
            if ((workingBits & 0x100) == 1)
            {
                carryFlag = 1;
            }
            else
            {
                carryFlag = 0;
            }
            int result = workingBits & 0xff;
            setCarryFlagTo(carryFlag);
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
            return fileAddress;
        }

        private int rrF(bool isResultWrittenToW, int fileAddress)
        {
            int fileContent = getFile(fileAddress);
            int carryFlag = getFile(0x03) & 1;
            int newCarryFlag = fileContent & 0x1;
            int workingBits = fileContent >> 1;
            int result = (workingBits + (carryFlag << 7)) & 0xff;
            setCarryFlagTo(newCarryFlag);
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
            return fileAddress;
        }

        private int subWF(bool isResultWrittenToW, int fileAddress)
        {
            int wContent = getWReg();
            int fileContent = getFile(fileAddress);
            int result = fileContent - wContent;
            int fourBitResult = (fileContent & 0xf) - (wContent & 0xf);
            setCarryFlagForSub(result);
            if (result > 0)
            {
                result += 256;
            }
            if (fourBitResult < 15)
            {
                setDigitCarryFlag();
            }
            setZeroFlagIfNeeded(result);
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
            return fileAddress;
        }

        private int swapF(bool isResultWrittenToW, int fileAddress)
        {
            int fileContent = getFile(fileAddress);
            int leftPart = (fileContent & 0xf0) >> 4;
            fileContent = fileContent << 4;
            int result = (fileContent + leftPart) & 0xff;
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
            return fileAddress;
        }

        private int xOrWF(bool isResultWrittenToW, int fileAddress)
        {
            int result = getWReg() ^ getFile(fileAddress);
            setZeroFlagIfNeeded(result);
            writeResultToRightDestination(result, isResultWrittenToW, fileAddress);
            return fileAddress;
        }
        private int bitSetFile(int fileAddress, int bitAddress)
        {
            int registerContent = GUI_Simu.memory.getFile(fileAddress);
            registerContent |= 1 << bitAddress;
            return GUI_Simu.memory.setFile(fileAddress, registerContent);
        }

        private int bitClearFile(int fileAddress, int bitAddress)
        {
            int registerContent = GUI_Simu.memory.getFile(fileAddress);
            registerContent &= ~(1 << bitAddress);
            return GUI_Simu.memory.setFile(fileAddress, registerContent);
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
            else
            {
                setZeroFlagTo(0);
            }
        }

        private void setCarryFlagTo(int value)
        {
            if (value == 0)
            {
                bitClearFile(0x03, 0);
            }
            else
            {
                bitSetFile(0x03, 0);
            }
        }

        private void setCarryFlagIfNeeded(int result)
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

        private void setCarryFlagForSub(int result)
        {
            if (result <= 255 && result >= 0)
            {
                setCarryFlagTo(1);
            }
            else
            {
                setCarryFlagTo(0);
            }
        }

        private void setCarryFlag()
        {
            setCarryFlagTo(1);
        }

        private void setDigitCarryFlagTo(int value)
        {
            if (value == 0)
            {
                bitClearFile(0x03, 0);
            }
            else
            {
                bitSetFile(0x03, 0);
            }
        }

        private void setDigitCarryFlag()
        {
            setDigitCarryFlagTo(1);
        }

        private int writeResultToRightDestination(int result, bool isResultWrittenToW, int fileAddress)
        {
            if (isResultWrittenToW)
            {
                return GUI_Simu.memory.setWReg(result);
            }
            return GUI_Simu.memory.setFile(fileAddress, result);
        }

        private int getWReg()
        {
            return GUI_Simu.memory.getWReg();
        }

        private int getFile(int fileAddress)
        {
            return GUI_Simu.memory.getFile(fileAddress);
        }
        #endregion

        public int test(int adress)
        {
            return GUI_Simu.memory.getFile(adress);
        }
    }
}
