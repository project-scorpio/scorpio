using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.DynamicProxy
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConventionaInterceptorRegistrar
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void Register(IConventionaInterceptorContext context);
    }
}
