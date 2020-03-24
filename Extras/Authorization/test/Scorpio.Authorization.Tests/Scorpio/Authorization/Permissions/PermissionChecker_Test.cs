using Scorpio.TestBase;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Shouldly;
namespace Scorpio.Authorization.Permissions
{
    public class PermissionChecker_Test: IntegratedTest<AuthorizationTestModule>
    {
        private IPermissionChecker _permissionChecker;
        public PermissionChecker_Test()
        {
            _permissionChecker = ServiceProvider.GetService<IPermissionChecker>();
        }

        [Fact]
        public void CheckAsync()
        {
            _permissionChecker.CheckAsync("Permission_Test_1").Result.ShouldBeTrue();
            _permissionChecker.CheckAsync("Permission_Test_2").Result.ShouldBeFalse();
            Should.Throw<PermissionNotFondException>(()=>_permissionChecker.CheckAsync("Permission_Test_3"));
        }
    }
}
