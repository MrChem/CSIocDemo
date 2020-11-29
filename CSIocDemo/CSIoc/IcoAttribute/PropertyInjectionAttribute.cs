using System;
using System.Collections.Generic;
using System.Text;

namespace CSIoc.IcoAttribute
{
    /// <summary>
    /// 只用于属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyInjectionAttribute:Attribute
    {
    }
}
