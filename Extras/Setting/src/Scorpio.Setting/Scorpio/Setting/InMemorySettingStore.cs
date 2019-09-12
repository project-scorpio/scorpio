using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.Setting
{
    class InMemorySettingStore : ISettingStore, DependencyInjection.ISingletonDependency
    {
        private readonly Dictionary<string, object> _values;

        public InMemorySettingStore()
        {
            _values = new Dictionary<string, object>();
        }
        public Task<SettingValue<T>> GetAsync<T>(ISettingStoreContext context)
        {
            var key = $"{context.Properties.Values.ExpandToString(":")}:{context.SettingDefinition.Name}";
            var value = _values.GetOrDefault(key);
            if (value == null)
            {
                return Task.FromResult<SettingValue<T>>(null);
            }
            return Task.FromResult(new SettingValue<T> { Definition = context.SettingDefinition, Value = (T)value });
        }

        public Task SetAsync<T>(ISettingStoreContext context, T value)
        {
            var key = $"{context.Properties.Values.ExpandToString(":")}:{context.SettingDefinition.Name}";
            _values[key] = value;
            return Task.CompletedTask;
        }
    }
}
