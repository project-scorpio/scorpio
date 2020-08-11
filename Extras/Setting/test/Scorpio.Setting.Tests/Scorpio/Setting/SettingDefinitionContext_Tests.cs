using System;
using System.Collections.Generic;
using System.Text;

using Shouldly;

using Xunit;

namespace Scorpio.Setting
{
    public class SettingDefinitionContext_Tests
    {
        [Fact]
        public void Add()
        {
            var dict = new Dictionary<string, SettingDefinition>();
            var context = new SettingDefinitionContext(dict);
            Should.NotThrow(() => context.Add());
            dict.Count.ShouldBe(0);
            Should.NotThrow(() => context.Add(new SettingDefinition<string>("Setting")));
            dict.ShouldHaveSingleItem().Key.ShouldBe("Setting");
        }
        [Fact]
        public void GetAll()
        {
            var dict = new Dictionary<string, SettingDefinition>();
            var context = new SettingDefinitionContext(dict);
           context.GetAll().ShouldBeEmpty();
            Should.NotThrow(() => context.Add(new SettingDefinition<string>("Setting")));
            context.GetAll().ShouldHaveSingleItem().Name.ShouldBe("Setting");
        }
        [Fact]
        public void GetOrNull()
        {
            var dict = new Dictionary<string, SettingDefinition>();
            var context = new SettingDefinitionContext(dict);
            Should.NotThrow(() => context.Add(new SettingDefinition<string>("Setting")));
            context.GetOrNull("Setting").ShouldNotBeNull();
            context.GetOrNull("NotExistsSetting").ShouldBeNull();
        }
    }
}
