using System;
using System.Collections;

namespace MyvarVM.Core
{
    public class Registers
    {
        /// <summary>
        /// Instruction pointer (Programm counter)
        /// </summary>
        public int IP { get; set; }

        /// <summary>
        /// 8 Bit Registers
        /// </summary>
        public byte al, cl, dl, bl, ah, ch, dh, bh;


        public void Set8BitREG(byte RegRM, byte value)
        {
            var raw = new BitArray(new byte[] { RegRM });
            var regbits = new BitArray(8);
            regbits.SetAll(false);
            regbits[0] = raw[5];
            regbits[1] = raw[4];
            regbits[3] = raw[3];
            var reg = ConvertToByte(regbits);
            switch (reg)
            {
                case 0:
                    al = value;
                    break;
                case 1:
                    cl = value;
                    break;
                case 2:
                    dl = value;
                    break;
                case 3:
                    bl = value;
                    break;
                case 4:
                    ah = value;
                    break;
                case 5:
                    ch = value;
                    break;
                case 6:
                    dh = value;
                    break;
                case 7:
                    bh = value;
                    break;
            }

        }

        public void Set8BitRM(byte RegRM, byte value)
        {
            var raw = new BitArray(new byte[] { RegRM });
            var regbits = new BitArray(8);
            regbits.SetAll(false);
            regbits[0] = raw[0];
            regbits[1] = raw[1];
            regbits[3] = raw[2];
            var reg = ConvertToByte(regbits);

            switch (reg)
            {
                case 0:
                    al = value;
                    break;
                case 1:
                    cl = value;
                    break;
                case 2:
                    dl = value;
                    break;
                case 3:
                    bl = value;
                    break;
                case 4:
                    ah = value;
                    break;
                case 5:
                    ch = value;
                    break;
                case 6:
                    dh = value;
                    break;
                case 7:
                    bh = value;
                    break;
            }

        }

        public byte Get8BitREG(byte RegRM)
        {
            var raw = new BitArray(new byte[] { RegRM });
            var regbits = new BitArray(8);
            regbits.SetAll(false);
            regbits[0] = raw[5];
            regbits[1] = raw[4];
            regbits[3] = raw[3];
            var reg = ConvertToByte(regbits);
            switch (reg)
            {
                case 0:
                    return al;

                case 1:
                    return cl;
                case 2:
                    return dl;
                case 3:
                    return bl;
                case 4:
                    return ah;
                case 5:
                    return ch;
                case 6:
                    return dh;
                case 7:
                    return bh;
            }

            return 0;
        }

        public byte Get8BitRM(byte RegRM)
        {
            var raw = new BitArray(new byte[] { RegRM });
            var regbits = new BitArray(8);
            regbits.SetAll(false);
            regbits[0] = raw[0];
            regbits[1] = raw[1];
            regbits[3] = raw[2];
            var reg = ConvertToByte(regbits);

            switch (reg)
            {
                case 0:
                    return al;

                case 1:
                    return cl;
                case 2:
                    return dl;
                case 3:
                    return bl;
                case 4:
                    return ah;
                case 5:
                    return ch;
                case 6:
                    return dh;
                case 7:
                    return bh;
            }

            return 0;

        }


        private byte ConvertToByte(BitArray bits)
        {
            //i went with this terible route becuse .net core has no copyto
            byte b = 0;
            if (bits.Get(0)) b++;
            if (bits.Get(1)) b += 2;
            if (bits.Get(2)) b += 4;
            return b;

        }
    }
}