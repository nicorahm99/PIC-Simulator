using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIC_Simulator
{
    public class Parser
    {

        private string filePath = "C:/tmp/testfile.txt";
        private List<int> rom = new List<int>();
        private List<string> file = new List<string>();

        public Parser(string filePath)
        {
            this.filePath = filePath;
            
            file = File.ReadAllLines(this.filePath).ToList();

            foreach (string line in file)
            {
                Regex regex = new Regex(@"(^([\d|\w]{4})\s([\d|\w]{4})\s+(\d+).*$)");
                Match match = regex.Match(line);
                string commandCode = match.Groups[3].ToString();
                if (commandCode != "")
                {
                    //MessageBox.Show(commandCode);
                    rom.Add(int.Parse(commandCode, System.Globalization.NumberStyles.HexNumber));
                }
            }
        }

        public List<string> getFile()
        {
            return this.file;
        }

        public List<int> getRom()
        {
            return this.rom;
        }
    }
}