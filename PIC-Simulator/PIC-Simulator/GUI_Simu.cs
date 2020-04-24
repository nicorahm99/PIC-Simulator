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
        #region variables
        Parser parser = new Parser("C:/tmp/testfile.txt");
        #endregion

        public GUI_Simu()
        {
            InitializeComponent();
        }


        #region Control-Buttons
        private void btnStart_Click(object sender, EventArgs e)
        {
            List<int> command = parser.getRom();
            for (int i = 0; i < command.Count; i++)
            {
                tBProgramm.AppendText(command[i].ToString() + Environment.NewLine);
            }
            //List<string> file = parser.getFile();
            //for (int i = 0; i < command.Count; i++)
            //{
            //    tBProgramm.AppendText(file[i] + Environment.NewLine);
            //}
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion
    }
}
