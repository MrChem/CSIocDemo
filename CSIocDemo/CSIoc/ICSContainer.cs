using System;
using System.Collections.Generic;
using System.Text;

namespace CSIoc
{
    public interface ICSContainer
    {
        /// <summary>
        /// 注册
        /// 
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        void Register<TFrom, TTo>() where TTo : TFrom;
        TFrom Resolve<TFrom>();
    }
}
