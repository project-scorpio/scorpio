using System;
using System.Collections.Generic;
using System.Text;

using Shouldly;

using Xunit;

namespace Scorpio.Authorization.Permissions
{
    public class PermissionGroupDefinition_Tests
    {
        [Fact]
        public void Name()
        {
            var def = new PermissionGroupDefinition("Test");
            def.Name.ShouldBe("Test");
            def.ToString().ShouldBe("[PermissionGroupDefinition Test]");
        }
        [Fact]
        public void DisplayName()
        {
            var def = new PermissionGroupDefinition("Test", "DisplayName");
            def.DisplayName.ShouldBe("DisplayName");
            Should.Throw<ArgumentNullException>(() => def.DisplayName = null);
            Should.NotThrow(() => def.DisplayName = "TestDisplay");
            def.DisplayName.ShouldBe("TestDisplay");
        }

        [Fact]
        public void Properties()
        {
            var def = new PermissionGroupDefinition("Test");
            def.Properties.ShouldBeEmpty();
            def.Properties.Add("TestKey", "TestValue");
            def.Properties.ShouldHaveSingleItem().Key.ShouldBe("TestKey");
        }

        [Fact]
        public void This()
        {
            var def = new PermissionGroupDefinition("Test");
            def.Properties.ShouldBeEmpty();
            def["TestKey"].ShouldBeNull();
            def["TestKey"] = "TestValue";
            def.Properties.ShouldHaveSingleItem().Key.ShouldBe("TestKey");
        }

        [Fact]
        public void AddPermission()
        {
            var def = new PermissionGroupDefinition("Test");
            def.Permissions.ShouldBeEmpty();
            def.AddPermission("TestPermission");
            def.Permissions.ShouldHaveSingleItem().Name.ShouldBe("TestPermission");
        }

        [Fact]
        public void GetPermissionsWithChildren()
        {
            var def = new PermissionGroupDefinition("Test");
            def.Permissions.ShouldBeEmpty();
            def
                .AddPermission("TestPermission", 
                    p => p.AddChild("TestChild"));
            def.GetPermissionsWithChildren().Count.ShouldBe(2);
        }
    }
}
