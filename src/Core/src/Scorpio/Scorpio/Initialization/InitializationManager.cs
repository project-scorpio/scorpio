using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Options;

using Scorpio.DependencyInjection;

namespace Scorpio.Initialization
{
    internal class InitializationManager : IInitializationManager,IServiceProviderAccessor,ISingletonDependency
    {
        private readonly InitializationOptions _options;

        public InitializationManager(IServiceProvider serviceProvider, IOptions<InitializationOptions> options)
        {
            _options = options.Value;
            ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; }

        public void Initialize()
        {
            _options.Initializables.ForEach(kv =>
            {
                kv.Value.ForEach(t =>
                {
                    if(ServiceProvider.GetService(t) is IInitializable initializable)
                    {
                        initializable.Initialize();
                    }
                });
            });
        }
    }
}
