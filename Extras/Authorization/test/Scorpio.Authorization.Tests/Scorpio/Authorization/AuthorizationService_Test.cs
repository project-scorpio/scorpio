using Scorpio.TestBase;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Shouldly;
namespace Scorpio.Authorization
{
    public class AuthorizationService_Test : IntegratedTest<AuthorizationTestModule>
    {
        private IAuthorizationService _authorizationService;
        public AuthorizationService_Test()
        {
            _authorizationService = ServiceProvider.GetService<IAuthorizationService>();
        }

        [Fact]
        public void CheckAsync()
        {
            _authorizationService.CheckAsync(
                new InvocationAuthorizationContext(
                    new string[] { "Permission_Test_1" }, false, null))
                .ShouldNotThrow();
            _authorizationService.CheckAsync(
                new InvocationAuthorizationContext(
                    new string[] { }, false, null))
                .ShouldNotThrow();
            _authorizationService.CheckAsync(
                new InvocationAuthorizationContext(
                    new string[] { "Permission_Test_2" }, false, null))
                .ShouldThrow<AuthorizationException>();
            _authorizationService.CheckAsync(
                new InvocationAuthorizationContext(
                    new string[] { "Permission_Test_1", "Permission_Test_2" }, false, null))
                .ShouldNotThrow();
            _authorizationService.CheckAsync(
                new InvocationAuthorizationContext(
                    new string[] { "Permission_Test_1", "Permission_Test_2" }, true, null))
                .ShouldThrow<AuthorizationException>();
            _authorizationService.CheckAsync(
                new InvocationAuthorizationContext(
                    new string[] { "Permission_Test_3", "Permission_Test_1", "Permission_Test_2" }, true, null))
                .ShouldThrow<Permissions.PermissionNotFondException>();
            _authorizationService.CheckAsync(
                new InvocationAuthorizationContext(
                    new string[] { "Permission_Test_3", "Permission_Test_1", "Permission_Test_2" }, true, this.GetType().GetMethod(nameof(FakeMethod), System.Reflection.BindingFlags.NonPublic| System.Reflection.BindingFlags.Instance)))
                .ShouldNotThrow();
        }

        [AllowAnonymous]
        void FakeMethod()
        {

        }

    }
}
