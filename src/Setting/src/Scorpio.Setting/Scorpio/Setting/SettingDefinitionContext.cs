using System.Collections.Generic;
using System.Collections.Immutable;

namespace Scorpio.Setting
{
    internal class SettingDefinitionContext : ISettingDefinitionContext
    {
        private readonly Dictionary<string, SettingDefinition> _settingDefinitions;

        public SettingDefinitionContext(Dictionary<string, SettingDefinition> settingDefinitions) => _settingDefinitions = settingDefinitions;

        public void Add(params SettingDefinition[] settingDefinitions)
        {
            if (settingDefinitions.IsNullOrEmpty())
            {
                return;
            }

            foreach (var definition in settingDefinitions)
            {
                _settingDefinitions[definition.Name] = definition;
            }
        }

        public virtual IReadOnlyList<SettingDefinition> GetAll() => _settingDefinitions.Values.ToImmutableList();

        public SettingDefinition GetOrNull(string name) => _settingDefinitions.GetOrDefault(name);
    }
}
