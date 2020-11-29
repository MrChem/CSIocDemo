using CSIoc.IcoAttribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSIcoBll
{
    public class ClassB : InterfaceB
    {
        [PropertyInjectionAttribute]
        public InterfaceA ProperA { get; set; }

        public ClassB(InterfaceA interfaceA) {
            interfaceA.showA();
        }
        public void show()
        {
            Console.WriteLine("调用" + typeof(ClassB).FullName);
        }

        [MethodInjection]
        public void show(InterfaceA interfaceA) {
            Console.WriteLine("我要调用属性注入");
            ProperA.showA();
            Console.WriteLine("我是函数注入");
            interfaceA.showA();
        }
    }
}
