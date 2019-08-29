using AspectCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// 
    /// </summary>
    internal class DynamicProxyServiceProviderFactory : IServiceProviderFactory<IServiceCollection>
    {
        public IServiceCollection CreateBuilder(IServiceCollection services)
        {
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <returns></returns>
        public IServiceProvider CreateServiceProvider(IServiceCollection containerBuilder)
        {
            var serviceProvider= containerBuilder.BuildAspectInjectorProvider();
            var bootstrapper = serviceProvider.GetRequiredService<Scorpio.InternalBootstrapper>();
            bootstrapper.SetServiceProvider(serviceProvider);
            bootstrapper.Initialize();
            return serviceProvider;
        }
    }
}
