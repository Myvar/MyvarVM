using MyvarVM.Core;
using Xunit;

namespace Tests
{
    public class Add
    {
        [Fact]
        public void Add_8_reg2reg()
        {
            var vm = new VirtualMachine();

            vm.Scope.al = 10;
            vm.Scope.cl = 10;

            vm.Start(new byte[] { 0, 0xC1 }, FileType.Bin); // add cl, al

            Assert.Equal(20, vm.Scope.cl);
        }

        [Fact]
        public void Add_32_reg2reg()
        {
            var vm = new VirtualMachine();

            vm.Scope.ecx = 10;
            vm.Scope.eax = 10;

            vm.Start(new byte[] { 0x66, 0x01, 0xC1 }, FileType.Bin); // add cl, al

            Assert.Equal(20, vm.Scope.ecx);
        }
    }
}
