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
        Parser parser = new Parser();
        List<int> commands = new List<int>();
        #endregion

        public GUI_Simu()
        {
            InitializeComponent();
        }


        #region Control-Buttons
        private void btnStart_Click(object sender, EventArgs e)
        {
            commands = parser.parseFile();
            for (int i = 0; i < commands.Count; i++)
            {
                tBProgramm.AppendText(commands[1].ToString());
            }
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
