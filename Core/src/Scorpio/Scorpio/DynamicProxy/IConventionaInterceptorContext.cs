using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.DynamicProxy
{

    /// <summary>
    /// 
    /// </summary>
    public interface IConventionaInterceptorContext
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="interceptor"></param>
        void Add(Type serviceType, Type interceptor);
    }
}
