using System;
using MyvarVM;
using MyvarVM.Core;
using Xunit;

namespace Tests
{
    public class Tests
    {
        [Fact]
        public void Add1_8bit() 
        {
            var cfg = new Config();            

            var vm = new VMEngine(cfg);
            vm.LoadBin("Asm/Add1_8bit.bin");

            vm._Cpu.Registers.cl = 10;
            vm._Cpu.Registers.al = 10;

            vm.Run();
            
            Assert.True(vm._Cpu.Registers.cl == 20);
        }
    }
}
