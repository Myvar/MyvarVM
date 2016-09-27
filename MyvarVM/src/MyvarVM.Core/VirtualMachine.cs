using MyvarVM.Core.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyvarVM.Core
{
    public class VirtualMachine
    {
        public Scope Scope { get; set; } = new Scope();

        public VirtualMachine()
        {
            Opcode.Init();
        }

        public void Start(string FileName, FileType ft)
        {
            if (!File.Exists(FileName))
            {
                return;
            }

            Scope.FileType = ft;
            Scope.LoadFile(FileName);


            Step();
        }

        public void Start(byte[] raw, FileType ft)
        {           
            Scope.FileType = ft;
            Scope.LoadRaw(raw);


            Step();
        }


        public void Step()
        {
            ParseSingleOpcode();
            ExecuteCurrentOpcode();
        }

        public void ParseSingleOpcode()
        {
            Scope.CurrentOpcode = new Opcode();
            Scope.CurrentOpcode.Parse(Scope.InputFile, ref Scope.PC);
        }

        public void ExecuteCurrentOpcode()
        {
            var op = Scope.CurrentOpcode;

            var bits = new BitArray(new byte[] { op.Opcode_0 });
            var code = Convert.ToInt32((bits[7] ? "1" : "0") + (bits[6] ? "1" : "0") + (bits[5] ? "1" : "0") + (bits[4] ? "1" : "0") + (bits[3] ? "1" : "0") + (bits[2] ? "1" : "0"), 2);

            switch (code)
            {
                case 0:
                    Add(op);
                    break;
            }
        }

        public void SetRegistor(int value, string Reg, int bits)
        {
            if (bits == 0)
            {
                switch (Reg)
                {
                    case "000":
                        Scope.al = (byte)value;
                        break;
                    case "001":
                        Scope.cl = (byte)value;
                        break;
                    case "010":
                        Scope.dl = (byte)value;
                        break;
                    case "011":
                        Scope.bl = (byte)value;
                        break;
                    case "100":
                        Scope.ah = (byte)value;
                        break;
                    case "101":
                        Scope.ch = (byte)value;
                        break;
                    case "110":
                        Scope.dh = (byte)value;
                        break;
                    case "111":
                        Scope.bh = (byte)value;
                        break;
                }
            }
            else if (bits == 1)
            {
                switch (Reg)
                {
                    case "000":
                        Scope.ax = (short)value;
                        break;
                    case "001":
                        Scope.cx = (short)value;
                        break;
                    case "010":
                        Scope.dx = (short)value;
                        break;
                    case "011":
                        Scope.bx = (short)value;
                        break;
                    case "100":
                        Scope.sp = (short)value;
                        break;
                    case "101":
                        Scope.bp = (short)value;
                        break;
                    case "110":
                        Scope.si = (short)value;
                        break;
                    case "111":
                        Scope.di = (short)value;
                        break;
                }
            }
            else if (bits == 2)
            {
                switch (Reg)
                {
                    case "000":
                        Scope.eax = value;
                        break;
                    case "001":
                        Scope.ecx = value;
                        break;
                    case "010":
                        Scope.edx = value;
                        break;
                    case "011":
                        Scope.ebx = value;
                        break;
                    case "100":
                        Scope.esp = value;
                        break;
                    case "101":
                        Scope.ebp = value;
                        break;
                    case "110":
                        Scope.esi = value;
                        break;
                    case "111":
                        Scope.edi = value;
                        break;
                }
            }

        }

        public int GetRegistor(string Reg, int bits)
        {
            if (bits == 0)
            {
                switch (Reg)
                {
                    case "000":
                        return Scope.al;
                    case "001":
                        return Scope.cl;
                    case "010":
                        return Scope.dl;
                    case "011":
                        return Scope.bl;
                    case "100":
                        return Scope.ah;
                    case "101":
                        return Scope.ch;
                    case "110":
                        return Scope.dh;
                    case "111":
                        return Scope.bh;
                }
            }
            else if (bits == 1)
            {
                switch (Reg)
                {
                    case "000":
                        return Scope.ax;
                    case "001":
                        return Scope.cx;
                    case "010":
                        return Scope.dx;
                    case "011":
                        return Scope.bx;
                    case "100":
                        return Scope.sp;
                    case "101":
                        return Scope.bp;
                    case "110":
                        return Scope.si;
                    case "111":
                        return Scope.di;
                }
            }
            else if (bits == 2)
            {
                switch (Reg)
                {
                    case "000":
                        return Scope.eax;
                    case "001":
                        return Scope.ecx;
                    case "010":
                        return Scope.edx;
                    case "011":
                        return Scope.ebx;
                    case "100":
                        return Scope.esp;
                    case "101":
                        return Scope.ebp;
                    case "110":
                        return Scope.esi;
                    case "111":
                        return Scope.edi;
                }
            }
            return 0;
        }


        public void Add(Opcode op)
        {
            int bits = 0;

            if (op.S == false)//8 bit registors
            {
                bits = 0;
            }
            else// 32 bits registor
            {
                bits = 2;
            }

            if (op.D == false)//adding reg to rm
            {
                var reg = GetRegistor(op.Reg, bits);

                if (op.Mod == Mod.AddresingMode)//rm is registor
                {
                    var regval = GetRegistor(op.RM, bits);
                    SetRegistor(reg + regval, op.RM, bits);
                }

            }
            else
            {

            }
        }
    }

    public enum FileType
    {
        Bin,
        ISO
    }
}
