using CSIcoBll;
using CSIoc;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

namespace CSIcoConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ICSContainer container = new CSContainer();
            container.Register<InterfaceA, ClassA>();
            container.Register<InterfaceB, ClassB>();


            InterfaceB itfb = container.Resolve<InterfaceB>();
            itfb.show();

        }
    }
}
