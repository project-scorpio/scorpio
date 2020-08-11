using System;
using System.Collections.Generic;
using System.Text;

using NSubstitute;

using Shouldly;

using Xunit;

namespace Scorpio.Setting
{
    public class SettingProvider_Tests
    {
        private SettingProvider GetSettingProvider()
        {
            var store = new InMemorySettingStore();
            var provider = Substitute.ForPartsOf<SettingProvider>(store);
            provider.Name.Returns("Default");
            provider.GetProperty("Key").Returns("DefaultKey");
            return provider;
        }

        [Fact]
        public void GetAsync()
        {
            var provider = GetSettingProvider();
            var def = new SettingDefinition<string>("Setting", "DefaultValue");
            Should.NotThrow(() => provider.GetAsync(def)).ShouldBeNull();
        }
        [Fact]
        public void SetAsync()
        {
            var provider = GetSettingProvider();
            var def = new SettingDefinition<string>("Setting", "DefaultValue");
            Should.NotThrow(() => provider.GetAsync(def)).ShouldBeNull();
            Should.NotThrow(() => provider.SetAsync(def,"SettingValue"));
            Should.NotThrow(() => provider.GetAsync(def)).Value.ShouldBe("SettingValue");
        }
    }
}
