using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyvarVM.Core.Internals
{
    public class Scope
    {
        public MemoryStream RawMem { get; set; }
        public Stream InputFile { get; set; }
        public FileType FileType { get; set; }

        public Opcode CurrentOpcode { get; set; } = new Opcode();
        public int PC = 0;

        public byte al, cl, dl, bl, ah, ch, dh, bh ;
        public short ax, cx, dx, bx, sp, bp, si, di;
        public int eax = 10, ecx = 10, edx, ebx, esp, ebp, esi, edi;

        public void LoadFile(string fileName)
        {
            InputFile = File.OpenRead(fileName);

        }

        public void LoadRaw(byte[] raw)
        {
            RawMem = new MemoryStream(raw);
            InputFile = RawMem;
        }
    }
}
