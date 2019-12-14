using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.DynamicProxy
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConventionalInterceptorExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TServiceType"></typeparam>
        /// <typeparam name="TInterceptor"></typeparam>
        /// <param name="context"></param>
        public static void Add<TServiceType, TInterceptor>(this IConventionaInterceptorContext context) where TInterceptor : IInterceptor
        {
            context.Add(typeof(TServiceType), typeof(TInterceptor));
        }
    }
}
