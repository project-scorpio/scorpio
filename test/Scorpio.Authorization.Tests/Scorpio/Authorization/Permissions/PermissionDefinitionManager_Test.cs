using Microsoft.Extensions.DependencyInjection;

using Scorpio.TestBase;

using Shouldly;

using Xunit;
namespace Scorpio.Authorization.Permissions
{
    public class PermissionDefinitionManager_Test : IntegratedTest<AuthorizationTestModule>
    {
        private readonly IPermissionDefinitionManager _permissionDefinitionManager;
        public PermissionDefinitionManager_Test() => _permissionDefinitionManager = ServiceProvider.GetService<IPermissionDefinitionManager>();

        [Fact]
        public void Get()
        {
            _permissionDefinitionManager.Get("Permission_Test_1").ShouldNotBeNull();
            Should.Throw<PermissionNotFondException>(() => _permissionDefinitionManager.Get("NotFoundPermission"));
        }

        [Fact]
        public void GetOrNull()
        {
            _permissionDefinitionManager.GetOrNull("Permission_Test_1").ShouldNotBeNull();
            _permissionDefinitionManager.GetOrNull("NotFoundPermission").ShouldBeNull();
        }

        [Fact]
        public void GetPermissions()
        {
            _permissionDefinitionManager.GetPermissions().Count.ShouldBe(3);
            _permissionDefinitionManager.GetPermissions()[0].Name.ShouldBe("Permission_Test_1");
            _permissionDefinitionManager.GetPermissions()[1].Name.ShouldBe("Permission_Test_2");
        }

        [Fact]
        public void GetGroups()
        {
            _permissionDefinitionManager.GetGroups().Count.ShouldBe(1);
            _permissionDefinitionManager.GetGroups()[0].Name.ShouldBe("PermissionGroup_Test_1");
        }
    }
}
