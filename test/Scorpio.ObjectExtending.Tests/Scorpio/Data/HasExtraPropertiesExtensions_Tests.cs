using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Scorpio.ObjectExtending;
using System.Reflection;
using Shouldly;
using Xunit;

namespace Scorpio.Data
{

    /// <summary>
    /// 
    /// </summary>
    public class HasExtraPropertiesExtensions_Tests
    {
        private class HasExtraPropertiesTest : IHasExtraProperties
        {
            public ExtraPropertyDictionary ExtraProperties { get; } = new ExtraPropertyDictionary();

            public string Name { get; set; }
        }

        [Fact]
        public void HasProperty()
        {
            var obj = new HasExtraPropertiesTest();
            obj.ExtraProperties.Add("key", "value");
            obj.HasProperty("key").ShouldBeTrue();
            obj.HasProperty("value").ShouldBeFalse();
        }

        [Fact]
        public void GetProperty()
        {
            var obj = new HasExtraPropertiesTest();
            obj.ExtraProperties.Add("key", "value");
            obj.GetProperty("key").ShouldBeOfType<string>().ShouldBe("value");
            obj.GetProperty("key1").ShouldBe(null);
            obj.GetProperty("key1", (object)"value1").ShouldBe("value1");
        }

        [Fact]
        public void GetProperty_T()
        {
            var obj = new HasExtraPropertiesTest();
            obj.ExtraProperties.Add("key", "123");
            obj.ExtraProperties.Add("guid", Guid.Empty.ToString());
            obj.GetProperty<string>("key").ShouldBeOfType<string>().ShouldBe("123");
            Should.Throw<ScorpioException>(() => obj.GetProperty<object>("key").ShouldBeOfType<object>());
            obj.GetProperty<string>("key1").ShouldBe(null);
            obj.GetProperty("key1", "value1").ShouldBe("value1");
            obj.GetProperty<int>("key1").ShouldBe(0);
            obj.GetProperty<int>("key").ShouldBe(123);
            obj.GetProperty<int?>("key").ShouldBe(123);
            obj.GetProperty<int?>("key1").ShouldBe(null);
            Should.Throw<FormatException>(() => obj.GetProperty<int>("guid").ShouldBe(0));
            obj.GetProperty<Guid>("guid").ShouldBe(Guid.Empty);
            obj.GetProperty<Guid?>("guid").ShouldBe(Guid.Empty);
        }

        [Fact]
        public void SetProperty()
        {
            var obj = new HasExtraPropertiesTest();
            obj.ExtraProperties.ShouldBeEmpty();
            obj.SetProperty("key", "value");
            obj.ExtraProperties.ShouldHaveSingleItem().Key.ShouldBe("key");
        }
        [Fact]
        public void RemoveProperty()
        {
            var obj = new HasExtraPropertiesTest();
            obj.ExtraProperties.ShouldBeEmpty();
            obj.SetProperty("key", "value");
            obj.ExtraProperties.ShouldHaveSingleItem().Key.ShouldBe("key");
            obj.RemoveProperty("key");
            obj.ExtraProperties.ShouldBeEmpty();
        }


        [Fact]
        public void SetDefaultsForExtraProperties_T()
        {
            ObjectExtensionManager.Instance
                .AddOrUpdateProperty<HasExtraPropertiesTest, string>("name")
                .AddOrUpdateProperty<HasExtraPropertiesTest, int>("age");
            var obj = new HasExtraPropertiesTest();
            obj.SetDefaultsForExtraProperties(typeof(string));
            obj.ExtraProperties.ShouldBeEmpty();
            obj.SetDefaultsForExtraProperties();
            obj.HasProperty("name").ShouldBeTrue();
            obj.HasProperty("age").ShouldBeTrue();
            obj.SetDefaultsForExtraProperties();
            obj.ExtraProperties.Where(kv => kv.Key == "name").ShouldHaveSingleItem();
            obj.ExtraProperties.Where(kv => kv.Key == "age").ShouldHaveSingleItem();
        }

        [Fact]
        public void SetDefaultsForExtraProperties()
        {
            ObjectExtensionManager.Instance
                .AddOrUpdateProperty<HasExtraPropertiesTest, string>("name")
                .AddOrUpdateProperty<HasExtraPropertiesTest, int>("age");
            var obj = new HasExtraPropertiesTest();
            Should.Throw<ArgumentException>(() => HasExtraPropertiesExtensions.SetDefaultsForExtraProperties(new object(), typeof(string)));
            Should.NotThrow(() => HasExtraPropertiesExtensions.SetDefaultsForExtraProperties((object)obj, typeof(HasExtraPropertiesTest)));
            obj.HasProperty("name").ShouldBeTrue();
            obj.HasProperty("age").ShouldBeTrue();
        }

        [Fact]
        public void SetExtraPropertiesToRegularProperties()
        {
            var obj = new HasExtraPropertiesTest();
            obj.SetProperty("Name", "scorpio");
            obj.Name.ShouldBeNull();
            obj.SetExtraPropertiesToRegularProperties();
            obj.Name.ShouldBe("scorpio");
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="source"></param>
        //public static void SetExtraPropertiesToRegularProperties(this IHasExtraProperties source)
        //{
        //    var properties = source.GetType().GetProperties()
        //        .Where(x => source.ExtraProperties.ContainsKey(x.Name)
        //                    && x.GetSetMethod(true) != null)
        //        .ToList();

        //    foreach (var property in properties)
        //    {
        //        property.SetValue(source, source.ExtraProperties[property.Name]);
        //        source.RemoveProperty(property.Name);
        //    }
        //}
    }
}

