using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PIC_Simulator
{
    public class Parser
    {
        private ROM romInstance;

        private string filePath = "C:/tmp/testfile.txt";
        private List<int> rom = new List<int>();
        private List<string> totalFile = new List<string>();

        public void init(ROM rom)
        {
            romInstance = rom;
        }

        public void setFilePath(string newPath)
        {
            filePath = newPath;
        }

        public void parse()
        {
            Dictionary<int, int> pc_line = new Dictionary<int, int>();
            Dictionary<int, int> line_pc = new Dictionary<int, int>();
            try
            {
                totalFile = File.ReadAllLines(filePath).ToList();

                foreach (string line in totalFile)
                {
                    Regex regex = new Regex(@"(^([\d|\w]{4})\s([\d|\w]{4})\s+(\d+).*$)");
                    Match match = regex.Match(line);
                    string commandCode = match.Groups[3].ToString();
                    if (commandCode != "")
                    {
                        int adress = int.Parse(match.Groups[2].ToString(), NumberStyles.HexNumber);
                        int lineNumber = int.Parse(match.Groups[4].ToString(), NumberStyles.Integer);
                        pc_line.Add(adress, lineNumber);
                        line_pc.Add(lineNumber, adress);
                        //MessageBox.Show(commandCode);
                        rom.Add(int.Parse(commandCode, System.Globalization.NumberStyles.HexNumber));
                    }
                }
                romInstance.setRom(rom);
                GUI_Simu.getDictPcToLine(pc_line);
                GUI_Simu.getDictLineToPc(line_pc);
            }
            catch (Exception ex) //invalid filepath
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        public void init()
        {
            rom = new List<int>();
            totalFile = new List<string>();
        }

        public List<string> getTotalFile()
        {
            parse();
            return totalFile;
        }

        public List<int> getRom()
        {
            parse();
            return rom;
        }
    }
}