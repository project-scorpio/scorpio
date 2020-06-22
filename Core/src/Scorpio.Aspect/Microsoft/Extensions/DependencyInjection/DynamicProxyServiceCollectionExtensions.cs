using Microsoft.Extensions.DependencyInjection;
using Scorpio.Conventional;
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
        /// <param name="types"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterConventionalInterceptor(
            this IServiceCollection services,
            IEnumerable<Type> types,
            Action<IConventionalConfiguration<ConventionalInterceptorAction>> configureAction)
        {
            return services.DoConventionalAction(types, configureAction);
        }

    }
}
