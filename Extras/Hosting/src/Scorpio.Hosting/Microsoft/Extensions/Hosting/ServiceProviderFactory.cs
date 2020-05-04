using Microsoft.Extensions.DependencyInjection;
using Scorpio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// 
    /// </summary>
    internal class ServiceProviderFactory : IServiceProviderFactory<IServiceCollection>
    {
        private readonly HostBuilderContext _context;
        private readonly Type _startupModuleType;
        private readonly Action<BootstrapperCreationOptions> _optionsAction;
        private InternalBootstrapper _bootstrapper;
        public ServiceProviderFactory(HostBuilderContext context, Type startupModuleType, Action<BootstrapperCreationOptions> optionsAction)
        {

            _context = context;
            _startupModuleType = startupModuleType;
            _optionsAction = optionsAction;
        }
        public IServiceCollection CreateBuilder(IServiceCollection services)
        {
            _bootstrapper = new InternalBootstrapper(_startupModuleType, services, _context.Configuration, _optionsAction);
            _bootstrapper.Properties["HostingEnvironment"] = _context.HostingEnvironment;
            services.AddSingleton(_bootstrapper);
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <returns></returns>
        public IServiceProvider CreateServiceProvider(IServiceCollection containerBuilder)
        {
            var serviceProvider = _bootstrapper.CreateServiceProvider(containerBuilder);
            _bootstrapper.SetServiceProviderInternal(serviceProvider);
            var applicationLifetime = serviceProvider.GetRequiredService<IHostApplicationLifetime>();
            applicationLifetime.ApplicationStopping.Register(() => _bootstrapper.Shutdown());
            applicationLifetime.ApplicationStopped.Register(() => (_bootstrapper as IDisposable).Dispose());
            _bootstrapper.Initialize();
            return serviceProvider;
        }
    }
}
