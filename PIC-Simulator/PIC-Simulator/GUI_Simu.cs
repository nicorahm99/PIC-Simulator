using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PIC_Simulator
{
    public partial class GUI_Simu : Form
    {
        #region defines & dicts
        //If there is the need to use meaningful names
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

        Dictionary<int, string> prescalerTMR0 = new Dictionary<int, string>() //prescaler with TMR0 Rate
        {
            {  0, "1 : 2"},
            {  1, "1 : 4"},
            {  2, "1 : 8"},
            {  3, "1 : 16"},
            {  4, "1 : 32"},
            {  5, "1 : 64"},
            {  6, "1 : 128"},
            {  7, "1 : 256"},
        };

        Dictionary<int, string> prescalerWDT = new Dictionary<int, string>() //prescaler with WDT Rate
        {
            {  0, "1 : 1"},
            {  1, "1 : 2"},
            {  2, "1 : 4"},
            {  3, "1 : 8"},
            {  4, "1 : 16"},
            {  5, "1 : 32"},
            {  6, "1 : 64"},
            {  7, "1 : 128"},
        };
        #endregion

        #region init

        public static readonly Parser parser = new Parser();
        public static readonly Decoder decoder = new Decoder();
        public static readonly Executer executer = new Executer();
        public static readonly ROM rom = new ROM();
        public static readonly Memory memory = new Memory();
        public static readonly EEPROM eeprom = new EEPROM();
        public static readonly Controller controller = new Controller();

        string helpMsg = "DS PIC16F84/CR84 - Simulator" + Environment.NewLine + "Dominik Lange & Nico Rahm" + Environment.NewLine + "25.04.2020" + Environment.NewLine + "Version 1.0";

        private void initialisation()
        {
            initMemory();
            refreshSFR_b();
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
            memory.setFullPC(0);
            controller.reset_taktCount();
            //reset memory
            refreshMemory();
            refreshSFR_b();
            reset_Timing();
        }

        //------------------------------------------------GUI------------------------------------------------------------------------

        #region SFR(Bit)
        private void refreshSFR_b()
        {
            lblIRPVal.Text = memory.getBit(0x3, 7).ToString();
            lblRP1Val.Text = memory.getBit(0x3, 6).ToString();
            lblRP0Val.Text = memory.getBit(0x3, 5).ToString();
            lblTOVal.Text = memory.getBit(0x3, 4).ToString();
            lblPDVal.Text = memory.getBit(0x3, 3).ToString();
            lblZVal.Text = memory.getBit(0x3, 2).ToString();
            lblDCVal.Text = memory.getBit(0x3, 1).ToString();
            lblCVal.Text = memory.getBit(0x3, 0).ToString();

            lblRPuVal.Text = memory.getBit(0x81, 7).ToString();
            lblIEgVal.Text = memory.getBit(0x81, 6).ToString();
            lblTCsVal.Text = memory.getBit(0x81, 5).ToString();
            lblTSeVal.Text = memory.getBit(0x81, 4).ToString();
            lblPSAVal.Text = memory.getBit(0x81, 3).ToString();
            lblPS2Val.Text = memory.getBit(0x81, 2).ToString();
            lblPS1Val.Text = memory.getBit(0x81, 1).ToString();
            lblPS0Val.Text = memory.getBit(0x81, 0).ToString();

            lblGIEVal.Text = memory.getBit(0xB, 7).ToString();
            lblEIEVal.Text = memory.getBit(0xB, 6).ToString();
            lblTIEVal.Text = memory.getBit(0xB, 5).ToString();
            lblIEVal.Text = memory.getBit(0xB, 4).ToString();
            lblRIEVal.Text = memory.getBit(0xB, 3).ToString();
            lblTIFVal.Text = memory.getBit(0xB, 2).ToString();
            lblIFVal.Text = memory.getBit(0xB, 1).ToString();
            lblRIFVal.Text = memory.getBit(0xB, 0).ToString();
        }
        #endregion

        #region SFR+W
        public string getPrescaler()
        {
            int optionFile = memory.getFile(0x81);
            optionFile = optionFile & 3;
            string presaler = prescalerWDT[optionFile];
            return presaler;
        }
        
        public void refreshSFRW()
        {
            lblWRegVal.Text = memory.getWReg().ToString();
            lblPCLVal.Text = memory.getFile(0x02).ToString();
            lblPCLATHVal.Text = memory.getFile(0x0A).ToString();
            lblPCLinternVal.Text = memory.getFile(0x02).ToString();
            lblStatusVal.Text = memory.getFile(0x03).ToString();
            lblFSRVal.Text = memory.getFile(0x04).ToString();

            lblOptionVal.Text = memory.getFile(0x81).ToString();
            lblVorteilerVal.Text = getPrescaler();
            //lblTimer0Val.Text = ;
        }
        #endregion

        #region Memory
        public void initMemory()
        {
            //build listView
            string firstrow = "Adr. | +00 | +01 | +02 | +03 | +04 | +05 | +06 | +07 |";
            lVMemory.Items.Add(firstrow);
            for (int i = 0; i < 32; i++)
            {
                string adr = (i * 8).ToString("X"); //convert adress integer to hex string
                if (adr.Length < 2) { adr = "0" + adr; }
                string newrow = adr + "   |";
                for (int j = 0; j < 8; j++)
                {
                    newrow += "  " + memory.getFile((i * 8) + j).ToString() + "  |";
                }
                lVMemory.Items.Add(newrow);
            }
        }
        public void refreshMemory()
        {
            //rebuild listView
            for (int i = 0; i < 32; i++)
            {
                string adr = (i * 8).ToString("X"); //convert adress integer to hex string
                if (adr.Length < 2) { adr = "0" + adr; }
                string newrow = adr + "   |";
                for (int j = 0; j < 8; j++)
                {
                    newrow += "  " + memory.getFile((i * 8) + j).ToString() + "  |";
                }
                lVMemory.Items[i + 1].Text = newrow;
            }
        }
        #endregion

        #region Timing
        public void reset_Timing()
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
            
        }
        #endregion

        #region Toolbar
        private void verlassenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void dateiÖffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var FD = new System.Windows.Forms.OpenFileDialog();
                //FD.FileName = @""; //start in direction ...
                if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    parser.setFilePath(FD.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            rom.setRom(parser.getRom());

            List<string> file = parser.getFile();
            for (int i = 0; i < file.Count; i++)
            {
                tBProgramm.AppendText(file[i] + Environment.NewLine);
            }
            refreshSFR_b();
        }

        private void tSBtnHilfe_Click(object sender, EventArgs e)
        {
            MessageBox.Show(helpMsg);
        }
        #endregion

        #region timer
        private void tWorkingInterval_Tick(object sender, EventArgs e)
        {
            controller.step();
            setLaufzeit(controller.get_taktCount());
            refreshMemory();
            refreshSFR_b();
        }
        #endregion
    }
}
