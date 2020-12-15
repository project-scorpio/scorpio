using System;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.DynamicProxy;

namespace Scorpio.Castle.DynamicProxy
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TInterceptor"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static TService GetServiceWithInterfaceProxy<TService,TInterceptor>(this IServiceProvider serviceProvider)
            where TService:class
            where TInterceptor:IInterceptor
        {
            var interceptor=serviceProvider.GetRequiredService<AsyncDeterminationInterceptor<TInterceptor>>();
            return serviceProvider.GetService<TService>().GenerateInterfaceProxy(interceptor);
        }

         /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TInterceptor"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static TService GetServiceWithClassProxy<TService,TInterceptor>(this IServiceProvider serviceProvider)
            where TService:class
            where TInterceptor:IInterceptor
        {
            var interceptor=serviceProvider.GetRequiredService<AsyncDeterminationInterceptor<TInterceptor>>();
            return serviceProvider.GetService<TService>().GenerateClassProxy(interceptor);
        }
    }
}
