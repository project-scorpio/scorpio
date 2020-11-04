using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Scorpio
{
    class InternalBootstrapper : Bootstrapper
    {
        public InternalBootstrapper(Type startupModuleType, IServiceCollection services, IConfiguration configuration, Action<BootstrapperCreationOptions> optionsAction) : base(startupModuleType, services, configuration, optionsAction)
        {
            
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Configuration.SafelyDispose();
            ServiceProvider.SafelyDispose();
        }

    }
}
