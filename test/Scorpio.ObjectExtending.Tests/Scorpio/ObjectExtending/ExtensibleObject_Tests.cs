using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Scorpio.Data;
using Scorpio.DynamicProxy;

using Shouldly;

using Xunit;

namespace Scorpio.ObjectExtending
{
    /// <summary>
    /// 
    /// </summary>
    public class ExtensibleObject_Tests
    {

        [Fact]
        public void ExtensibleObject()
        {
            ObjectExtensionManager.Instance
                .AddOrUpdateProperty<ExtensibleObject, string>("name")
                .AddOrUpdateProperty<ExtensibleObject, int>("age");
            var obj = new ExtensibleObject();
            obj.HasProperty("name").ShouldBeTrue();
            obj.HasProperty("age").ShouldBeTrue();
            obj = new ExtensibleObject(false);
            obj.HasProperty("name").ShouldBeFalse();
            obj.HasProperty("age").ShouldBeFalse();
        }

    }
}
