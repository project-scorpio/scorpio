using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Scorpio.Setting
{
    internal class SettingDefinitionManager : ISettingDefinitionManager, DependencyInjection.ISingletonDependency
    {
        private readonly Lazy<IDictionary<string, SettingDefinition>> _settingDefinitions;
        private readonly IServiceProvider _serviceProvider;
        private readonly SettingOptions _options;

        public SettingDefinitionManager(IOptions<SettingOptions> options, IServiceProvider serviceProvider)
        {
            _settingDefinitions = new Lazy<IDictionary<string, SettingDefinition>>(CreateSettingDefinitions, true);
            _options = options.Value;
            _serviceProvider = serviceProvider;
        }


        public SettingDefinition Get(string name)
        {
            Check.NotNull(name, nameof(name));

            var setting = _settingDefinitions.Value.GetOrDefault(name);
            if (setting == null)
            {
                throw new ScorpioException("Undefined setting: " + name);
            }
            return setting;
        }

        public virtual IReadOnlyList<SettingDefinition> GetAll() => _settingDefinitions.Value.Values.ToImmutableList();

        protected virtual IDictionary<string, SettingDefinition> CreateSettingDefinitions()
        {
            var settings = new Dictionary<string, SettingDefinition>();
            using (var scope = _serviceProvider.CreateScope())
            {
                var providers = _options
                    .DefinitionProviders
                    .Select(p => scope.ServiceProvider.GetRequiredService(p) as ISettingDefinitionProvider)
                    .ToList();

                foreach (var provider in providers)
                {
                    provider.Define(new SettingDefinitionContext(settings));
                }
            }

            return settings;
        }
    }
}
