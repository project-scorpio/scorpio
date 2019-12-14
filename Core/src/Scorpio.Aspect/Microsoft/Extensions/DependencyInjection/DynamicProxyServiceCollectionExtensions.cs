using Microsoft.Extensions.DependencyInjection;
using Scorpio.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class DynamicProxyServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="registrar"></param>
        /// <returns></returns>
        public static IServiceCollection AddConventionalRegistrar(this IServiceCollection services, IConventionaInterceptorRegistrar registrar)
        {
            InterceptorHelper.AddConventionalRegistrar(registrar);
            return services;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddConventionalRegistrar<T>(this IServiceCollection services)
            where T : IConventionaInterceptorRegistrar
        {
            return services.AddConventionalRegistrar(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        internal static IServiceCollection RegisterConventionalInterceptor(this IServiceCollection services)
        {
            InterceptorHelper.RegisterConventionalInterceptor(services);
            return services;
        }
    }
}
