namespace Scorpio.Setting
{
    class TestSettingDefinitionProvider : ISettingDefinitionProvider
    {
        public void Define(ISettingDefinitionContext context)
        {
            context.Add(
                new SettingDefinition<string>("Setting", defaultValue: "SettingValue"),
                new SettingDefinition<string>("DefaultSetting"),
                new SettingDefinition<string>("SettingWhthDisplayName", "Setting with display name","Setting description" ,"SettingWhthDisplayNameValue"),
                new SettingDefinition<int>("IntegerSetting", defaultValue: 20)
                );
        }
    }
}
