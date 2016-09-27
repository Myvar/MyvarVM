using MyvarVM.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyvarVM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("MyvarVM Starting ...");

            var vm = new VirtualMachine();
            vm.Start(args[0], FileType.Bin);

        }
    }
}
