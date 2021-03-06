using System;
using MyvarVM.Core.Opcodes;

namespace MyvarVM.Core
{
    public class Opcode
    {
        //prefex group 1
        public bool Prefex_Lock { get; set; }
        public bool Prefex_REPNE_REPNZ { get; set; }
        public bool Prefex_REP_REPE_REPZ { get; set; }

        //prefex group 2
        public bool Prefex_CS_SegmentOveride { get; set; }
        public bool Prefex_SS_SegmentOveride { get; set; }
        public bool Prefex_DS_SegmentOveride { get; set; }
        public bool Prefex_ES_SegmentOveride { get; set; }
        public bool Prefex_FS_SegmentOveride { get; set; }
        public bool Prefex_GS_SegmentOveride { get; set; }
        public bool Prefex_BranchNotTaken { get; set; }
        public bool Prefex_BranchTaken { get; set; }

        //prefex group 3
        public bool Prefex_OperandSizeOveride { get; set; }

        //prefex group 4
        public bool Prefex_AddressSizeOveride { get; set; }


        public byte POpcode { get; set; }
        public byte SOpcode { get; set; }

        public byte ModRM { get; set; }

        int ParseState;
        public bool Parse(byte b, Func<byte> read)
        {
            switch (ParseState)
            {
                case 0:
                    //prefex
                    if (!ParsePrefex(b))
                    {
                        ParseState = 1;
                        Parse(b, read);
                    }
                    return true;
                case 1:
                //opcode it self
                if(POpcode == 0xFF)
                {
                    SOpcode = b;
                    ParseState = 2;
                    var me = this;
                    aOpcode.Parseop(POpcode == 0xFF ? SOpcode : POpcode ,ref me, read);
                }

                if(b == 0xFF)
                {
                    POpcode = b;                    
                }
                else
                {
                    POpcode = b;
                    ParseState = 2;
                    var me = this;
                    aOpcode.Parseop(POpcode == 0xFF ? SOpcode : POpcode ,ref me, read);
                }
                return false;

            }
            return false;
        }

        private bool ParsePrefex(byte b)
        {
            switch (b)
            {
                //prefex group 1
                case 0xF0:
                    Prefex_Lock = true;
                    return true;
                case 0xF2:
                    Prefex_REPNE_REPNZ = true;
                    return true;
                case 0xF3:
                    Prefex_REP_REPE_REPZ = true;
                    return true;
                //prefex group 2
                case 0x2E:
                    Prefex_CS_SegmentOveride = true;
                    Prefex_BranchNotTaken = true;
                    return true;
                case 0x36:
                    Prefex_SS_SegmentOveride = true;
                    return true;
                case 0x3E:
                    Prefex_DS_SegmentOveride = true;
                    Prefex_BranchTaken = true;
                    return true;
                case 0x26:
                    Prefex_ES_SegmentOveride = true;
                    return true;
                case 0x64:
                    Prefex_FS_SegmentOveride = true;
                    return true;
                case 0x65:
                    Prefex_GS_SegmentOveride = true;
                    return true;
                //prefex group 3                
                case 0x66:
                    Prefex_OperandSizeOveride = true;
                    return true;
                //prefex group 4
                case 0x67:
                    Prefex_AddressSizeOveride = true;
                    return true;
            }

            return false;
        }
    }
}