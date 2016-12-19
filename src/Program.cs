using System;
using MyvarVM.Core;

namespace MyvarVM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cfg = new Config();            

            var vm = new VMEngine(cfg);
            vm.Run();
        }
    }
}
