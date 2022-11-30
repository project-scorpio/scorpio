using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

using Shouldly;

using Xunit;

namespace Scorpio.ObjectExtending
{
    public class ExtensionPropertyHelper_Tests
    {
        [Fact]
        public void GetDefaultAttributes()
        {
            ExtensionPropertyHelper.GetDefaultAttributes(typeof(object)).ShouldBeEmpty();
            ExtensionPropertyHelper.GetDefaultAttributes(typeof(int)).ShouldHaveSingleItem().ShouldBeOfType<RequiredAttribute>();
            ExtensionPropertyHelper.GetDefaultAttributes(typeof(int?)).ShouldBeEmpty();
            ExtensionPropertyHelper.GetDefaultAttributes(typeof(StringComparison)).ShouldBeOfTypes(typeof(RequiredAttribute), typeof(EnumDataTypeAttribute));
            ExtensionPropertyHelper.GetDefaultAttributes(typeof(StringComparison?)).ShouldHaveSingleItem().ShouldBeOfType<EnumDataTypeAttribute>();
        }

        [Fact]
        public void GetDefaultValue()
        {
            ExtensionPropertyHelper.GetDefaultValue(typeof(string)).ShouldBeNull();
            ExtensionPropertyHelper.GetDefaultValue(typeof(string), () => "fact").ShouldBe("fact");
            ExtensionPropertyHelper.GetDefaultValue(typeof(string), defaultValue: "value").ShouldBe("value");
            ExtensionPropertyHelper.GetDefaultValue(typeof(string), () => "fact", "value").ShouldBe("fact");
            ExtensionPropertyHelper.GetDefaultValue(typeof(int)).ShouldBe(0);
            Should.Throw<FormatException>(() => ExtensionPropertyHelper.GetDefaultValue(typeof(int), () => "fact").ShouldBe("fact"));
            Should.Throw<FormatException>(() => ExtensionPropertyHelper.GetDefaultValue(typeof(int), defaultValue: "value").ShouldBe("value"));
            Should.Throw<FormatException>(() => ExtensionPropertyHelper.GetDefaultValue(typeof(int), () => "fact", "value").ShouldBe("fact"));
        }
    }
}