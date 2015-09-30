using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyvarVM.Core.Objects
{
    public class Opcode
    {
        public byte InstructionPrefex { get; set; }
        public byte AddessSizePrefex { get; set; }
        public byte OperandSizePrefex { get; set; }
        public byte SegmentOveridePrefex { get; set; }

        public byte OpcodeID0 { get; set; }
        public byte OpcodeID1 { get; set; }
        public List<byte> Perms { get; set; } = new List<byte>();

        public BitArray GetBits(byte[] b)
        {
            return new BitArray(b);
        }

        public BitArray GetBits(byte b)
        {
            return GetBits(new byte[] { b });
        }

        public ModRW GetModRW(byte b)
        {
            var bits = GetBits(b);
            return new ModRW()
            {
                RW = (byte)getIntFromBitArray(new bool[] { bits[0], bits[1], bits[2] }),
                Reg = (byte)getIntFromBitArray(new bool[] { bits[3], bits[4], bits[5] }),
                Mod = (byte)getIntFromBitArray(new bool[] { bits[6], bits[7] })
            };
        }

        private int getIntFromBitArray(bool[] bitArray)
        {
            return getIntFromBitArray(new BitArray(bitArray));
        }

        private int getIntFromBitArray(BitArray bitArray)
        {

            if (bitArray.Length > 32)
                throw new ArgumentException("Argument length shall be at most 32 bits.");

            int[] array = new int[1];
            bitArray.CopyTo(array, 0);
            return array[0];

        }
    }

    public class ModRW
    {
        public byte Mod { get; set; }
        public byte Reg { get; set; }
        public byte RW { get; set; }
    }



}
