using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyvarVM.Core.Internals
{
    public class Scope
    {
        public Stream InputFile { get; set; }
        public FileType FileType { get; set; }

        public Opcode CurrentOpcode { get; set; } = new Opcode();
        public int PC = 0;


        public void LoadFile(string fileName)
        {
            InputFile = File.OpenRead(fileName);

        }
    }
}
