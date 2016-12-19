namespace MyvarVM.Core
{
    public class x86CPU
    {
        private Config _cfg { get; set; }
        public Registers Registers { get; set; }
        public CPUState State { get; set; }
        public Memmory Memmory { get; set; }

        public x86CPU(Config cfg)
        {
            _cfg = cfg;
            Registers = new Registers();
            State = new CPUState();
            Memmory = new Memmory(_cfg);
        }

        public void ParseOpcode()
        {

        }

        private byte ReadIPByte()
        {
            return Memmory.Ram[Registers.IP++];
        }

        public void StepOneOpCode()
        {
            
        }
    }
}