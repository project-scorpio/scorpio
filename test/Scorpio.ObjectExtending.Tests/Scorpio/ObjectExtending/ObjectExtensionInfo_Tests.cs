using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Scorpio.Data;

using Shouldly;

using Xunit;

namespace Scorpio.ObjectExtending
{

    /// <summary>
    /// 
    /// </summary>
    public class ObjectExtensionInfo_Tests
    {


        private class ExtensibleObjectMapperTest : IHasExtraProperties
        {
            public ExtraPropertyDictionary ExtraProperties { get; } = new ExtraPropertyDictionary();
        }

        [Fact]
        public void AddOrUpdateProperty_T()
        {
            var info = new ObjectExtensionInfo(typeof(ExtensibleObjectMapperTest));
            info.HasProperty("name").ShouldBeFalse();
            info.AddOrUpdateProperty<string>("name");
            info.HasProperty("name").ShouldBeTrue();
            info.Properties.ShouldHaveSingleItem().Value.Name.ShouldBe("name");
            info.Properties.ShouldHaveSingleItem().Value.CheckPairDefinitionOnMapping.ShouldBeNull();
            info.AddOrUpdateProperty<string>("name", i => i.CheckPairDefinitionOnMapping = true);
            info.Properties.ShouldHaveSingleItem().Value.Name.ShouldBe("name");
            info.Properties.ShouldHaveSingleItem().Value.CheckPairDefinitionOnMapping.ShouldBe(true);
        }

        [Fact]
        public void AddOrUpdateProperty()
        {
            var info = new ObjectExtensionInfo(typeof(ExtensibleObjectMapperTest));
            info.HasProperty("name").ShouldBeFalse();
            info.AddOrUpdateProperty(typeof(string), "name");
            info.HasProperty("name").ShouldBeTrue();
            info.Properties.ShouldHaveSingleItem().Value.Name.ShouldBe("name");
            info.Properties.ShouldHaveSingleItem().Value.CheckPairDefinitionOnMapping.ShouldBeNull();
            info.AddOrUpdateProperty(typeof(string), "name", i => i.CheckPairDefinitionOnMapping = true);
            info.Properties.ShouldHaveSingleItem().Value.Name.ShouldBe("name");
            info.Properties.ShouldHaveSingleItem().Value.CheckPairDefinitionOnMapping.ShouldBe(true);

        }

        [Fact]
        public void GetProperties()
        {
            var info = new ObjectExtensionInfo(typeof(ExtensibleObjectMapperTest));
            info.AddOrUpdateProperty<string>("name");
            info.AddOrUpdateProperty<int>("age");
            info.AddOrUpdateProperty<string>("location");
            info.GetProperties().Select(i => i.Name).ShouldBeInOrder();

        }

    }
}
