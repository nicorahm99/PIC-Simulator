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
        string testFilePath = "C:/tmp/testfile.txt";
        Parser parser = new Parser(testFilePath);
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
            //list<string> file = parser.getfile();
            //for (int i = 0; i < command.count; i++)
            //{
            //    tbprogramm.appendtext(file[i] + environment.newline);
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
        #endregion

        #region Toolbar
        private void verlassenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        private void grpBSFRW_Enter(object sender, EventArgs e)
        {
            try
            {
                var FD = new System.Windows.Forms.OpenFileDialog();
                FD.FileName = @"C\tmp";
                if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string filename = FD.FileName;
                }
            }
        }
    }
}
