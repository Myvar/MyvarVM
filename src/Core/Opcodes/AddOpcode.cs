using System;
using System.Collections;
using MyvarVM.Core.Opcodes;

namespace MyvarVM.Core.Opcodes
{
    public class AddOpcode : aOpcode
    {
        public override void Execute(int id, Opcode opcode, ref x86CPU cpu)
        {
            
        }

        public override bool ID(int id) => id >= 0 && id <= 6;

        public override void Parse(ref Opcode opcode, Func<byte> read)
        {

            opcode.ModRM = read();
            switch(opcode.POpcode)
            {
                case 0:
                break;

                default:
                break;
            }
        }
    }
}