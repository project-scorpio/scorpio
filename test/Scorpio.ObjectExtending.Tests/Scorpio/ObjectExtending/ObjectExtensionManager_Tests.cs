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
    public class ObjectExtensionManager_Tests
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
        public void AddOrUpdate_T()
        {
            var manager = new ObjectExtensionManager();
            manager.ObjectsExtensions.ShouldBeEmpty();
            manager.AddOrUpdate<ExtensibleObjectMapperTest>();
            manager.ObjectsExtensions.ShouldHaveSingleItem().Value.Properties.ShouldBeEmpty();
            _ = manager.AddOrUpdate<ExtensibleObjectMapperTest>(c => c.AddOrUpdateProperty<string>("name"));
            manager.ObjectsExtensions.ShouldHaveSingleItem().Value.Properties.ShouldHaveSingleItem().Value.Name.ShouldBe("name");
        }

        [Fact]
        public void AddOrUpdate_Array()
        {
            var manager = new ObjectExtensionManager();
            manager.ObjectsExtensions.ShouldBeEmpty();
            manager.AddOrUpdate(new Type[] {typeof(ExtensibleObjectMapperTest),typeof(ExtensibleObjectMapperTest2) });
            manager.ObjectsExtensions.Values.ShouldContain(i => i.Type == typeof(ExtensibleObjectMapperTest2));
            manager.ObjectsExtensions.Values.ShouldContain(i => i.Type == typeof(ExtensibleObjectMapperTest));
            manager.ObjectsExtensions.Count.ShouldBe(2);
            manager.ObjectsExtensions.Values.First().Properties.ShouldBeEmpty();
            manager.ObjectsExtensions.Values.Last().Properties.ShouldBeEmpty();
            _ = manager.AddOrUpdate(new Type[] { typeof(ExtensibleObjectMapperTest), typeof(ExtensibleObjectMapperTest2) },c => c.AddOrUpdateProperty<string>("name"));
            manager.ObjectsExtensions.Values.ShouldContain(i => i.Type == typeof(ExtensibleObjectMapperTest2));
            manager.ObjectsExtensions.Values.ShouldContain(i => i.Type == typeof(ExtensibleObjectMapperTest));
            manager.ObjectsExtensions.Count.ShouldBe(2);
            manager.ObjectsExtensions.Values.First().Properties.ShouldHaveSingleItem().Value.Name.ShouldBe("name");
            manager.ObjectsExtensions.Values.Last().Properties.ShouldHaveSingleItem().Value.Name.ShouldBe("name");
        }

        [Fact]
        public void AddOrUpdate()
        {
            var manager = new ObjectExtensionManager();
            manager.ObjectsExtensions.ShouldBeEmpty();
            manager.AddOrUpdate(typeof(ExtensibleObjectMapperTest));
            manager.ObjectsExtensions.ShouldHaveSingleItem().Value.Properties.ShouldBeEmpty();
            _ = manager.AddOrUpdate(typeof(ExtensibleObjectMapperTest),c => c.AddOrUpdateProperty<string>("name"));
            manager.ObjectsExtensions.ShouldHaveSingleItem().Value.Properties.ShouldHaveSingleItem().Value.Name.ShouldBe("name");
        }

    }
}
