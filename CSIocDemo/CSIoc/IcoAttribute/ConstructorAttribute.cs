using System;
using System.Collections.Generic;
using System.Text;

namespace CSIoc.IcoAttribute
{
    [AttributeUsage(AttributeTargets.Constructor)]//只能在构造函数使用
   public class ConstructorAttribute:Attribute
    {
    }
}
