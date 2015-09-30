using MyvarVM.Core.Objects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyvarVM.Core
{
    public static class Globals
    {
        public static object AL, CL, DL, BL, AH, CH, DH, BH;
        public static object AX, CX, DX, BX, SP, BP, SI, DI;
        public static object EAX, ECX, EDX, EBX, ESP, EBP, ESI, EDI;
        public static object DS, ES, FS, GS, SS, CS, IP;

        public static List<string> ErrorStack = new List<string>();

    }
}
