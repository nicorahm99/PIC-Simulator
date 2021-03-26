using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PIC_Simulator
{
    public partial class GUI_Simu : Form
    {
        #region defines & dicts
        //possibility to use meaningful names
        //Dictionary<string, int> status = new Dictionary<string, int>() //bits in status reg
        //{
        //    {  "lblIRPVal", 0},
        //    {  "lblRP1Val", 1},
        //    {  "lblRP0Val", 2},
        //    {  "lblTOVal", 3},
        //    {  "lblPDVal", 4},
        //    {  "lblZVal", 5},
        //    {  "lblDCVal", 6},
        //    {  "lblCVal", 7},
        //};

        //Dictionary<string, int> option = new Dictionary<string, int>() //bits in option reg
        //{
        //    {  "lblRPuVal", 0},
        //    {  "lblIEgVal", 1},
        //    {  "lblTCsVal", 2},
        //    {  "lblTSeVal", 3},
        //    {  "lblPSAVal", 4},
        //    {  "lblPS2Val", 5},
        //    {  "lblPS1Val", 6},
        //    {  "lblPS0Val", 7},
        //};

        //Dictionary<string, int> intcon = new Dictionary<string, int>() //bits in intcon reg
        //{
        //    {  "lblGIEVal", 0},
        //    {  "lblEIEVal", 1},
        //    {  "lblTIEVal", 2},
        //    {  "lblIEVal", 3},
        //    {  "lblRIEVal", 4},
        //    {  "lblTIFVal", 5},
        //    {  "lblIFVal", 6},
        //    {  "lblRIFVal", 7},
        //};

        #endregion

        #region init

        public static Dictionary<int, int> pcToLine = new Dictionary<int, int>();
        public static Dictionary<int, int> lineToPc = new Dictionary<int, int>();


        private Stack stack = new Stack();
        private Decoder decoder = new Decoder();
        private ROM rom = new ROM();

        private Prescaler prescaler = new Prescaler();
        private Memory memory = new Memory();
        private EEPROM EEPROM = new EEPROM();
        private InterruptController interruptController = new InterruptController();
        private Executer executer = new Executer();
        private Controller controller = new Controller();
        private Parser parser = new Parser();


        string helpMsg = "DS PIC16F84/CR84 - Simulator" + Environment.NewLine + "Dominik Lange & Nico Rahm" + Environment.NewLine + "15.06.2020" + Environment.NewLine + "Version 1.0";

        #endregion

        #region windows
        public GUI_Simu()
        {
            InitializeComponent();
        }

        private void GUI_Simu_load(object sender, EventArgs e)
        {
            initComponents();
            initMemory();
            refreshSFR_b();
            refreshSFRW();
            refreshStack();
        }

        private void GUI_Simu_close(object sender, FormClosingEventArgs e)
        {

        }
        #endregion

        public void reset()
        {
            memory.reset();
            controller.reset();
            stack.init();
            refreshSFRW();
            refreshMemory();
            refreshSFR_b();
            resetTiming();
        }

        private void initComponents()
        {
            memory.init(controller, interruptController, EEPROM);
            EEPROM.init(memory);
            executer.init(memory, stack);
            interruptController.init(memory, executer);
            prescaler.init(memory);
            controller.init(rom, interruptController, memory, decoder, executer, prescaler);
            parser.init(rom);
            decoder.init(memory, controller, stack);
        }
        public void init()
        {
            memory.reset();
            controller.reset();
            rom.init();
            parser.init();
            stack.init();
            refreshSFRW();
            refreshMemory();
            refreshSFR_b();
            resetTiming();
            refreshIO();
        }

        public void refresh()
        {
            refreshMemory();
            refreshSFRW();
            refreshSFR_b();
            refreshIO();
        }

        //------------------------------------------------GUI------------------------------------------------------------------------
        #region memory adress resolution functions

        public int memAdrRes_setFile(int fileAddress, int value)
        {
            if (fileAddress >= 0 && fileAddress <= 0x4F)
            {
                int tmp =  memory.setFile(fileAddress, value);
            }
            else if (fileAddress == 0x83)
            {
                memory.setFile(0x3, value);
            }
            else if (fileAddress >= 0x80 && fileAddress <= 0xCF)
            {
                int currentMemoryBank = memory.getCurrentMemoryBank();
                memory.setMemoryBankTo(1);
                fileAddress -= 0x80;
                int tmp =  memory.setFile(fileAddress, value);
                memory.setMemoryBankTo(currentMemoryBank);
            }
            return 0;
        }

        public int memAdrRes_getFile(int fileAddress)
        {
            int file = 0;
            if (fileAddress >= 0 && fileAddress <= 0x4F)
            {
                file = memory.getFile(fileAddress);
            }
            else if (fileAddress == 0x83)
            {
                file = memory.getFile(0x3);
            }
            else if (fileAddress >= 0x80 && fileAddress <= 0xCF)
            {
                int currentMemoryBank = memory.getCurrentMemoryBank();
                memory.setMemoryBankTo(1);
                fileAddress -= 0x80;
                file = memory.getFile(fileAddress);
                memory.setMemoryBankTo(currentMemoryBank);
            }
            return file;
        }

        public int memAdrRes_getBit(int fileAddress, int bitAddress)
        {
            int bit = 0;
            if (fileAddress >= 0 && fileAddress <= 0x4F)
            {
                bit = memory.getBit(fileAddress, bitAddress);
            }
            else if (fileAddress == 0x83)
            {
                bit = memory.getBit(0x3, bitAddress);
            }
            else if (fileAddress >= 0x80 && fileAddress <= 0xCF)
            {
                int currentMemoryBank = memory.getCurrentMemoryBank();
                memory.setMemoryBankTo(1);
                fileAddress -= 0x80;
                bit = memory.getBit(fileAddress, bitAddress);
                memory.setMemoryBankTo(currentMemoryBank);
            }
            return bit;
        }

        public void memAdrRes_setBit(int fileAddress, int bitAddress)
        {
            if (fileAddress >= 0 && fileAddress <= 0x4F)
            {
                memory.setBit(fileAddress, bitAddress);
            }
            else if (fileAddress == 0x83)
            {
                memory.setBit(0x3, bitAddress);
            }
            else if (fileAddress >= 0x80 && fileAddress <= 0xCF)
            {
                int currentMemoryBank = memory.getCurrentMemoryBank();
                memory.setMemoryBankTo(1);
                fileAddress -= 0x80;
                memory.setBit(fileAddress, bitAddress);
                memory.setMemoryBankTo(currentMemoryBank);
            }
        }

        public void memAdrRes_clearBit(int fileAddress, int bit)
        {
            if (fileAddress >= 0 && fileAddress <= 0x4F)
            {
                memory.clearBit(fileAddress, bit);
            }
            else if (fileAddress >= 0x80 && fileAddress <= 0xCF)
            {
                int currentMemoryBank = memory.getCurrentMemoryBank();
                memory.setMemoryBankTo(1);
                fileAddress -= 0x80;
                memory.clearBit(fileAddress, bit);
                memory.setMemoryBankTo(currentMemoryBank);
            }
        }

        public bool memAdrRes_requestAccess(int fileAddress, int bit)
        {
            bool access = false;
            if (fileAddress >= 0 && fileAddress <= 0x4F)
            {
                access = memory.requestAccess(fileAddress, bit);
            }
            else if (fileAddress >= 0x80 && fileAddress <= 0xCF)
            {
                int currentMemoryBank = memory.getCurrentMemoryBank();
                memory.setMemoryBankTo(1);
                fileAddress -= 0x80;
                access = memory.requestAccess(fileAddress, bit);
                memory.setMemoryBankTo(currentMemoryBank);
            }
            return access;
        }

        #endregion

        #region Stack
        public void refreshStack()
        {
            int[] tmpStackArray = new int[8];
            List<int> tmpStackList = stack.get();
            for (int i = 0; i<tmpStackList.Count; i++)
            {
                tmpStackArray[i] = tmpStackList[i];
            }
            lblStck0.Text = tmpStackArray[0].ToString();
            lblStck1.Text = tmpStackArray[1].ToString();
            lblStck2.Text = tmpStackArray[2].ToString();
            lblStck3.Text = tmpStackArray[3].ToString();
            lblStck4.Text = tmpStackArray[4].ToString();
            lblStck5.Text = tmpStackArray[5].ToString();
            lblStck6.Text = tmpStackArray[6].ToString();
            lblStck7.Text = tmpStackArray[7].ToString();
        }
        #endregion

        #region I/O Ports
        public void refreshIO()
        {
            bool[] tmpPortA = new bool[5];
            bool[] tmpPortB = new bool[8];
            bool[] tmpTrisA = new bool[5];
            bool[] tmpTrisB = new bool[8];
            int regPortA = memory.getFile(0x05);
            int regPortB = memory.getFile(0x06);
            int currentMemBank = memory.getCurrentMemoryBank();
            memory.setMemoryBankTo(1);
            int regTrisA = memory.getFile(0x05);
            int regTrisB = memory.getFile(0x06);
            memory.setMemoryBankTo(currentMemBank);

            for (int i = 0; i<5; i++)
            {
                if ((regPortA & 1 << i) == 0) { tmpPortA[i] = false; } else { tmpPortA[i] = true; }
            }
            for (int i = 0; i < 8; i++)
            {
                if ((regPortB & 1 << i) == 0) { tmpPortB[i] = false; } else { tmpPortB[i] = true; }
            }
            for (int i = 0; i < 5; i++)
            {
                if ((regTrisA & 1 << i) == 0) { tmpTrisA[i] = false; } else { tmpTrisA[i] = true; }
            }
            for (int i = 0; i < 8; i++)
            {
                if ((regTrisB & 1 << i) == 0) { tmpTrisB[i] = false; } else { tmpTrisB[i] = true; }
            }

            chckBPortAPin0.Checked = tmpPortA[0];
            chckBPortAPin1.Checked = tmpPortA[1];
            chckBPortAPin2.Checked = tmpPortA[2];
            chckBPortAPin3.Checked = tmpPortA[3];
            chckBPortAPin4.Checked = tmpPortA[4];

            chckBPortBPin0.Checked = tmpPortB[0];
            chckBPortBPin1.Checked = tmpPortB[1];
            chckBPortBPin2.Checked = tmpPortB[2];
            chckBPortBPin3.Checked = tmpPortB[3];
            chckBPortBPin4.Checked = tmpPortB[4];
            chckBPortBPin5.Checked = tmpPortB[5];
            chckBPortBPin6.Checked = tmpPortB[6];
            chckBPortBPin7.Checked = tmpPortB[7];

            chckBPortATris0.Checked = tmpTrisA[0];
            chckBPortATris1.Checked = tmpTrisA[1];
            chckBPortATris2.Checked = tmpTrisA[2];
            chckBPortATris3.Checked = tmpTrisA[3];
            chckBPortATris4.Checked = tmpTrisA[4];

            chckBPortBTris0.Checked = tmpTrisB[0];
            chckBPortBTris1.Checked = tmpTrisB[1];
            chckBPortBTris2.Checked = tmpTrisB[2];
            chckBPortBTris3.Checked = tmpTrisB[3];
            chckBPortBTris4.Checked = tmpTrisB[4];
            chckBPortBTris5.Checked = tmpTrisB[5];
            chckBPortBTris6.Checked = tmpTrisB[6];
            chckBPortBTris7.Checked = tmpTrisB[7];

            //if (memory.getBit(0x05, 0) == 1) { chckBPortAPin0.Checked = true; } else { chckBPortAPin0.Checked = false; }
            //if (memory.getBit(0x05, 1) == 1) { chckBPortAPin1.Checked = true; } else { chckBPortAPin1.Checked = false; }
            //if (memory.getBit(0x05, 2) == 1) { chckBPortAPin2.Checked = true; } else { chckBPortAPin2.Checked = false; }
            //if (memory.getBit(0x05, 3) == 1) { chckBPortAPin3.Checked = true; } else { chckBPortAPin3.Checked = false; }
            //if (memory.getBit(0x05, 4) == 1) { chckBPortAPin4.Checked = true; } else { chckBPortAPin4.Checked = false; }

            //if (memory.getBit(0x06, 0) == 1) { chckBPortBPin0.Checked = true; } else { chckBPortBPin0.Checked = false; }
            //if (memory.getBit(0x06, 1) == 1) { chckBPortBPin1.Checked = true; } else { chckBPortBPin1.Checked = false; }
            //if (memory.getBit(0x06, 2) == 1) { chckBPortBPin2.Checked = true; } else { chckBPortBPin2.Checked = false; }
            //if (memory.getBit(0x06, 3) == 1) { chckBPortBPin3.Checked = true; } else { chckBPortBPin3.Checked = false; }
            //if (memory.getBit(0x06, 4) == 1) { chckBPortBPin4.Checked = true; } else { chckBPortBPin4.Checked = false; }
            //if (memory.getBit(0x06, 5) == 1) { chckBPortBPin5.Checked = true; } else { chckBPortBPin5.Checked = false; }
            //if (memory.getBit(0x06, 6) == 1) { chckBPortBPin6.Checked = true; } else { chckBPortBPin6.Checked = false; }
            //if (memory.getBit(0x06, 7) == 1) { chckBPortBPin7.Checked = true; } else { chckBPortBPin7.Checked = false; }

            //int currentMemBank = memory.getCurrentMemoryBank();
            //memory.setMemoryBankTo(1);
            //if (memory.getBit(0x05, 0) == 1) { chckBPortATris0.Checked = true; } else { chckBPortATris0.Checked = false; }
            //if (memory.getBit(0x05, 1) == 1) { chckBPortATris1.Checked = true; } else { chckBPortATris1.Checked = false; }
            //if (memory.getBit(0x05, 2) == 1) { chckBPortATris2.Checked = true; } else { chckBPortATris2.Checked = false; }
            //if (memory.getBit(0x05, 3) == 1) { chckBPortATris3.Checked = true; } else { chckBPortATris3.Checked = false; }
            //if (memory.getBit(0x05, 4) == 1) { chckBPortATris4.Checked = true; } else { chckBPortATris4.Checked = false; }

            //if (memory.getBit(0x06, 0) == 1) { chckBPortBTris0.Checked = true; } else { chckBPortBTris0.Checked = false; }
            //if (memory.getBit(0x06, 1) == 1) { chckBPortBTris1.Checked = true; } else { chckBPortBTris1.Checked = false; }
            //if (memory.getBit(0x06, 2) == 1) { chckBPortBTris2.Checked = true; } else { chckBPortBTris2.Checked = false; }
            //if (memory.getBit(0x06, 3) == 1) { chckBPortBTris3.Checked = true; } else { chckBPortBTris3.Checked = false; }
            //if (memory.getBit(0x06, 4) == 1) { chckBPortBTris4.Checked = true; } else { chckBPortBTris4.Checked = false; }
            //if (memory.getBit(0x06, 5) == 1) { chckBPortBTris5.Checked = true; } else { chckBPortBTris5.Checked = false; }
            //if (memory.getBit(0x06, 6) == 1) { chckBPortBTris6.Checked = true; } else { chckBPortBTris6.Checked = false; }
            //if (memory.getBit(0x06, 7) == 1) { chckBPortBTris7.Checked = true; } else { chckBPortBTris7.Checked = false; }
            //memory.setMemoryBankTo(currentMemBank);
        }

        #region Port A
        private void PortAPin0(object sender, EventArgs e)
        {
            // if checkbox is checked corresponding tris bitAddress is set
            if ( chckBPortAPin0.Checked == true && memAdrRes_requestAccess(0x85, 0)) { memory.setBit(0x05, 0); }
            else if( chckBPortAPin0.Checked == false && memAdrRes_requestAccess(0x85, 0)) { memory.clearBit(0x05, 0); }
            refreshMemory();
        }

        private void PortAPin1(object sender, EventArgs e)
        {
            if ( chckBPortAPin1.Checked == true && memAdrRes_requestAccess(0x85, 1)) { memory.setBit(0x05, 1); }
            else if ( chckBPortAPin1.Checked == false && memAdrRes_requestAccess(0x85, 1)) { memory.clearBit(0x05, 1); }
            refreshMemory();
        }

        private void PortAPin2(object sender, EventArgs e)
        {
            if ( chckBPortAPin2.Checked == true && memAdrRes_requestAccess(0x85, 2)) { memory.setBit(0x05, 2); }
            else if ( chckBPortAPin2.Checked == false && memAdrRes_requestAccess(0x85, 2)) { memory.clearBit(0x05, 2); }
            refreshMemory();
        }

        private void PortAPin3(object sender, EventArgs e)
        {
            if ( chckBPortAPin3.Checked == true && memAdrRes_requestAccess(0x85, 3)) { memory.setBit(0x05, 3); }
            else if ( chckBPortAPin3.Checked == false && memAdrRes_requestAccess(0x85, 3)) { memory.clearBit(0x05, 3); }
            refreshMemory();
        }

        private void PortAPin4(object sender, EventArgs e)
        {
            if ( chckBPortAPin4.Checked == true && memAdrRes_requestAccess(0x85, 4)) { memory.setBit(0x05, 4); }
            else if ( chckBPortAPin4.Checked == false && memAdrRes_requestAccess(0x85, 4)) { memory.clearBit(0x05, 4); }
            refreshMemory();
        }
        #endregion

        #region Port B
        private void PortBPin0(object sender, EventArgs e)
        {
            if ( chckBPortBPin0.Checked == true && memAdrRes_requestAccess(0x86, 0)) { memory.setBit(0x06, 0); }
            else if ( chckBPortBPin0.Checked == false && memAdrRes_requestAccess(0x86, 0)) { memory.clearBit(0x06, 0); }
            refreshMemory();
        }

        private void PortBPin1(object sender, EventArgs e)
        {
            if ( chckBPortBPin1.Checked == true && memAdrRes_requestAccess(0x86, 1)) { memory.setBit(0x06, 1); }
            else if ( chckBPortBPin1.Checked == false && memAdrRes_requestAccess(0x86, 1)) { memory.clearBit(0x06, 1); }
            refreshMemory();
        }

        private void PortBPin2(object sender, EventArgs e)
        {
            if ( chckBPortBPin2.Checked == true && memAdrRes_requestAccess(0x86, 2)) { memory.setBit(0x06, 2); }
            else if ( chckBPortBPin2.Checked == false && memAdrRes_requestAccess(0x86, 2)) { memory.clearBit(0x06, 2); }
            refreshMemory();
        }

        private void PortBPin3(object sender, EventArgs e)
        {
            if ( chckBPortBPin3.Checked == true && memAdrRes_requestAccess(0x86, 3)) { memory.setBit(0x06, 3); }
            else if ( chckBPortBPin3.Checked == false && memAdrRes_requestAccess(0x86, 3)) { memory.clearBit(0x06, 3); }
            refreshMemory();
        }

        private void PortBPin4(object sender, EventArgs e)
        {
            if ( chckBPortBPin4.Checked == true && memAdrRes_requestAccess(0x86, 4)) { memory.setBit(0x06, 4); }
            else if ( chckBPortBPin4.Checked == false && memAdrRes_requestAccess(0x86, 4)) { memory.clearBit(0x06, 4); }
            refreshMemory();
        }

        private void PortBPin5(object sender, EventArgs e)
        {
            if ( chckBPortBPin5.Checked == true && memAdrRes_requestAccess(0x86, 5)) { memory.setBit(0x06, 5); }
            else if ( chckBPortBPin5.Checked == false && memAdrRes_requestAccess(0x86, 5)) { memory.clearBit(0x06, 5); }
            refreshMemory();
        }

        private void PortBPin6(object sender, EventArgs e)
        {
            if ( chckBPortBPin6.Checked == true && memAdrRes_requestAccess(0x86, 6)) { memory.setBit(0x06, 6); }
            else if ( chckBPortBPin6.Checked == false && memAdrRes_requestAccess(0x86, 6)) { memory.clearBit(0x06, 6); }
            refreshMemory();
        }

        private void PortBPin7(object sender, EventArgs e)
        {
            if ( chckBPortBPin7.Checked == true && memAdrRes_requestAccess(0x86, 7)) { memory.setBit(0x06, 7); }
            else if ( chckBPortBPin7.Checked == false && memAdrRes_requestAccess(0x86, 7)) { memory.clearBit(0x06, 7); }
            refreshMemory();
        }
        #endregion

        #region Tris A

        #endregion

        #region Tris B
        #endregion

        #endregion

        #region SFR(Bit)
        private void refreshSFR_b()
        {
            lblIRPVal.Text = memAdrRes_getBit(0x3, 7).ToString();
            lblRP1Val.Text = memAdrRes_getBit(0x3, 6).ToString();
            lblRP0Val.Text = memAdrRes_getBit(0x3, 5).ToString();
            lblTOVal.Text = memAdrRes_getBit(0x3, 4).ToString();
            lblPDVal.Text = memAdrRes_getBit(0x3, 3).ToString();
            lblZVal.Text = memAdrRes_getBit(0x3, 2).ToString();
            lblDCVal.Text = memAdrRes_getBit(0x3, 1).ToString();
            lblCVal.Text = memAdrRes_getBit(0x3, 0).ToString();

            lblRPuVal.Text = memAdrRes_getBit(0x81, 7).ToString();
            lblIEgVal.Text = memAdrRes_getBit(0x81, 6).ToString();
            lblTCsVal.Text = memAdrRes_getBit(0x81, 5).ToString();
            lblTSeVal.Text = memAdrRes_getBit(0x81, 4).ToString();
            lblPSAVal.Text = memAdrRes_getBit(0x81, 3).ToString();
            lblPS2Val.Text = memAdrRes_getBit(0x81, 2).ToString();
            lblPS1Val.Text = memAdrRes_getBit(0x81, 1).ToString();
            lblPS0Val.Text = memAdrRes_getBit(0x81, 0).ToString();

            lblGIEVal.Text = memAdrRes_getBit(0xB, 7).ToString();
            lblEIEVal.Text = memAdrRes_getBit(0xB, 6).ToString();
            lblTIEVal.Text = memAdrRes_getBit(0xB, 5).ToString();
            lblIEVal.Text = memAdrRes_getBit(0xB, 4).ToString();
            lblRIEVal.Text = memAdrRes_getBit(0xB, 3).ToString();
            lblTIFVal.Text = memAdrRes_getBit(0xB, 2).ToString();
            lblIFVal.Text = memAdrRes_getBit(0xB, 1).ToString();
            lblRIFVal.Text = memAdrRes_getBit(0xB, 0).ToString();
        }
        #endregion

        #region SFR+W

        public void refreshSFRW()
        {
            lblWRegVal.Text = memory.getWReg().ToString("X");
            lblPCLVal.Text = memAdrRes_getFile(0x02).ToString("X");
            lblPCLATHVal.Text = memAdrRes_getFile(0x0A).ToString("X");
            lblPCLinternVal.Text = memAdrRes_getFile(0x02).ToString("X");
            lblStatusVal.Text = memAdrRes_getFile(0x03).ToString("X");
            lblFSRVal.Text = memAdrRes_getFile(0x04).ToString("X");

            lblOptionVal.Text = memAdrRes_getFile(0x81).ToString("X");
            lblVorteilerVal.Text = "1 : " + prescaler.getPrescaler().ToString();
            lblTimer0Val.Text = memory.getTMR0().ToString("X");
        }
        #endregion

        #region Memory
        public void initMemory()
        {
            //build listView
            string firstrow = "Adr. || +00 | +01 | +02 | +03 | +04 | +05 | +06 | +07 |";
            lVMemory.Items.Add(firstrow);
            for (int i = 0; i < 32; i++)
            {
                string adr = (i * 8).ToString("X"); //convert adress integer to hex string
                if (adr.Length < 2) { adr = "0" + adr; }
                string newrow = adr + "   ||";
                for (int j = 0; j < 8; j++)
                {
                    string val = memAdrRes_getFile((i * 8) + j).ToString("X");
                    if (val.Length == 1) { newrow += "   " + val + " |"; }
                    else if (val.Length == 2) { newrow += "  " + val + " |"; }
                    else if (val.Length == 3) { newrow += " " + val + " |"; }
                }
                lVMemory.Items.Add(newrow);
            }
        }
        public void refreshMemory()
        {
            int currentMemoryBank = memory.getCurrentMemoryBank();
            memory.setMemoryBankTo(0);
            //rebuild listView
            for (int i = 0; i < 32; i++)
            {
                string adr = (i * 8).ToString("X"); //convert adress integer to hex string
                if (adr.Length < 2) { adr = "0" + adr; }
                string newrow = adr + "   ||";
                for (int j = 0; j < 8; j++)
                {
                    string val = memAdrRes_getFile((i * 8) + j).ToString("X");
                    if (val.Length == 1) { newrow += "   " + val + " |"; }
                    else if (val.Length == 2) { newrow += "  " + val + " |"; }
                    else if (val.Length == 3) { newrow += " " + val + " |"; }

                }
                lVMemory.Items[i + 1].Text = newrow; // replace old orw by new row (first row should not be touched)
            }
            memory.setMemoryBankTo(currentMemoryBank);
        }
        #endregion

        #region Timing
        public void resetTiming()
        {
            lblLaufztVal.Text = "0";
            //Watchdog
        }

        public void setLaufzeit(int val)
        {
            string ms = (val / Convert.ToInt16(cmbBQuarz.Text)).ToString();
            lblLaufztVal.Text = ms;
        }
        #endregion

        #region Control-Buttons

        public void enableButtons()
        {
            btnReset.Enabled = true;
            btnStart.Enabled = true;
            btnStep.Enabled = true;
            btnStop.Enabled = true;
        }

        public void disableButtons()
        {
            btnReset.Enabled = false;
            btnStart.Enabled = false;
            btnStep.Enabled = false;
            btnStop.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            startTimer();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            stopTimer();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            stopTimer();
            reset();
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            controllerStep();
        }
        #endregion

        #region Program Listiew + Breakpoints
        private void itemChecked(object sender, ItemCheckedEventArgs e)
        {
            setBreakPoint();
        }

        public static void getDictPcToLine(Dictionary<int, int> newdict)
        {
            // get dit from Parser class
            pcToLine = newdict;
        }

        public static void getDictLineToPc(Dictionary<int, int> newdict)
        {
            // get dit from Parser class
            lineToPc = newdict;
        }

        public void showFile(List<string> file)
        {
            //clear list
            lVProgram.Clear();
            //define new Column header
            ColumnHeader header = new ColumnHeader();
            header.Text = "B; Program";
            header.Name = "header";
            header.Width = 800;
            //add header to listview
            lVProgram.Columns.Add(header);
            //add all lines from file to listview
            for (int i = 0; i < file.Count; i++)
            {
                lVProgram.Items.Add(file[i]);
            }
        }

        public void selectLine()
        {
            //unselect all items
            for (int i = 0; i<lVProgram.SelectedItems.Count; i++)
            {
                lVProgram.SelectedItems[i].Selected = false;
            }
            int pc = memory.getFullPC(); // Program Counter
            int line = pcToLine[pc] -1; // corresponding (to pc) line of file
            lVProgram.Items[line].Selected = true; // select the current line
        }

       public void setBreakPoint()
       {
            List<int> breakPoints = new List<int>(); // list contains pcs of all breakpoints
            if (lVProgram.CheckedItems.Count > 0)
            {
                // items are checked
                for (int i = 0; i < lVProgram.Items.Count; i++)
                {
                    // go through all listview items
                    if (lVProgram.Items[i].Checked == true && lineToPc.ContainsKey(i+1))// (i+1) --> linenumbers are 1-based
                    {
                        // checked & part of the programm code
                        breakPoints.Add(lineToPc[i+1]);// (i+1) --> linenumbers are 1-based
                    }
                    else if (lVProgram.Items[i].Checked == true && lineToPc.ContainsKey(i+1) == false)// (i+1) --> linenumbers are 1-based
                    {
                        // checked but not part of the programm code
                        lVProgram.Items[i].Checked = false; // uncheck
                    }
                }
            }
            controller.setBreakPoints(breakPoints);
       }
        #endregion

        #region Toolbar
        private void verlassenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void dateiÖffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tWorkingInterval.Enabled = false;
            init();
            try
            {
                var FD = new System.Windows.Forms.OpenFileDialog();
                //FD.FileName = @""; //start in direction ...
                if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    parser.setFilePath(FD.FileName);
                }
                enableButtons();
                FD.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //set rom
            rom.setRom(parser.getRom());
            //view file in textbox
            showFile(parser.getTotalFile());
            selectLine();
        }

        private void tSBtnHilfe_Click(object sender, EventArgs e)
        {
            MessageBox.Show(helpMsg);
        }
        #endregion

        #region timer
        private void tWorkingInterval_Tick(object sender, EventArgs e)
        {
            controllerStep();
        }

        public void stopTimer()
        {
            tWorkingInterval.Enabled = false;
        }

        public void startTimer()
        {
            tWorkingInterval.Enabled = true;
        }
        #endregion

        #region controller
        public void controllerStep()
        {
            bool isBreakPoint = controller.step();
            if (isBreakPoint == false)
            {
                selectLine();
                setLaufzeit(memory.getTMR0());
                refreshMemory();
                refreshSFR_b();
                refreshSFRW();
                refreshStack();
                refreshIO();
            }
            else
            {
                stopTimer();
            }
        }

        #endregion
    }
}
