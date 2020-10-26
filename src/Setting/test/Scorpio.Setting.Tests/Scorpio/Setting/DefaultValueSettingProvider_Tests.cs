
using Shouldly;

using Xunit;

namespace Scorpio.Setting
{
    public class DefaultValueSettingProvider_Tests
    {
        [Fact]
        public void GetAsync()
        {
            var def = new SettingDefinition<string>("def", "TestValue");
            var provider = new DefaultValueSettingProvider();
            Should.NotThrow(() => provider.GetAsync(def)).Value.ShouldBe("TestValue");
        }
        [Fact]
        public void SetAsync()
        {
            var def = new SettingDefinition<string>("def", "TestValue");
            var provider = new DefaultValueSettingProvider();
            Should.NotThrow(() => provider.GetAsync(def)).Value.ShouldBe("TestValue");
            Should.NotThrow(() => provider.SetAsync(def, "ModifiedValue"));
            Should.NotThrow(() => provider.GetAsync(def)).Value.ShouldBe("TestValue");
        }
    }
}
