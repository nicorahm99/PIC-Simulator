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

        public List<int> parseFile()
        {
            List<int> rom = new List<int>();
            List<string> lines = File.ReadAllLines(this.filePath).ToList();

            foreach (string line in lines)
            {
                Regex regex = new Regex(@"(^([\d|\w]{4})\s([\d|\w]{4})\s+(\d+).*$)");
                Match match = regex.Match(line);
                string commandCode = match.Groups[3].ToString();
                if (commandCode != "")
                {
                    rom.Add(int.Parse(commandCode, System.Globalization.NumberStyles.HexNumber));
                }
            }
            return rom;
        }
    }
}