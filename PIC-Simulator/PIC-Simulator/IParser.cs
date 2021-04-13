using System.Collections.Generic;

namespace PIC_Simulator
{
    public interface IParser
    {
        List<int> getRom();
        List<string> getTotalFile();
        void init();
        void init(ROM rom);
        void parse();
        void setFilePath(string newPath);
    }
}