using System;
using System.Collections.Generic;
using System.Text;

using Shouldly;

using Xunit;

namespace Scorpio.Authorization.Permissions
{
    public class PermissionDefinition_Tests
    {
        [Fact]
        public void Name()
        {
            var def = new PermissionDefinition("Test");
            def.Name.ShouldBe("Test");
            def.ToString().ShouldBe("[PermissionDefinition Test]");
        }
        [Fact]
        public void DisplayName()
        {
            var def = new PermissionDefinition("Test", "DisplayName");
            def.DisplayName.ShouldBe("DisplayName");
            Should.Throw<ArgumentNullException>(() => def.DisplayName = null);
            Should.NotThrow(() => def.DisplayName = "TestDisplay");
            def.DisplayName.ShouldBe("TestDisplay");
        }

        [Fact]
        public void Properties()
        {
            var def = new PermissionDefinition("Test");
            def.Properties.ShouldBeEmpty();
            def.Properties.Add("TestKey", "TestValue");
            def.Properties.ShouldHaveSingleItem().Key.ShouldBe("TestKey");
        }

        [Fact]
        public void This()
        {
            var def = new PermissionDefinition("Test");
            def.Properties.ShouldBeEmpty();
            def["TestKey"].ShouldBeNull();
            def["TestKey"] = "TestValue";
            def.Properties.ShouldHaveSingleItem().Key.ShouldBe("TestKey");
        }

        [Fact]
        public void AddPermission()
        {
            var def = new PermissionDefinition("Test");
            def.Children.ShouldBeEmpty();
            def.AddChild("TestPermission");
            def.Children.ShouldHaveSingleItem().FullName.ShouldBe("Test.TestPermission");
        }

        [Fact]
        public void GetPermissionsWithChildren()
        {
            var def = new PermissionDefinition("Test");
            def.Children.ShouldBeEmpty();
            def
                .AddChild("TestPermission", 
                    p => p.AddChild("TestChild"));
            def.Children.ShouldHaveSingleItem().Children.ShouldHaveSingleItem().FullName.ShouldBe("Test.TestPermission.TestChild");
        }
    }
}
