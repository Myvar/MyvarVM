using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyvarVM.Core.Internals
{
    public enum Mod
    {
        InderectMode,
        OneByte,
        FourByte,
        AddresingMode
    }

    public enum Scale
    {
        Index1,
        Index2,
        Index4,
        Index8
    }

    public class Opcode
    {

        public static ParseTables PT { get; set; } = new ParseTables();

        public static void Init()
        {
            PT = JsonConvert.DeserializeObject<ParseTables>(File.ReadAllText("parse_tables.json"));
        }

        public bool LockPrefex { get; set; }
        public bool RepnePrefex { get; set; }
        public bool RepPrefex { get; set; }

        public bool CS_SegOveridePrefex { get; set; }
        public bool SS_SegOveridePrefex { get; set; }
        public bool DS_SegOveridePrefex { get; set; }
        public bool ES_SegOveridePrefex { get; set; }
        public bool FS_SegOveridePrefex { get; set; }
        public bool GS_SegOveridePrefex { get; set; }
        public bool BranchNotTakenPrefex { get; set; }
        public bool BranchTakenPrefex { get; set; }

        public bool OperandSizeOveridePrefex { get; set; }
        public bool AddressSizeOveridePrefex { get; set; }

        public byte Opcode_0 { get; set; }
        public byte Opcode_1 { get; set; }

        public bool D { get; set; }
        public bool S { get; set; }

        public byte ModRW { get; set; }

        public Mod Mod { get; set; }
        public string Reg { get; set; }
        public string RM { get; set; }

        public byte SIB { get; set; }
        public Scale SIBScale { get; set; }
        public string SIBIndex { get; set; }
        public string SIBBase { get; set; }


        public byte[] Displacement { get; set; }
        public byte[] Immediate { get; set; }


        public void Parse(Stream inp, ref int offset)
        {
            ReadPrefexes(inp, ref offset);
            ReadOpcode(inp, ref offset);
            ReadModRW(inp, ref offset);
            ReadSib(inp, ref offset);
            ReadDisplacement(inp, ref offset);
       
        }

        public void ReadPrefexes(Stream imp, ref int offset)
        {
            for (int i = 0; i < 4; i++)
            {
                var b = new byte[1];
                imp.Position = offset;
                imp.Read(b, 0, 1);

                if (!ValidatePrefex(b[0], ref offset))
                {
                    break;
                }
            }
        }

        public bool ValidatePrefex(byte prefex, ref int Offset)
        {
            switch (prefex)
            {
                //prefex group 1
                case 0xF0:
                    LockPrefex = true;
                    Offset++;
                    return true;
                case 0xF2:
                    RepnePrefex = true;
                    Offset++;
                    return true;
                case 0xF3:
                    RepPrefex = true;
                    Offset++;
                    return true;
                //prefex group 2
                case 0x2E:
                    CS_SegOveridePrefex = true;
                    BranchNotTakenPrefex = true;
                    Offset++;
                    return true;
                case 0x36:
                    SS_SegOveridePrefex = true;
                    Offset++;
                    return true;
                case 0x3E:
                    DS_SegOveridePrefex = true;
                    BranchTakenPrefex = true;
                    Offset++;
                    return true;
                case 0x26:
                    ES_SegOveridePrefex = true;
                    Offset++;
                    return true;
                case 0x64:
                    FS_SegOveridePrefex = true;
                    Offset++;
                    return true;
                case 0x65:
                    GS_SegOveridePrefex = true;
                    Offset++;
                    return true;
                //prefex group 3  
                case 0x66:
                    OperandSizeOveridePrefex = true;
                    Offset++;
                    return true;
                //prefex group 4    
                case 0x67:
                    AddressSizeOveridePrefex = true;
                    Offset++;
                    return true;
            }

            return false;
        }

        public void ReadOpcode(Stream imp, ref int offset)
        {
            var b = new byte[1];
            imp.Position = offset;
            imp.Read(b, 0, 1);
            if (b[0] == 0xF)
            {
                offset++;
                imp.Position = offset;
                imp.Read(b, 0, 1);
                Opcode_1 = b[0];

                var bits = new BitArray(new byte[] { b[0] });
                S = bits[0];
                D = bits[1];
            }
            else
            {
                Opcode_0 = b[0];
                var bits = new BitArray(new byte[] { b[0] });
                S = bits[0];
                D = bits[1];
            }

            offset++;
        }

        public void ReadModRW(Stream imp, ref int offset)
        {
            if(PT.HasModRW.Contains(Opcode_0))
            {
                var b = new byte[1];
                imp.Position = offset;
                imp.Read(b, 0, 1);

                ModRW = b[0];
                var bits = new BitArray(new byte[] { ModRW });

                string mod = (bits[7] ? "1" : "0") + (bits[6] ? "1" : "0");
                
                switch(mod)
                {
                    case "00":
                        Mod = Mod.InderectMode;
                        break;
                    case "01":
                        Mod = Mod.OneByte;
                        break;
                    case "10":
                        Mod = Mod.FourByte;
                        break;
                    case "11":
                        Mod = Mod.AddresingMode;
                        break;
                }

                string reg = (bits[5] ? "1" : "0") + (bits[4] ? "1" : "0") + (bits[3] ? "1" : "0");
                Reg = reg;

                string rm = (bits[2] ? "1" : "0") + (bits[1] ? "1" : "0") + (bits[0] ? "1" : "0");
                RM = rm;

                offset++;
            }
        }

        public void ReadSib(Stream imp, ref int offset)
        {
            if (ModRW != 0)
            {
                var b = new byte[1];
                imp.Position = offset;
                imp.Read(b, 0, 1);

                SIB = b[0];
                var bits = new BitArray(new byte[] { ModRW });

                string scale = (bits[7] ? "1" : "0") + (bits[6] ? "1" : "0");

                switch (scale)
                {
                    case "00":
                        SIBScale = Scale.Index1;
                        break;
                    case "01":
                        SIBScale = Scale.Index2;
                        break;
                    case "10":
                        SIBScale = Scale.Index4;
                        break;
                    case "11":
                        SIBScale = Scale.Index8;
                        break;
                }

                string reg = (bits[5] ? "1" : "0") + (bits[4] ? "1" : "0") + (bits[3] ? "1" : "0");
                SIBIndex = reg;

                string rm = (bits[2] ? "1" : "0") + (bits[1] ? "1" : "0") + (bits[0] ? "1" : "0");
                SIBBase = rm;

                offset++;
            }
        }

        public void ReadDisplacement(Stream imp, ref int offset)
        {
            if(OperandSizeOveridePrefex)
            {
                Displacement = new byte[4];
                imp.Position = offset;
                imp.Read(Displacement, 0, 4);
            }
        }
    }
}
