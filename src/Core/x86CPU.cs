using MyvarVM.Core.Opcodes;

namespace MyvarVM.Core
{
    public class x86CPU
    {
        private Config _cfg { get; set; }
        public Registers Registers { get; set; }
        public CPUState State { get; set; }
        public Memmory Memmory { get; set; }
        private Opcode _opcode { get; set; }

        public x86CPU(Config cfg)
        {
            _cfg = cfg;
            Registers = new Registers();
            State = new CPUState();
            Memmory = new Memmory(_cfg);
            _opcode = new Opcode();
        }

        public void ParseOpcode()
        {
            _opcode = new Opcode();
            while(_opcode.Parse(ReadIPByte(), ReadIPByte));
        }

        public void ExecuteOpCode()
        {
            var me = this;
            aOpcode.Executeop(_opcode.POpcode == 0xFF
             ? _opcode.SOpcode : _opcode.POpcode, _opcode, ref me);
        }

        private byte ReadIPByte()
        {
            return Memmory.Ram[Registers.IP++];
        }

        public void StepOneOpCode()
        {
            ParseOpcode();
            ExecuteOpCode();
        }
    }
}