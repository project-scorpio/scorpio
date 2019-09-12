using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Setting
{
    class TestSettingDefinitionProvider : ISettingDefinitionProvider
    {
        public void Define(ISettingDefinitionContext context)
        {
            context.Add(
                new SettingDefinition<string>("Setting", defaultValue: "SettingValue"),
                new SettingDefinition<string>("SettingWhthDisplayName", "Setting with display name", "SettingWhthDisplayNameValue"),
                new SettingDefinition<int>("IntegerSetting", defaultValue: 20)
                ) ;
        }
    }
}
