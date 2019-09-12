using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;
namespace Scorpio.Setting
{
    public class SettingDeinitionManager_Tests:TestBase.IntegratedTest<SettingTestModule>
    {
        private readonly ISettingDefinitionManager _settingDefinitionManager;


        public SettingDeinitionManager_Tests()
        {
            _settingDefinitionManager = ServiceProvider.GetService<ISettingDefinitionManager>();

        }

        [Fact]
        public void SettingDefinitionManagerTest()
        {
            _settingDefinitionManager.ShouldBeOfType<SettingDefinitionManager>().ShouldNotBeNull();
        }

        [Fact]
        public void SettingDefinitionCount()
        {
            _settingDefinitionManager.GetAll().Count.ShouldBe(3);
        }

        [Fact]
        public void SettingDefinitionFirst()
        {
            _settingDefinitionManager.Get("Setting").ShouldBeOfType<SettingDefinition<string>>().Name.ShouldBe("Setting");

        }
        [Fact]
        public void SettingDefinitionThrowWhenNotExists()
        {
            _settingDefinitionManager.Get("Setting").ShouldBeOfType<SettingDefinition<string>>().Name.ShouldBe("Setting");
            Should.Throw(() => {
                _settingDefinitionManager.Get("SettingNotExists");
            }, typeof(ScorpioException));
        }
    }
}
