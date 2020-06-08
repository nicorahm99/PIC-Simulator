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

        public static readonly Parser parser = new Parser();
        public static readonly Decoder decoder = new Decoder();
        public static readonly Executer executer = new Executer();
        public static readonly ROM rom = new ROM();
        public static readonly Memory memory = new Memory();
        public static readonly EEPROM eeprom = new EEPROM();
        public static readonly Controller controller = new Controller();
        public static readonly InterruptController interruptController = new InterruptController();
        public static readonly Prescaler prescaler = new Prescaler();
        public static Dictionary<int, int> pcToLine = new Dictionary<int, int>();


        string helpMsg = "DS PIC16F84/CR84 - Simulator" + Environment.NewLine + "Dominik Lange & Nico Rahm" + Environment.NewLine + "25.04.2020" + Environment.NewLine + "Version 1.0";

        private void initialisation()
        {
            initMemory();
            refreshSFR_b();
            refreshSFRW();
        }
        #endregion

        #region windows
        public GUI_Simu()
        {
            InitializeComponent();
        }

        private void GUI_Simu_load(object sender, EventArgs e)
        {
            initialisation();
        }

        private void GUI_Simu_close(object sender, FormClosingEventArgs e)
        {

        }
        #endregion

        public void reset()
        {
            memory.init();
            controller.init();
            refreshSFRW();
            refreshMemory();
            refreshSFR_b();
            resetTiming();
        }

        public void init()
        {
            memory.init();
            controller.init();
            rom.init();
            parser.init();
            refreshSFRW();
            refreshMemory();
            refreshSFR_b();
            resetTiming();
        }

        public void refresh()
        {
            refreshMemory();
            refreshSFRW();
            refreshSFR_b();
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
                int currentMemoryBank = GUI_Simu.memory.getCurrentMemoryBank();
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
                int currentMemoryBank = GUI_Simu.memory.getCurrentMemoryBank();
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
                int currentMemoryBank = GUI_Simu.memory.getCurrentMemoryBank();
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
                int currentMemoryBank = GUI_Simu.memory.getCurrentMemoryBank();
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
                int currentMemoryBank = GUI_Simu.memory.getCurrentMemoryBank();
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
                int currentMemoryBank = GUI_Simu.memory.getCurrentMemoryBank();
                memory.setMemoryBankTo(1);
                fileAddress -= 0x80;
                access = memory.requestAccess(fileAddress, bit);
                memory.setMemoryBankTo(currentMemoryBank);
            }
            return access;
        }

        #endregion

        #region I/O Ports
        public void refreshIO()
        {

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
            lblWRegVal.Text = memory.getWReg().ToString();
            lblPCLVal.Text = memAdrRes_getFile(0x02).ToString();
            lblPCLATHVal.Text = memAdrRes_getFile(0x0A).ToString();
            lblPCLinternVal.Text = memAdrRes_getFile(0x02).ToString();
            lblStatusVal.Text = memAdrRes_getFile(0x03).ToString();
            lblFSRVal.Text = memAdrRes_getFile(0x04).ToString();

            lblOptionVal.Text = memAdrRes_getFile(0x81).ToString();
            lblVorteilerVal.Text = "1 : " + prescaler.getPrescaler().ToString();
            lblTimer0Val.Text = memory.getTMR0().ToString();
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
                    string val = memAdrRes_getFile((i * 8) + j).ToString();
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
                    string val = memAdrRes_getFile((i * 8) + j).ToString();
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
            tWorkingInterval.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            tWorkingInterval.Enabled = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            tWorkingInterval.Enabled = false;
            reset();
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            controllerStep();
        }
        #endregion

        #region Program
        public void showFile(List<string> file)
        {
            for (int i = 0; i < file.Count; i++)
            {
                tBProgramm.AppendText(file[i] + Environment.NewLine);
            }
        }

        public void clearFile()
        {
            tBProgramm.Clear();
        }

        public void selectLine()
        {
            int pc = memory.getFullPC(); // Program Counter
            int line = pcToLine[pc];
        }

        public static void assignPCToLine(int pc, int line)
        {
            pcToLine.Add(pc, line);
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
            clearFile();
            showFile(parser.getFile());
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
        #endregion

        #region controller
        public void controllerStep()
        {
            controller.step();
            setLaufzeit(memory.getTMR0());
            refreshMemory();
            refreshSFR_b();
            refreshSFRW();
        }
        #endregion


    }
}
