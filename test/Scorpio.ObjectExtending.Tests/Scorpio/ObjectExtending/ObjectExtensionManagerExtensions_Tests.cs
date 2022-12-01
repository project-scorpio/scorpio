using System;
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
    public class ObjectExtensionManagerExtensions_Tests
    {
        private class ExtensibleObjectMapperTest : IHasExtraProperties
        {
            public ExtraPropertyDictionary ExtraProperties { get; } = new ExtraPropertyDictionary();
        }
        private class ExtensibleObjectMapperTest2 : IHasExtraProperties
        {
            public ExtraPropertyDictionary ExtraProperties { get; } = new ExtraPropertyDictionary();
        }

        [Fact]
        public void AddOrUpdateProperty_T_Array()
        {
            var manager = new ObjectExtensionManager();
            manager.AddOrUpdateProperty<string>(new Type[]
            {
                typeof(ExtensibleObjectMapperTest),
                typeof(ExtensibleObjectMapperTest2)
            }, "name", c => c.DefaultValue = "scorpio");
            manager.ObjectsExtensions.Values.ShouldContain(i => i.Type == typeof(ExtensibleObjectMapperTest2));
            manager.ObjectsExtensions.Values.ShouldContain(i => i.Type == typeof(ExtensibleObjectMapperTest));
            manager.ObjectsExtensions.Count.ShouldBe(2);
            manager.ObjectsExtensions.Values.First().Properties.ShouldHaveSingleItem().Value.Name.ShouldBe("name");
            manager.ObjectsExtensions.Values.Last().Properties.ShouldHaveSingleItem().Value.Name.ShouldBe("name");
            manager.ObjectsExtensions.Values.First().Properties.ShouldHaveSingleItem().Value.DefaultValue.ShouldBe("scorpio");
            manager.ObjectsExtensions.Values.Last().Properties.ShouldHaveSingleItem().Value.DefaultValue.ShouldBe("scorpio");

        }

        [Fact]
        public void AddOrUpdateProperty_Array()
        {
            var manager = new ObjectExtensionManager();
            manager.AddOrUpdateProperty(new Type[]
            {
                typeof(ExtensibleObjectMapperTest),
                typeof(ExtensibleObjectMapperTest2)
            }, typeof(string), "name", c => c.DefaultValue = "scorpio");
            manager.ObjectsExtensions.Values.ShouldContain(i => i.Type == typeof(ExtensibleObjectMapperTest2));
            manager.ObjectsExtensions.Values.ShouldContain(i => i.Type == typeof(ExtensibleObjectMapperTest));
            manager.ObjectsExtensions.Count.ShouldBe(2);
            manager.ObjectsExtensions.Values.First().Properties.ShouldHaveSingleItem().Value.Name.ShouldBe("name");
            manager.ObjectsExtensions.Values.Last().Properties.ShouldHaveSingleItem().Value.Name.ShouldBe("name");
            manager.ObjectsExtensions.Values.First().Properties.ShouldHaveSingleItem().Value.DefaultValue.ShouldBe("scorpio");
            manager.ObjectsExtensions.Values.Last().Properties.ShouldHaveSingleItem().Value.DefaultValue.ShouldBe("scorpio");

        }

        [Fact]
        public void AddOrUpdateProperty_T()
        {
            var manager = new ObjectExtensionManager();
            manager.AddOrUpdateProperty<ExtensibleObjectMapperTest, string>("name", c => c.DefaultValue = "scorpio");
            manager.ObjectsExtensions.ShouldHaveSingleItem().Value.Properties.ShouldHaveSingleItem().Value.Name.ShouldBe("name");
            manager.ObjectsExtensions.ShouldHaveSingleItem().Value.Properties.ShouldHaveSingleItem().Value.DefaultValue.ShouldBe("scorpio");

        }

        [Fact]
        public void AddOrUpdateProperty()
        {
            var manager = new ObjectExtensionManager();
            manager.AddOrUpdateProperty(typeof(ExtensibleObjectMapperTest), typeof(string), "name", c => c.DefaultValue = "scorpio");
            manager.ObjectsExtensions.ShouldHaveSingleItem().Value.Properties.ShouldHaveSingleItem().Value.Name.ShouldBe("name");
            manager.ObjectsExtensions.ShouldHaveSingleItem().Value.Properties.ShouldHaveSingleItem().Value.DefaultValue.ShouldBe("scorpio");

        }

        [Fact]
        public void GetPropertyOrNull_T()
        {
            var manager = new ObjectExtensionManager();
            manager.GetPropertyOrNull<ExtensibleObjectMapperTest>("name").ShouldBeNull();
            manager.AddOrUpdateProperty<ExtensibleObjectMapperTest, string>("name");
            manager.GetPropertyOrNull<ExtensibleObjectMapperTest>("name").ShouldNotBeNull().Name.ShouldBe("name");
        }

        [Fact]
        public void GetPropertyOrNull()
        {
            var manager = new ObjectExtensionManager();
            manager.GetPropertyOrNull(typeof(ExtensibleObjectMapperTest), "name").ShouldBeNull();
            manager.AddOrUpdateProperty<ExtensibleObjectMapperTest, string>("name");
            manager.GetPropertyOrNull(typeof(ExtensibleObjectMapperTest), "name").ShouldNotBeNull().Name.ShouldBe("name");
        }

        [Fact]
        public void GetProperties_T()
        {
            var manager = new ObjectExtensionManager();
            manager.GetProperties<ExtensibleObjectMapperTest>().ShouldNotBeNull().ShouldBeEmpty();
            manager.AddOrUpdateProperty<ExtensibleObjectMapperTest, string>("name");
            manager.GetProperties<ExtensibleObjectMapperTest>().ShouldNotBeNull().ShouldHaveSingleItem().Name.ShouldBe("name");
        }
        [Fact]
        public void GetProperties()
        {
            var manager = new ObjectExtensionManager();
            manager.GetProperties(typeof(ExtensibleObjectMapperTest)).ShouldNotBeNull().ShouldBeEmpty();
            manager.AddOrUpdateProperty<ExtensibleObjectMapperTest, string>("name");
            manager.GetProperties(typeof(ExtensibleObjectMapperTest)).ShouldNotBeNull().ShouldHaveSingleItem().Name.ShouldBe("name");
        }
    }
}