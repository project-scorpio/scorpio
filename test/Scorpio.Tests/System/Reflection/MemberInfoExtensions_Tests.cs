using Shouldly;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit;

namespace System.Reflection
{
    public class MemberInfoExtensions_Tests
    {
        [Fact]
        public void GetDisplayNameTest()
        {
            typeof(AttributeTestClassWithDisplayNameAttirbute).GetDisplayName().ShouldBe("DispalyNameOfAttributeTestClass");
            typeof(AttributeTestClass).GetDisplayName().ShouldBe("AttributeTestClass");
            new AttributeTestClassWithDisplayNameAttirbute().GetDisplayName().ShouldBe("DispalyNameOfAttributeTestClass");
            new AttributeTestClass().GetDisplayName().ShouldBe("AttributeTestClass");
            new AttributeTestClassWithDisplayAttirbute().GetDisplayName(o => o.Name).ShouldBe("UserName");
            new AttributeTestClassWithDisplayAttirbute().GetDisplayName(o => o.Value).ShouldBe("Value");
            new AttributeTestClassWithDisplayAttirbute().GetDisplayName(o => o.DisplayName).ShouldBe("Display UserName");
            Should.Throw<ArgumentNullException>(() => (null as Type).GetDisplayName());
            Should.Throw<ArgumentNullException>(() => (null as object).GetDisplayName());
        }

        [Fact]
        public void GetDescriptionTest()
        {
            typeof(AttributeTestClassWithDisplayNameAttirbute).GetDescription().ShouldBe("Description of DispalyNameOfAttributeTestClass");
            typeof(AttributeTestClass).GetDescription().ShouldBe("AttributeTestClass");
            new AttributeTestClassWithDisplayNameAttirbute().GetDescription().ShouldBe("Description of DispalyNameOfAttributeTestClass");
            new AttributeTestClass().GetDescription().ShouldBe("AttributeTestClass");
            new AttributeTestClassWithDisplayAttirbute().GetDescription(o => o.Name).ShouldBe("User's name");
            new AttributeTestClassWithDisplayAttirbute().GetDescription(o => o.Value).ShouldBe("Value");
            new AttributeTestClassWithDisplayAttirbute().GetDescription(o => o.DisplayName).ShouldBe("User's display name");
            Should.Throw<ArgumentNullException>(() => (null as Type).GetDescription());
            Should.Throw<ArgumentNullException>(() => (null as object).GetDescription());
            Should.Throw<ArgumentNullException>(() => new AttributeTestClassWithDisplayAttirbute().GetDescription((Linq.Expressions.Expression<Func<AttributeTestClassWithDisplayAttirbute, string>>)null));

        }
        [Fact]
        public void GetAttributeTest()
        {
            typeof(AttributeTestClassWithDisplayNameAttirbute).GetAttribute<DisplayNameAttribute>().ShouldNotBeNull();
            typeof(AttributeTestClass).GetAttribute<DisplayNameAttribute>().ShouldBeNull();
            new AttributeTestClassWithDisplayNameAttirbute().GetAttribute<DisplayNameAttribute>().ShouldNotBeNull();
            new AttributeTestClass().GetAttribute<DisplayNameAttribute>().ShouldBeNull();
            Should.Throw<ArgumentNullException>(() => (null as Type).GetAttribute<DisplayNameAttribute>());
            Should.Throw<ArgumentNullException>(() => (null as object).GetAttribute<DisplayNameAttribute>());
        }

        [Fact]
        public void GetAttributesTest()
        {
            typeof(AttributeTestClassWithDisplayNameAttirbute).GetAttributes<DisplayNameAttribute>().ShouldNotBeEmpty();
            typeof(AttributeTestClass2).GetAttribute<Attribute>().ShouldBeOfType<Scorpio.DependencyInjection.ExposeServicesAttribute>().ShouldNotBeNull();
            typeof(AttributeTestClass).GetAttributes<DisplayNameAttribute>().ShouldBeEmpty();
            new AttributeTestClassWithDisplayNameAttirbute().GetAttributes<DisplayNameAttribute>().ShouldNotBeEmpty();
            new AttributeTestClass().GetAttributes<DisplayNameAttribute>().ShouldBeEmpty();
            Should.Throw<ArgumentNullException>(() => (null as Type).GetAttributes<DisplayNameAttribute>());
            Should.Throw<ArgumentNullException>(() => (null as object).GetAttributes<DisplayNameAttribute>());
        }
        [Fact]
        public void AttributeExistsTest()
        {
            typeof(AttributeTestClassWithDisplayNameAttirbute).AttributeExists<DisplayNameAttribute>().ShouldBeTrue();
            typeof(AttributeTestClass).AttributeExists<DisplayNameAttribute>().ShouldBeFalse();
            new AttributeTestClassWithDisplayNameAttirbute().AttributeExists<DisplayNameAttribute>().ShouldBeTrue();
            new AttributeTestClass().AttributeExists<DisplayNameAttribute>().ShouldBeFalse();
            Should.Throw<ArgumentNullException>(() => (null as Type).AttributeExists<DisplayNameAttribute>());
            Should.Throw<ArgumentNullException>(() => (null as object).AttributeExists<DisplayNameAttribute>());
        }
    }

    [DisplayName("DispalyNameOfAttributeTestClass")]
    [Description("Description of DispalyNameOfAttributeTestClass")]

    class AttributeTestClassWithDisplayNameAttirbute
    {

    }

    class AttributeTestClassWithDisplayAttirbute
    {
        [DisplayName("UserName")]
        [Description("User's name")]
        public string Name { get; set; }
        public string Value { get; set; }

        [Display(Name = "Display UserName", Description = "User's display name")]
        public string DisplayName { get; set; }

    }
    class AttributeTestClass
    {

    }
    [Scorpio.DependencyInjection.ExposeServices()]
    class AttributeTestClass2
    {

    }
}
