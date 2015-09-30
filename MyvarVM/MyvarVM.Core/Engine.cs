using MyvarVM.Core.Internal;
using MyvarVM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyvarVM.Core
{
    public class Engine
    {
        public List<Opcode> Opcodes = new List<Opcode>();

        private RawReader _rawreader { get; set; }


        public Engine()
        {
            for (int i = 0; i < 8; i++)
            {
                SetRegistor32((Registor32)i, 0);
                SetRegistor8((Registor8)i, 0);
            }
        }
        public void Run(byte[] BootDevice)
        {
            Opcodes.Clear();
            bool FoundAll = false;
            _rawreader = new RawReader(BootDevice);
            while (!FoundAll)
            {
                var op = _rawreader.ReadOpcode();
                if (op?.Perms.Count != 0 && op != null)
                {
                    Opcodes.Add(op);
                }
                else
                {
                    FoundAll = true;
                    break;
                }
            }
            ExecuteOpcodes();
        }

        public void ExecuteOpcodes()
        {
            foreach (var i in Opcodes)
            {
                if (new List<byte> { 00, 01, 02, 03, 04, 05 }.Contains(i.OpcodeID0))//Add opcode
                {
                    var bitid = i.GetBits(i.OpcodeID0);
                    bool s = bitid[0];
                    bool d = bitid[1];
                    var modrw = i.GetModRW(i.Perms[0]);
                    if (d == false)
                    {
                        if (s == true)
                        {
                            if (modrw.Mod == 3)
                            {
                                var a = GetRegistor32((Registor32)modrw.RW);
                                var b = GetRegistor32((Registor32)modrw.Reg);
                                SetRegistor32((Registor32)modrw.RW, (int)a + (int)b);
                            }
                        }
                        else
                        {
                            if (modrw.Mod == 3)
                            {
                                var a = GetRegistor8((Registor8)modrw.RW);
                                var b = GetRegistor8((Registor8)modrw.Reg);
                                SetRegistor8((Registor8)modrw.RW, (int)a + (int)b);
                            }
                        }
                    }
                    else
                    {

                    }
                }
                if (new List<byte> { 184, 185, 186, 187, 188, 189, 190, 191 }.Contains(i.OpcodeID0))//Mov opcode 32 bit
                {
                    var modrw = i.GetModRW(i.OpcodeID0);
                    SetRegistor32((Registor32)modrw.RW, BitConverter.ToInt32(i.Perms.ToArray(), 0));
                }
                if (new List<byte> { 0xB0, 0xB1, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xB7  }.Contains(i.OpcodeID0))//Mov opcode 8 bit
                {
                    //176
                    SetRegistor8((Registor8)(i.OpcodeID0  - 176), i.Perms[0]);
                }
            }
        }

        public void SetRegistor32(Registor32 r, object Value)
        {
            switch (r)
            {
                case Registor32.Eax:
                    Globals.EAX = Value;
                    break;
                case Registor32.Ecx:
                    Globals.ECX = Value;
                    break;
                case Registor32.Edx:
                    Globals.EDX = Value;
                    break;
                case Registor32.Ebx:
                    Globals.EBX = Value;
                    break;
                case Registor32.Esp:
                    Globals.ESP = Value;
                    break;
                case Registor32.Ebp:
                    Globals.EBP = Value;
                    break;
                case Registor32.Esi:
                    Globals.ESI = Value;
                    break;
                case Registor32.Edi:
                    Globals.EDI = Value;
                    break;
            }
        }

        public object GetRegistor32(Registor32 r)
        {
            switch (r)
            {
                case Registor32.Eax:
                    return Globals.EAX;

                case Registor32.Ecx:
                    return Globals.ECX;

                case Registor32.Edx:
                    return Globals.EDX;

                case Registor32.Ebx:
                    return Globals.EBX;

                case Registor32.Esp:
                    return Globals.ESP;

                case Registor32.Ebp:
                    return Globals.EBP;

                case Registor32.Esi:
                    return Globals.ESI;

                case Registor32.Edi:
                    return Globals.EDI;

            }
            return null;
        }

        public void SetRegistor8(Registor8 r, object Value)
        {
            switch (r)
            {
                case Registor8.Al:
                    Globals.AL = Value;
                    break;
                case Registor8.Cl:
                    Globals.CL = Value;
                    break;
                case Registor8.Dl:
                    Globals.DL = Value;
                    break;
                case Registor8.Bl:
                    Globals.BL = Value;
                    break;
                case Registor8.Ah:
                    Globals.AH = Value;
                    break;
                case Registor8.Ch:
                    Globals.CH = Value;
                    break;
                case Registor8.Dh:
                    Globals.DH = Value;
                    break;
                case Registor8.Bh:
                    Globals.BH = Value;
                    break;
            }
        }

        public object GetRegistor8(Registor8 r)
        {
            switch (r)
            {
                case Registor8.Al:
                   return Globals.AL;
                  
                case Registor8.Cl:
                    return Globals.CL;
                
                case Registor8.Dl:
                    return Globals.DL;
                  
                case Registor8.Bl:
                    return Globals.BL;
                  
                case Registor8.Ah:
                    return Globals.AH;
                  
                case Registor8.Ch:
                    return Globals.CH;
                  
                case Registor8.Dh:
                    return Globals.DH;
                    
                case Registor8.Bh:
                    return Globals.BH;
                    

            }
            return null;
        }
    }

    public enum Registor32
    {
        Eax = 0,
        Ecx = 1,
        Edx = 2,
        Ebx = 3,
        Esp = 4,
        Ebp = 5,
        Esi = 6,
        Edi = 7
    }

    public enum Registor8
    {
        Al = 0,
        Cl = 1,
        Dl = 2,
        Bl = 3,
        Ah = 4,
        Ch = 5,
        Dh = 6,
        Bh = 7
    }
}
