using System.Collections.Generic;
using Scorpio.Data;

using Shouldly;

using Xunit;

namespace Scorpio.ObjectExtending
{
    /// <summary>
    /// 
    /// </summary>
    public class HasExtraPropertiesObjectExtendingExtensions_Tests
    {
        public HasExtraPropertiesObjectExtendingExtensions_Tests()
        {
            ObjectExtensionManager.Instance
                .AddOrUpdateProperty<ExtensibleObjectMapperTestSource, string>("name")
                .AddOrUpdateProperty<ExtensibleObjectMapperTestSource, int>("age")
                .AddOrUpdateProperty<ExtensibleObjectMapperTestSource, int>("sex")
                .AddOrUpdateProperty<ExtensibleObjectMapperTestSource, int>("level", i => i.CheckPairDefinitionOnMapping = false)
                .AddOrUpdateProperty<ExtensibleObjectMapperTestSource, int>("timestamp", i => i.CheckPairDefinitionOnMapping = true)
                .AddOrUpdateProperty<ExtensibleObjectMapperTestDistination, string>("name")
                .AddOrUpdateProperty<ExtensibleObjectMapperTestDistination, int>("age")
                .AddOrUpdateProperty<ExtensibleObjectMapperTestDistination, string>("position")
                .AddOrUpdateProperty<ExtensibleObjectMapperTestDistination, int>("lv", i => i.CheckPairDefinitionOnMapping = false)
                .AddOrUpdateProperty<ExtensibleObjectMapperTestDistination, int>("ts", i => i.CheckPairDefinitionOnMapping = true)
;
        }
        private class ExtensibleObjectMapperTestSource : IHasExtraProperties
        {
            public ExtraPropertyDictionary ExtraProperties { get; } = new ExtraPropertyDictionary();
        }

        private class ExtensibleObjectMapperTestDistination : IHasExtraProperties
        {
            public ExtraPropertyDictionary ExtraProperties { get; } = new ExtraPropertyDictionary();
        }

        
        public static IEnumerable<object[]> MapExtraPropertiesToData()
        {
            yield return new object[] { MappingPropertyDefinitionChecks.None, null, true, true, true, true };
            yield return new object[] { MappingPropertyDefinitionChecks.Source, null, true, true, true, false };
            yield return new object[] { MappingPropertyDefinitionChecks.Destination, null, true, true, false, true };
            yield return new object[] { MappingPropertyDefinitionChecks.Both, null, true, true, false, false };
            yield return new object[] { MappingPropertyDefinitionChecks.None, new string[] { "name" }, false, true, true, true };
        }

        [Theory]
        [MemberData(nameof(MapExtraPropertiesToData))]
        public void MapExtraPropertiesTo_Object(MappingPropertyDefinitionChecks? mappingPropertyDefinitionChecks, string[] ignoredProperties, bool except1, bool except2, bool except3, bool except4)
        {
            var source = new ExtensibleObjectMapperTestSource();
            source.SetProperty("name", "scorpio");
            source.SetProperty("age", 123);
            source.SetProperty("sex", 1);
            source.SetProperty("position", "manager");
            var dest = new ExtensibleObjectMapperTestDistination();
            source.MapExtraPropertiesTo( dest, mappingPropertyDefinitionChecks, ignoredProperties);
            dest.HasProperty("name").ShouldBe(except1);
            dest.HasProperty("age").ShouldBe(except2);
            dest.HasProperty("sex").ShouldBe(except3);
            dest.HasProperty("position").ShouldBe(except4);

        }
    }
}