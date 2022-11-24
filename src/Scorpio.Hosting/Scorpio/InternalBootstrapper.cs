using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Scorpio
{
    internal class InternalBootstrapper : Bootstrapper
    {
        public InternalBootstrapper(Type startupModuleType, IServiceCollection services, IConfiguration configuration, Action<BootstrapperCreationOptions> optionsAction) : base(startupModuleType, services, configuration, optionsAction)
        {
        }

        internal void SetServiceProviderInternal(IServiceProvider serviceProvider) => SetServiceProvider(serviceProvider);

        internal new IServiceProvider CreateServiceProvider(IServiceCollection services) => base.CreateServiceProvider(services);
    }
}
