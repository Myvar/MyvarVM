using System.IO;

namespace MyvarVM.Core
{
    public class VMEngine
    {        
        private Config _cfg { get; set; }
        private x86CPU _Cpu { get; set;}

        private int BootLoaderOffset = 0x00007DFF;

        public VMEngine(Config cfg)
        {
            _cfg = cfg;
            _Cpu = new x86CPU(cfg);
        }

        public void LoadBin(byte[] data)
        {
            _Cpu.Memmory.LoadIntoMemmory(ref data, BootLoaderOffset);
        }

        public void LoadBin(string file)
        {
            LoadBin(File.ReadAllBytes(file));
        }

        public void Run()
        {
            _Cpu.Registers.IP = BootLoaderOffset;
        }
    }
}