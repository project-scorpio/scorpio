using System;
using System.Collections.Generic;
using System.Linq;

using Scorpio.Data;

using Shouldly;

using Xunit;

namespace Scorpio.ObjectExtending
{
    /// <summary>
    /// 
    /// </summary>
    public class ExtensibleObjectMapper_Tests
    {
        public ExtensibleObjectMapper_Tests()
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

        private class ExtensibleObjectMapperTestNoProperties : IHasExtraProperties
        {
            public ExtraPropertyDictionary ExtraProperties { get; } = new ExtraPropertyDictionary();
        }

        public static IEnumerable<object[]> MapExtraPropertiesToData()
        {
            yield return new object[] { MappingPropertyDefinitionChecks.None, null,true, true, true, true };
            yield return new object[] { MappingPropertyDefinitionChecks.Source, null, true, true, true, false };
            yield return new object[] { MappingPropertyDefinitionChecks.Destination, null, true, true, false, true };
            yield return new object[] { MappingPropertyDefinitionChecks.Both, null, true, true, false, false };
            yield return new object[] { MappingPropertyDefinitionChecks.None, new string[] { "name" }, false, true, true, true };
        }

        [Theory]
        [MemberData(nameof(MapExtraPropertiesToData))]
        public void MapExtraPropertiesTo_Object(MappingPropertyDefinitionChecks? mappingPropertyDefinitionChecks, string[] ignoredProperties,bool except1, bool except2, bool except3, bool except4)
        {
            var source = new ExtensibleObjectMapperTestSource();
            source.SetProperty("name", "scorpio");
            source.SetProperty("age", 123);
            source.SetProperty("sex", 1);
            source.SetProperty("position", "manager");
            var dest = new ExtensibleObjectMapperTestDistination();
            ExtensibleObjectMapper.MapExtraPropertiesTo(source, dest,mappingPropertyDefinitionChecks,ignoredProperties);
            dest.HasProperty("name").ShouldBe(except1);
            dest.HasProperty("age").ShouldBe(except2);
            dest.HasProperty("sex").ShouldBe(except3);
            dest.HasProperty("position").ShouldBe(except4);
            
        }
        [Theory]
        [MemberData(nameof(MapExtraPropertiesToData))]
        public void MapExtraPropertiesTo_Dict_T(MappingPropertyDefinitionChecks? mappingPropertyDefinitionChecks, string[] ignoredProperties, bool except1, bool except2, bool except3, bool except4)
        {
            var source = new ExtraPropertyDictionary
            {
                { "name", "scorpio" },
                { "age", 123 },
                { "sex", 1 },
                { "position", "manager" }
            };
            var dest = new ExtraPropertyDictionary();
            ExtensibleObjectMapper.MapExtraPropertiesTo<ExtensibleObjectMapperTestSource, ExtensibleObjectMapperTestDistination>(source, dest, mappingPropertyDefinitionChecks, ignoredProperties);
            dest.ContainsKey("name").ShouldBe(except1);
            dest.ContainsKey("age").ShouldBe(except2);
            dest.ContainsKey("sex").ShouldBe(except3);
            dest.ContainsKey("position").ShouldBe(except4);
           
        }

        [Theory]
        [MemberData(nameof(MapExtraPropertiesToData))]
        public void MapExtraPropertiesTo_Dict(MappingPropertyDefinitionChecks? mappingPropertyDefinitionChecks, string[] ignoredProperties, bool except1, bool except2, bool except3, bool except4)
        {
            var source = new ExtraPropertyDictionary
            {
                { "name", "scorpio" },
                { "age", 123 },
                { "sex", 1 },
                { "position", "manager" }
            };
            var dest = new ExtraPropertyDictionary();
            ExtensibleObjectMapper.MapExtraPropertiesTo(typeof(ExtensibleObjectMapperTestSource), typeof(ExtensibleObjectMapperTestDistination), source, dest, mappingPropertyDefinitionChecks, ignoredProperties);
            dest.ContainsKey("name").ShouldBe(except1);
            dest.ContainsKey("age").ShouldBe(except2);
            dest.ContainsKey("sex").ShouldBe(except3);
            dest.ContainsKey("position").ShouldBe(except4);

        }
        public static IEnumerable<object[]> CanMapPropertyData()
        {
            yield return new object[] { "name", MappingPropertyDefinitionChecks.None, null, true };
            yield return new object[] { "name", MappingPropertyDefinitionChecks.Source, null, true };
            yield return new object[] { "name", MappingPropertyDefinitionChecks.Destination, null, true };
            yield return new object[] { "name", MappingPropertyDefinitionChecks.Both, null, true };
            yield return new object[] { "name", null, null, true };
            yield return new object[] { "name", MappingPropertyDefinitionChecks.None, new string[] { "name"}, false };
            yield return new object[] { "name", MappingPropertyDefinitionChecks.Source, new string[] { "name" }, false };
            yield return new object[] { "name", MappingPropertyDefinitionChecks.Destination, new string[] { "name" }, false };
            yield return new object[] { "name", MappingPropertyDefinitionChecks.Both, new string[] { "name" }, false };
            yield return new object[] { "name", null, new string[] { "name" }, false };

            yield return new object[] { "sex", MappingPropertyDefinitionChecks.None, null, true };
            yield return new object[] { "sex", MappingPropertyDefinitionChecks.Source, null, true };
            yield return new object[] { "sex", MappingPropertyDefinitionChecks.Destination, null, false };
            yield return new object[] { "sex", MappingPropertyDefinitionChecks.Both, null, false };
            yield return new object[] { "sex", null, null, false };
            yield return new object[] { "sex", MappingPropertyDefinitionChecks.None, new string[] { "sex" }, false };
            yield return new object[] { "sex", MappingPropertyDefinitionChecks.Source, new string[] { "sex" }, false };
            yield return new object[] { "sex", MappingPropertyDefinitionChecks.Destination, new string[] { "sex" }, false };
            yield return new object[] { "sex", MappingPropertyDefinitionChecks.Both, new string[] { "sex" }, false };
            yield return new object[] { "sex", null, new string[] { "sex" }, false };

            yield return new object[] { "level", MappingPropertyDefinitionChecks.None, null, true };
            yield return new object[] { "level", MappingPropertyDefinitionChecks.Source, null, true };
            yield return new object[] { "level", MappingPropertyDefinitionChecks.Destination, null, false };
            yield return new object[] { "level", MappingPropertyDefinitionChecks.Both, null, false };
            yield return new object[] { "level", null, null, true };
            yield return new object[] { "level", MappingPropertyDefinitionChecks.None, new string[] { "level" }, false };
            yield return new object[] { "level", MappingPropertyDefinitionChecks.Source, new string[] { "level" }, false };
            yield return new object[] { "level", MappingPropertyDefinitionChecks.Destination, new string[] { "level" }, false };
            yield return new object[] { "level", MappingPropertyDefinitionChecks.Both, new string[] { "level" }, false };
            yield return new object[] { "level", null, new string[] { "level" }, false };

            yield return new object[] { "timestamp", MappingPropertyDefinitionChecks.None, null, true };
            yield return new object[] { "timestamp", MappingPropertyDefinitionChecks.Source, null, true };
            yield return new object[] { "timestamp", MappingPropertyDefinitionChecks.Destination, null, false };
            yield return new object[] { "timestamp", MappingPropertyDefinitionChecks.Both, null, false };
            yield return new object[] { "timestamp", null, null, false };
            yield return new object[] { "timestamp", MappingPropertyDefinitionChecks.None, new string[] { "timestamp" }, false };
            yield return new object[] { "timestamp", MappingPropertyDefinitionChecks.Source, new string[] { "timestamp" }, false };
            yield return new object[] { "timestamp", MappingPropertyDefinitionChecks.Destination, new string[] { "timestamp" }, false };
            yield return new object[] { "timestamp", MappingPropertyDefinitionChecks.Both, new string[] { "timestamp" }, false };
            yield return new object[] { "timestamp", null, new string[] { "timestamp" }, false };

            yield return new object[] { "position", MappingPropertyDefinitionChecks.None, null, true };
            yield return new object[] { "position", MappingPropertyDefinitionChecks.Source, null, false };
            yield return new object[] { "position", MappingPropertyDefinitionChecks.Destination, null, true };
            yield return new object[] { "position", MappingPropertyDefinitionChecks.Both, null, false };
            yield return new object[] { "position", null, null, false };
            yield return new object[] { "position", MappingPropertyDefinitionChecks.None, new string[] { "position" }, false };
            yield return new object[] { "position", MappingPropertyDefinitionChecks.Source, new string[] { "position" }, false };
            yield return new object[] { "position", MappingPropertyDefinitionChecks.Destination, new string[] { "position" }, false };
            yield return new object[] { "position", MappingPropertyDefinitionChecks.Both, new string[] { "position" }, false };
            yield return new object[] { "position", null, new string[] { "position" }, false };

            yield return new object[] { "lv", MappingPropertyDefinitionChecks.None, null, true };
            yield return new object[] { "lv", MappingPropertyDefinitionChecks.Source, null, false };
            yield return new object[] { "lv", MappingPropertyDefinitionChecks.Destination, null, true };
            yield return new object[] { "lv", MappingPropertyDefinitionChecks.Both, null, false };
            yield return new object[] { "lv", null, null, true };
            yield return new object[] { "lv", MappingPropertyDefinitionChecks.None, new string[] { "lv" }, false };
            yield return new object[] { "lv", MappingPropertyDefinitionChecks.Source, new string[] { "lv" }, false };
            yield return new object[] { "lv", MappingPropertyDefinitionChecks.Destination, new string[] { "lv" }, false };
            yield return new object[] { "lv", MappingPropertyDefinitionChecks.Both, new string[] { "lv" }, false };
            yield return new object[] { "lv", null, new string[] { "lv" }, false };

            yield return new object[] { "ts", MappingPropertyDefinitionChecks.None, null, true };
            yield return new object[] { "ts", MappingPropertyDefinitionChecks.Source, null, false };
            yield return new object[] { "ts", MappingPropertyDefinitionChecks.Destination, null, true };
            yield return new object[] { "ts", MappingPropertyDefinitionChecks.Both, null, false };
            yield return new object[] { "ts", null, null, false };
            yield return new object[] { "ts", MappingPropertyDefinitionChecks.None, new string[] { "ts" }, false };
            yield return new object[] { "ts", MappingPropertyDefinitionChecks.Source, new string[] { "ts" }, false };
            yield return new object[] { "ts", MappingPropertyDefinitionChecks.Destination, new string[] { "ts" }, false };
            yield return new object[] { "ts", MappingPropertyDefinitionChecks.Both, new string[] { "ts" }, false };
            yield return new object[] { "ts", null, new string[] { "ts" }, false };
        }

        [Theory]
        [MemberData(nameof(CanMapPropertyData))]
        public void CanMapProperty_T(string propertyName,MappingPropertyDefinitionChecks? propertyDefinitionChecks, string[] ignoredProperties,bool except)
        {
            ExtensibleObjectMapper.CanMapProperty<ExtensibleObjectMapperTestSource, ExtensibleObjectMapperTestDistination>(propertyName,propertyDefinitionChecks,ignoredProperties).ShouldBe(except);
        }

        [Theory]
        [MemberData(nameof(CanMapPropertyData))]
        public void CanMapProperty(string propertyName, MappingPropertyDefinitionChecks? propertyDefinitionChecks, string[] ignoredProperties, bool except)
        {
            ExtensibleObjectMapper.CanMapProperty(typeof(ExtensibleObjectMapperTestSource), typeof(ExtensibleObjectMapperTestDistination),propertyName, propertyDefinitionChecks, ignoredProperties).ShouldBe(except);
        }

        public static IEnumerable<object[]> CanMapPropertyTypeData()
        {
            yield return new object[] { typeof(ExtensibleObjectMapperTestSource), typeof(ExtensibleObjectMapperTestNoProperties), MappingPropertyDefinitionChecks.None, true };
            yield return new object[] { typeof(ExtensibleObjectMapperTestSource), typeof(ExtensibleObjectMapperTestNoProperties), MappingPropertyDefinitionChecks.Source, true };
            yield return new object[] { typeof(ExtensibleObjectMapperTestSource), typeof(ExtensibleObjectMapperTestNoProperties), MappingPropertyDefinitionChecks.Destination, false };
            yield return new object[] { typeof(ExtensibleObjectMapperTestSource), typeof(ExtensibleObjectMapperTestNoProperties), null, false };
            yield return new object[] { typeof(ExtensibleObjectMapperTestNoProperties), typeof(ExtensibleObjectMapperTestDistination), MappingPropertyDefinitionChecks.None, true };
            yield return new object[] { typeof(ExtensibleObjectMapperTestNoProperties), typeof(ExtensibleObjectMapperTestDistination), MappingPropertyDefinitionChecks.Source, false };
            yield return new object[] { typeof(ExtensibleObjectMapperTestNoProperties), typeof(ExtensibleObjectMapperTestDistination), MappingPropertyDefinitionChecks.Destination, true };
            yield return new object[] { typeof(ExtensibleObjectMapperTestNoProperties), typeof(ExtensibleObjectMapperTestDistination), null, false };
        }

        [Theory]
        [MemberData(nameof(CanMapPropertyTypeData))]
        public void CanMapPropertyType(Type souceType,Type descType, MappingPropertyDefinitionChecks? propertyDefinitionChecks ,bool except)
        {
            ExtensibleObjectMapper.CanMapProperty(souceType,descType, "name", propertyDefinitionChecks).ShouldBe(except);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sourceType"></param>
        ///// <param name="destinationType"></param>
        ///// <param name="propertyName"></param>
        ///// <param name="definitionChecks"></param>
        ///// <param name="ignoredProperties"></param>
        ///// <returns></returns>
        //public static bool CanMapProperty(
        //    Type sourceType,
        //    Type destinationType,
        //    string propertyName,
        //    MappingPropertyDefinitionChecks? definitionChecks = null,
        //    string[] ignoredProperties = null)
        //{
        //    Check.AssignableTo<IHasExtraProperties>(sourceType, nameof(sourceType));
        //    Check.AssignableTo<IHasExtraProperties>(destinationType, nameof(destinationType));
        //    Check.NotNull(propertyName, nameof(propertyName));

        //    var sourceObjectExtension = ObjectExtensionManager.Instance.GetOrNull(sourceType);
        //    var destinationObjectExtension = ObjectExtensionManager.Instance.GetOrNull(destinationType);

        //    return CanMapProperty(
        //        propertyName,
        //        sourceObjectExtension,
        //        destinationObjectExtension,
        //        definitionChecks,
        //        ignoredProperties);
        //}

        //private static bool CanMapProperty(
        //    string propertyName,
        //    ObjectExtensionInfo sourceObjectExtension,
        //    ObjectExtensionInfo destinationObjectExtension,
        //    MappingPropertyDefinitionChecks? definitionChecks = null,
        //    string[] ignoredProperties = null)
        //{
        //    Check.NotNull(propertyName, nameof(propertyName));

        //    if (ignoredProperties != null &&
        //        ignoredProperties.Contains(propertyName))
        //    {
        //        return false;
        //    }

        //    if (definitionChecks != null)
        //    {
        //        if (definitionChecks.Value.HasFlag(MappingPropertyDefinitionChecks.Source))
        //        {
        //            if (sourceObjectExtension == null)
        //            {
        //                return false;
        //            }

        //            if (!sourceObjectExtension.HasProperty(propertyName))
        //            {
        //                return false;
        //            }
        //        }

        //        if (definitionChecks.Value.HasFlag(MappingPropertyDefinitionChecks.Destination))
        //        {
        //            if (destinationObjectExtension == null)
        //            {
        //                return false;
        //            }

        //            if (!destinationObjectExtension.HasProperty(propertyName))
        //            {
        //                return false;
        //            }
        //        }

        //        return true;
        //    }
        //    else
        //    {
        //        var sourcePropertyDefinition = sourceObjectExtension?.GetPropertyOrNull(propertyName);
        //        var destinationPropertyDefinition = destinationObjectExtension?.GetPropertyOrNull(propertyName);

        //        if (sourcePropertyDefinition != null)
        //        {
        //            if (destinationPropertyDefinition != null)
        //            {
        //                return true;
        //            }

        //            if (sourcePropertyDefinition.CheckPairDefinitionOnMapping == false)
        //            {
        //                return true;
        //            }
        //        }
        //        else if (destinationPropertyDefinition != null)
        //        {
        //            if (destinationPropertyDefinition.CheckPairDefinitionOnMapping == false)
        //            {
        //                return true;
        //            }
        //        }

        //        return false;
        //    }
        //}
    }
}
