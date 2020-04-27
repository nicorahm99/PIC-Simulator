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
        #region init
        Parser parser = new Parser();
        Decoder decoder = new Decoder();
        Executer executer = new Executer();
        ROM rom = new ROM();
        Memory memory = new Memory();

        string helpMsg = "PIC Simulator" + Environment.NewLine + "Dominik Lange & Nico Rahm" + Environment.NewLine + "25.04.2020" + Environment.NewLine + "Version 1.0";

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
        }

        private void tSBtnHilfe_Click(object sender, EventArgs e)
        {
            MessageBox.Show(helpMsg);
        }
        #endregion
    }
}
