using System.Threading.Tasks;

namespace Scorpio.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class SettingProvider : ISettingProvider, DependencyInjection.ISingletonDependency
    {
        private readonly ISettingStore _settingStore;

        /// <summary>
        /// 
        /// </summary>
        public string Name => "Default";

        /// <summary>
        /// 
        /// </summary>
        protected abstract string Key { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingStore"></param>
        protected SettingProvider(ISettingStore settingStore)
        {
            _settingStore = settingStore;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settingDefinition"></param>
        /// <returns></returns>
        public virtual async Task<SettingValue<T>> GetAsync<T>(SettingDefinition<T> settingDefinition)
        {
            var context = CreateContext(settingDefinition);
            var value = await _settingStore.GetAsync<T>(context);
            if (value == null)
            {
                return null;
            }
            return new SettingValue<T> { Definition = value.Definition, Value = value.Value };
        }

        private SettingStoreContext CreateContext<T>(SettingDefinition<T> settingDefinition)
        {
            var context = new SettingStoreContext(settingDefinition);
            context.Properties["ProviderName"] = Name;
            if (Key != null)
            {
                context.Properties["ProviderKey"] = Key;
            }
            ConfigContext(context);
            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settingDefinition"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual async Task SetAsync<T>(SettingDefinition<T> settingDefinition, T value)
        {
            var context = CreateContext(settingDefinition);
            await _settingStore.SetAsync(context, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected virtual void ConfigContext(ISettingStoreContext context)
        {

        }
    }
}
