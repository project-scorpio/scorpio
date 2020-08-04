using System;
using System.Security.Principal;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Security;
using Scorpio.TestBase;

using Shouldly;

using Xunit;
namespace Scorpio.Authorization
{
    public class AuthorizationService_Test : IntegratedTest<AuthorizationTestModule>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ICurrentPrincipalAccessor _currentPrincipalAccessor;

        public AuthorizationService_Test()
        {
            _authorizationService = ServiceProvider.GetService<IAuthorizationService>();
            _currentPrincipalAccessor= ServiceProvider.GetService<ICurrentPrincipalAccessor>();
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
                    new string[] { "Permission_Test_3", "Permission_Test_1", "Permission_Test_2" }, true, ((Action)FakeMethod).Method))
                .ShouldNotThrow();
            _currentPrincipalAccessor.ShouldBeOfType<FakePrincipalAccessor>().SetPrincipal(null);
            _authorizationService.CheckAsync(
                new InvocationAuthorizationContext(
                    new string[] { }, false, null))
                .ShouldThrow<AuthorizationException>();
        }

        [AllowAnonymous]
        void FakeMethod()
        {
            // Method intentionally left empty.
        }

    }
}
