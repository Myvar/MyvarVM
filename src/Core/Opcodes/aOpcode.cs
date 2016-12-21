using System;
using System.Collections.Generic;
using MyvarVM.Core;
using MyvarVM.Core.Opcodes;

namespace MyvarVM.Core.Opcodes
{
    public abstract class aOpcode
    {
        public static List<aOpcode> Opcodes { get; set; } = new List<aOpcode>(){
            new AddOpcode()           
        };

        public static void Parseop(int id, ref Opcode op, Func<byte> read)
        {
            foreach(var i in Opcodes)
            {
                if(i.ID(id))
                {
                    i.Parse(ref op, read);
                    return;
                }
            }
        }

        public static void Executeop(int id, Opcode op, ref x86CPU cpu)
        {
            foreach(var i in Opcodes)
            {
                if(i.ID(id))
                {
                    i.Execute(id, op, ref cpu);
                    return;
                }
            }
        }

        public abstract bool ID(int id);
        public abstract void Parse(ref Opcode opcode, Func<byte> read);

        public abstract void Execute(int id, Opcode opcode, ref x86CPU cpu);
    }
}