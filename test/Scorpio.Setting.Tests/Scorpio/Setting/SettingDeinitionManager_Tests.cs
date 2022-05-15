using Microsoft.Extensions.DependencyInjection;

using Shouldly;

using Xunit;
namespace Scorpio.Setting
{
    public class SettingDeinitionManager_Tests : TestBase.IntegratedTest<SettingTestModule>
    {
        private readonly ISettingDefinitionManager _settingDefinitionManager;


        public SettingDeinitionManager_Tests() => _settingDefinitionManager = ServiceProvider.GetService<ISettingDefinitionManager>();

        [Fact]
        public void SettingDefinitionManagerTest() => _settingDefinitionManager.ShouldBeOfType<SettingDefinitionManager>().ShouldNotBeNull();

        [Fact]
        public void SettingDefinitionCount() => _settingDefinitionManager.GetAll().Count.ShouldBe(4);

        [Fact]
        public void SettingDefinitionFirst()
        {
            var def = _settingDefinitionManager.Get("SettingWhthDisplayName").ShouldBeOfType<SettingDefinition<string>>();
            def.Name.ShouldBe("SettingWhthDisplayName");
            def.DisplayName.ShouldBe("Setting with display name");
            def.Description.ShouldBe("Setting description");
            def.Default.ShouldBe("SettingWhthDisplayNameValue");
            def.ValueType.ShouldBe(typeof(string));

        }
        [Fact]
        public void SettingDefinitionThrowWhenNotExists()
        {
            _settingDefinitionManager.Get("Setting").ShouldBeOfType<SettingDefinition<string>>().Name.ShouldBe("Setting");
            Should.Throw(() => _settingDefinitionManager.Get("SettingNotExists"), typeof(ScorpioException));
        }
    }
}
