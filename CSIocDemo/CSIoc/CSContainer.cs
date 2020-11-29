using CSIoc.IcoAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CSIoc
{
    public class CSContainer : ICSContainer
    {
        //存储不同类型的注册
        private Dictionary<string, Type> ContainerDictionayr = new Dictionary<string, Type>();
        public void Register<TFrom, TTo>() where TTo : TFrom
        {
            this.ContainerDictionayr.Add(typeof(TFrom).FullName, typeof(TTo));
        }

        public TFrom Resolve<TFrom>()
        {
            return (TFrom)this.ResolveObject(typeof(TFrom));

        }

        /// <summary>
        /// 用于实例化
        /// </summary>
        /// <param name="abstractType"></param>
        /// <returns></returns>
        private object ResolveObject(Type abstractType)
        {
            string key = abstractType.FullName;
            Type type = this.ContainerDictionayr[key];

            //取得构造函数
            //这里构造函数可能有多个，可以有多种解决方法
            //1.取参数最多的
            //2.通过特性标记
            //这里我们采用先判断是不是有标记，如果有标记，优先采用有标记的，如果没有取参数最多的

            //1.取有标记的
            var ctor = type.GetConstructors().FirstOrDefault(x => x.IsDefined(typeof(ConstructorAttribute), true));
            //2.没有有标记的，取参数最多的
            if (ctor == null)
            {
                ctor = type.GetConstructors().OrderByDescending(x => x.GetParameters().Length).First();
            }
            #region  构造函数实例化
            List<object> paraList = new List<object>();
            foreach (var para in ctor.GetParameters())
            {
                Type paratype = para.ParameterType;//取得参数的类型
                object paraInstance = this.ResolveObject(paratype);
                paraList.Add(paraInstance);
            }
            #endregion
            ///实例化 
            ///第二个参数为 构造函数的参数
            object oInstance = Activator.CreateInstance(type, paraList.ToArray());

            #region 属性注入
            foreach (var prop in type.GetProperties().Where(x => x.IsDefined(typeof(PropertyInjectionAttribute), true)))
            {
                Type propType = prop.PropertyType;
                object propInstance = this.ResolveObject(propType);
                prop.SetValue(oInstance, propInstance);
            }

            #endregion

            #region 方法注入
          
            foreach (var method in type.GetMethods().Where(x => x.IsDefined(typeof(MethodInjectionAttribute), true))) {
                List<object> MethodparaList = new List<object>();
                foreach (var para in method.GetParameters()) {
                    Type paratype = para.ParameterType;
                    object propInstance = this.ResolveObject(paratype);
                    MethodparaList.Add(propInstance);
                }
                method.Invoke(oInstance, MethodparaList.ToArray());
            }
            #endregion



            return oInstance;
        }
    }
}
