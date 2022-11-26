using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Scorpio.Conventional;
using Scorpio.DynamicProxy;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
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
            Action<IConventionalConfiguration<ConventionalInterceptorAction>> configureAction) => services.DoConventionalAction(types, configureAction);

    }
}
