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

        public void setFilePath(string newPath)
        {
            filePath = newPath;
        }

        public void parse()
        {
            try
            {
                file = File.ReadAllLines(filePath).ToList();

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
                GUI_Simu.rom.setRom(rom);
            }
            catch (Exception ex) //invalid filepath
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        public void init()
        {
            rom = new List<int>();
            file = new List<string>();
        }

        public List<string> getFile()
        {
            parse();
            return file;
        }

        public List<int> getRom()
        {
            parse();
            return rom;
        }
    }
}