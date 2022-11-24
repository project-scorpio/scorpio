using System.Threading.Tasks;

namespace Scorpio.Setting
{
    internal class DefaultValueSettingProvider : ISettingProvider, DependencyInjection.ISingletonDependency
    {
        public string Name { get; } = "DefaultValue";

        public Task<SettingValue<T>> GetAsync<T>(SettingDefinition<T> settingDefinition) => Task.FromResult(settingDefinition.Default == null ? null : new SettingValue<T> { Definition = settingDefinition, Value = settingDefinition.Default });

        public Task SetAsync<T>(SettingDefinition<T> settingDefinition, T value) => Task.CompletedTask;
    }
}
