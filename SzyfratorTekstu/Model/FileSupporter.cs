using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzyfratorTekstu.Model
{
    class FileSupporter
    {
        public string getDataFromFile(string name)
        {
            return System.IO.File.ReadAllText(name);
        }

        public byte[] getDataFromFileB(string name)
        {
            return System.IO.File.ReadAllBytes(name);
        }

        public void writeToFileAsm(byte[] name)
        {
            System.IO.File.WriteAllBytes("ASMoutput.txt", name);
        }

        public void writeToFileCpp(byte[] name)
        {
            System.IO.File.WriteAllBytes("CPPoutput.txt", name);
        }
    }

}
