namespace Scorpio.Setting
{
    internal class DefaultSettingProvider : SettingProvider, DependencyInjection.ISingletonDependency
    {
        public DefaultSettingProvider(ISettingStore settingStore) : base(settingStore)
        {
        }

        public override string Name => "Default";

        protected override string Key => null;
    }
}
