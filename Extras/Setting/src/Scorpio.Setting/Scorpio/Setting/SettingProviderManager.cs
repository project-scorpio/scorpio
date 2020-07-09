using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Options;

namespace Scorpio.Setting
{
    internal class SettingProviderManager : ISettingProviderManager, DependencyInjection.ISingletonDependency
    {
        private readonly Lazy<List<ISettingProvider>> _providers;
        private readonly IServiceProvider _serviceProvider;
        private readonly SettingOptions _options;

        public ICollection<ISettingProvider> Providers => _providers.Value;

        public SettingProviderManager(IServiceProvider serviceProvider,
            IOptions<SettingOptions> options)
        {
            _serviceProvider = serviceProvider;
            _options = options.Value;
            _providers = new Lazy<List<ISettingProvider>>(() =>
                _options.SettingProviders.Select(t => _serviceProvider.GetService(t) as ISettingProvider).ToList(), true);
        }
    }
}
