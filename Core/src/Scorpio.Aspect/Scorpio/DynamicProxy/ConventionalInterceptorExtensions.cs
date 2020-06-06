using AspectCore.DynamicProxy;
using Scorpio.Conventional;
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
        /// <typeparam name="TInterceptor"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IConventionalContext<ConventionalInterceptorAction> Intercept<TInterceptor>(this IConventionalContext<ConventionalInterceptorAction> context)
            where TInterceptor : IInterceptor
        {
            var typeList= context.GetOrAdd(ConventionalInterceptorAction.Interceptors, n => new TypeList<IInterceptor>());
            typeList.Add<TInterceptor>();
            return context;
        }
    }
}
