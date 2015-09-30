using MyvarVM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyvarVM.Core.Internal
{
    public class RawReader
    {
        public static List<byte> OpcodeIDS = new List<byte>(){
            00,01,02,03,04,05, /* Add */
             184, 185, 186, 187, 188, 189, 190, 191, /* Mov 32 bit */
             0xB0,0xB1,0xB2,0xB3,0xB4,0xB5,0xB6,0xB7 /* Mov 8 bit */
        };
        public static List<byte> OpcodeIDS1 = new List<byte>();
        

        public List<byte> Prefexes = new List<byte>() { 0xF0, 0xF2, 0xF3, 0x2E, 0x36 , 0x3E , 0x26 , 0x64 , 0x65 , 0x2E, 0x3E , 0x66 , 0x67 };

        private byte[] _raw { get; set; }
        private int _offset { get; set; } 

        public RawReader(byte[] raw)
        {
            _raw = raw;
        }

        public Opcode ReadOpcode()
        {
            Opcode op = new Opcode();
            bool Done = false;
            bool FoundOpcode = false;
            /*
                1. Load all the prefexes
                2. Find The opcode
                3. Load all the values
            */
            while (!Done)
            {
                if (_offset < _raw.Length)
                {
                    byte b = ReadByte();

                    if (!FoundOpcode)
                    {
                        #region Prefex Switch
                        switch (b)
                        {
                            case 0XF0:
                                op.InstructionPrefex = 0XF0;
                                break;
                            case 0xF2:
                                op.InstructionPrefex = 0xF2;
                                break;
                            case 0xF3:
                                op.InstructionPrefex = 0xF3;
                                break;


                            case 0x2E:
                                op.AddessSizePrefex = 0x2E;
                                break;
                            case 0x36:
                                op.AddessSizePrefex = 0x36;
                                break;
                            case 0x3E:
                                op.AddessSizePrefex = 0x3E;
                                break;
                            case 0x26:
                                op.AddessSizePrefex = 0x26;
                                break;
                            case 0x64:
                                op.AddessSizePrefex = 0x64;
                                break;
                            case 0x65:
                                op.AddessSizePrefex = 0x65;
                                break;

                            case 0x66:
                                op.OperandSizePrefex = 0x66;
                                break;

                            case 0x67:
                                op.SegmentOveridePrefex = 0x67;
                                break;

                        }
                        #endregion

                        if (OpcodeIDS.Contains(b) || (b == 0XFF && OpcodeIDS1.Contains(_raw[_offset + 1])))
                        {
                            try
                            {
                                if (b == 0XFF && OpcodeIDS1.Contains(_raw[_offset + 1]))
                                {
                                    // ReadByte();
                                }
                                op.OpcodeID1 = _raw[_offset + 1];
                            }
                            catch { }
                            op.OpcodeID0 = b;

                            FoundOpcode = true;
                        }
                    }
                    else
                    {

                        if (!Prefexes.Contains(b))
                        {

                            if (op.Perms.Count > 0)
                            {
                                if (!OpcodeIDS.Contains(b) && op.AddessSizePrefex > op.Perms.Count)
                                {
                                    op.Perms.Add(b);
                                }
                                else
                                {
                                    _offset--;
                                    Done = true;
                                    break;
                                }
                            }
                            else
                            {
                                op.Perms.Add(b);
                            }


                        }
                        else
                        {
                            _offset--;
                            Done = true;
                            break;
                        }
                    }
                }
                else
                {
                    return op;

                }
            }
            return op;
        }

        public byte ReadByte()
        {
           
                return _raw[_offset++];
            
            return 0;
        }


    }
}
