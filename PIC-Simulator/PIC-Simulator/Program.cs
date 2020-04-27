using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIC_Simulator
{
    public class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI_Simu());
        }
        public Parser parser = new Parser();
        public Decoder decoder = new Decoder();
        public Executer executer = new Executer();
        public ROM rom = new ROM();
        public static readonly Memory memory = new Memory();
    }
}
