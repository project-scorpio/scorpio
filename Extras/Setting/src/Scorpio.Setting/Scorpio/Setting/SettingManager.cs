using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace Scorpio.Setting
{
    internal class SettingManager : ISettingManager,DependencyInjection.ISingletonDependency
    {
        private readonly ISettingDefinitionManager _definitionManager;
        private readonly ISettingProviderManager _providerManager;

        public SettingManager(ISettingDefinitionManager definitionManager, ISettingProviderManager providerManager)
        {
            _definitionManager = definitionManager;
            _providerManager = providerManager;
        }
        public async Task<T> GetAsync<T>(string name)
        {
            var setting = _definitionManager.Get(name) as SettingDefinition<T>;
            var providers = _providerManager.Providers.Reverse();
            var value =await GetValueFromProvidersAsync(providers, setting);
            return value;
        }

        public async Task SetAsync<T>(string name, T value, string providerName="Default")
        {
            var setting = _definitionManager.Get(name) as SettingDefinition<T>;
            var providers = _providerManager.Providers.Where(p => p.Name == providerName);
            await providers.ForEachAsync(f => f.SetAsync(setting, value));
        }

        protected virtual async Task<T> GetValueFromProvidersAsync<T>(
            IEnumerable<ISettingProvider> providers,
            SettingDefinition<T> setting)
        {
            foreach (var provider in providers)
            {
                var value = await provider.GetAsync(setting);
                if (value != null)
                {
                    return value.Value;
                }
            }
            return default;
        }
    }
}
