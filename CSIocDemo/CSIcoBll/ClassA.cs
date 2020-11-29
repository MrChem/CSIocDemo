using System;
using System.Collections.Generic;
using System.Text;

namespace CSIcoBll
{
    public class ClassA : InterfaceA
    {
        public void showA()
        {
            Console.WriteLine("调用"+typeof(ClassA).FullName);
        }
    }
}
