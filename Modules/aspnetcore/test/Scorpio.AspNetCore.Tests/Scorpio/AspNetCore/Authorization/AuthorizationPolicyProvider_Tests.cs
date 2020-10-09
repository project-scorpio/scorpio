
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Options;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.Authorization
{
    public class AuthorizationPolicyProvider_Tests
    {
        [Fact]
        public void GetPolicyAsync()
        {
            var provider = new AuthorizationPolicyProvider(new OptionsWrapper<AuthorizationOptions>(new AuthorizationOptions()));
            Should.NotThrow(() => provider.GetPolicyAsync("test")).Requirements.ShouldHaveSingleItem().ShouldBeOfType<PermissionRequirement>();
        }

        [Fact]
        public void GetPolicyAsync_2()
        {
            var opt = new AuthorizationOptions();
            opt.AddPolicy("test", p => p.Requirements.Add(new NameAuthorizationRequirement("test")));
            var provider = new AuthorizationPolicyProvider(new OptionsWrapper<AuthorizationOptions>(opt));
            Should.NotThrow(() => provider.GetPolicyAsync("test")).Requirements.ShouldHaveSingleItem().ShouldBeOfType<NameAuthorizationRequirement>();
        }
    }
}
