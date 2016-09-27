using MyvarVM.Core.Internals;
using System;
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
            if(!File.Exists(FileName))
            {
                return;
            }

            Scope.FileType = ft;
            Scope.LoadFile(FileName);


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

        }
    }

    public enum FileType
    {
        Bin,
        ISO
    }
}
