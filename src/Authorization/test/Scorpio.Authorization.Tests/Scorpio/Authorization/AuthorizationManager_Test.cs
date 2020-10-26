using Microsoft.Extensions.DependencyInjection;

using Scorpio.TestBase;

using Shouldly;

using Xunit;

namespace Scorpio.Authorization
{
    public class AuthorizationManager_Test : IntegratedTest<AuthorizationTestModule>
    {
        private readonly IAuthorizationManager _authorizationManager;

        public AuthorizationManager_Test()
        {
            _authorizationManager = ServiceProvider.GetService<IAuthorizationManager>();
        }

        [Fact]
        public void AuthorizeAsync()
        {
            _authorizationManager.AuthorizeAsync(false, "Permission_Test_1").ShouldNotThrow();
            _authorizationManager.AuthorizeAsync(false).ShouldNotThrow();
            _authorizationManager.AuthorizeAsync(true).ShouldNotThrow();
            _authorizationManager.AuthorizeAsync(false, "Permission_Test_1.Permission_Test_2").ShouldThrow<AuthorizationException>();
            _authorizationManager.AuthorizeAsync(false, "Permission_Test_1", "Permission_Test_1.Permission_Test_2").ShouldNotThrow();
            _authorizationManager.AuthorizeAsync(true, "Permission_Test_1", "Permission_Test_1.Permission_Test_2").ShouldThrow<AuthorizationException>();
        }
    }
}
