using System;
using System.Collections;
using MyvarVM.Core.Opcodes;

namespace MyvarVM.Core.Opcodes
{
    public class AddOpcode : aOpcode
    {
        public override void Execute(int id, Opcode opcode, ref x86CPU cpu)
        {
            var bits = ParseBits((byte)id);

            var s = bits[0];
            var d = bits[1];

            var ModRM = ParseBits(opcode.ModRM);
            //NB: remember msb and lsb
            if(s)
            {
                //32 bit
                if(d)
                {
                    //REG << REG + R/M
                    
                }
                else
                {
                    //R/M << R/M + REG
                    
                }
            }
            else
            {
                //8bit
                if(d)
                {
                    //REG << REG + R/M

                    byte RM = cpu.Registers.Get8BitRM(opcode.ModRM);
                    byte REG = cpu.Registers.Get8BitREG(opcode.ModRM);

                    cpu.Registers.Set8BitREG(opcode.ModRM, (byte)(RM + REG));
                }
                else
                {
                    //R/M << R/M + REG
                    byte RM = cpu.Registers.Get8BitRM(opcode.ModRM);
                    byte REG = cpu.Registers.Get8BitREG(opcode.ModRM);

                    cpu.Registers.Set8BitRM(opcode.ModRM, (byte)(RM + REG));
                }
            }

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