namespace Scorpio.Setting
{
    internal class DefaultSettingProvider : SettingProvider, DependencyInjection.ISingletonDependency
    {
        public DefaultSettingProvider(ISettingStore settingStore) : base(settingStore)
        {
        }

        protected override string Key => null;
    }
}
