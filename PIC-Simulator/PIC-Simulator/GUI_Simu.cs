using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PIC_Simulator
{
    public partial class GUI_Simu : Form
    {
        #region defines & dicts
        Dictionary<string, int> status = new Dictionary<string, int>()
            {
                {  "lblIRPVal", 0},
                {  "lblRP1Val", 1},
                {  "lblRP0Val", 2},
                {  "lblTOVal", 3},
                {  "lblPDVal", 4},
                {  "lblZVal", 5},
                {  "lblDCVal", 6},
                {  "lblCVal", 7},
            };

        Dictionary<string, int> option = new Dictionary<string, int>()
        {
            {  "lblRPuVal", 0},
            {  "lblIEgVal", 1},
            {  "lblTCsVal", 2},
            {  "lblTSeVal", 3},
            {  "lblPSAVal", 4},
            {  "lblPS2Val", 5},
            {  "lblPS1Val", 6},
            {  "lblPS0Val", 7},
        };

        Dictionary<string, int> intcon = new Dictionary<string, int>()
        {
            {  "lblGIEVal", 0},
            {  "lblEIEVal", 1},
            {  "lblTIEVal", 2},
            {  "lblIEVal", 3},
            {  "lblRIEVal", 4},
            {  "lblTIFVal", 5},
            {  "lblIFVal", 6},
            {  "lblRIFVal", 7},
        };
        #endregion

        #region init


        string helpMsg = "DS PIC16F84/CR84 - Simulator" + Environment.NewLine + "Dominik Lange & Nico Rahm" + Environment.NewLine + "25.04.2020" + Environment.NewLine + "Version 1.0";

        private void initialisation()
        {

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


        #region helpfunctions
        private void refreshSRF()
        {
            lblIRPVal.Text = Program.memory.getBit(0x3, 0).ToString();
            lblRP1Val.Text = Program.memory.getBit(0x3, 1).ToString();
            lblRP0Val.Text = Program.memory.getBit(0x3, 2).ToString();
            lblTOVal.Text = Program.memory.getBit(0x3, 3).ToString();
            lblPDVal.Text = Program.memory.getBit(0x3, 4).ToString();
            lblZVal.Text = Program.memory.getBit(0x3, 5).ToString();
            lblDCVal.Text = Program.memory.getBit(0x3, 6).ToString();
            lblCVal.Text = Program.memory.getBit(0x3, 7).ToString();

            lblRPuVal.Text = Program.memory.getBit(0x81, 0).ToString();
            lblIEgVal.Text = Program.memory.getBit(0x81, 1).ToString();
            lblTCsVal.Text = Program.memory.getBit(0x81, 2).ToString();
            lblTSeVal.Text = Program.memory.getBit(0x81, 3).ToString();
            lblPSAVal.Text = Program.memory.getBit(0x81, 4).ToString();
            lblPS2Val.Text = Program.memory.getBit(0x81, 5).ToString();
            lblPS1Val.Text = Program.memory.getBit(0x81, 6).ToString();
            lblPS0Val.Text = Program.memory.getBit(0x81, 7).ToString();

            lblGIEVal.Text = Program.memory.getBit(0xB, 0).ToString();
            lblEIEVal.Text = Program.memory.getBit(0xB, 1).ToString();
            lblTIEVal.Text = Program.memory.getBit(0xB, 2).ToString();
            lblIEVal.Text = Program.memory.getBit(0xB, 3).ToString();
            lblRIEVal.Text = Program.memory.getBit(0xB, 4).ToString();
            lblTIFVal.Text = Program.memory.getBit(0xB, 5).ToString();
            lblIFVal.Text = Program.memory.getBit(0xB, 6).ToString();
            lblRIFVal.Text = Program.memory.getBit(0xB, 7).ToString();
        }
        #endregion

        #region Control-Buttons
        private void btnStart_Click(object sender, EventArgs e)
        {
            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {

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
                    Program.parser.setFilePath(FD.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            Program.rom.setRom(Program.parser.getRom());

            List<string> file = Program.parser.getFile();
            for (int i = 0; i < file.Count; i++)
            {
                tBProgramm.AppendText(file[i] + Environment.NewLine);
            }
            refreshSRF();
        }

        private void tSBtnHilfe_Click(object sender, EventArgs e)
        {
            MessageBox.Show(helpMsg);
        }
        #endregion
    }
}
