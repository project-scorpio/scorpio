using System.Collections.Generic;

using Microsoft.AspNetCore.Authorization;

using NSubstitute;
using NSubstitute.ExceptionExtensions;

using Scorpio.Authorization.Permissions;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.Authorization
{
    public class PermissionRequirementHandler_Tests
    {
        [Fact]
        public void HandleRequirementAsync_Failed()
        {
            var checker = Substitute.For<IPermissionChecker>();
            var handler = new PermissionRequirementHandler(checker);
            var requirement = new PermissionRequirement("test");
            var context = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, new System.Security.Claims.ClaimsPrincipal(), null);
            Should.NotThrow(() => handler.HandleAsync(context));
            context.HasFailed.ShouldBeTrue();
            context.HasSucceeded.ShouldBeFalse();
        }

        [Fact]
        public void HandleRequirementAsync_Ex()
        {
            var checker = Substitute.For<IPermissionChecker>();
            checker.CheckAsync(default, default).ThrowsForAnyArgs(c => new PermissionNotFondException(c.Arg<string>()));
            var handler = new PermissionRequirementHandler(checker);
            var requirement = new PermissionRequirement("test");
            var context = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, new System.Security.Claims.ClaimsPrincipal(), null);
            Should.NotThrow(() => handler.HandleAsync(context));
            context.HasFailed.ShouldBeTrue();
            context.HasSucceeded.ShouldBeFalse();
        }

        [Fact]
        public void HandleRequirementAsync_Successed()
        {
            var checker = Substitute.For<IPermissionChecker>();
            checker.CheckAsync(default, default).ReturnsForAnyArgs(true);
            var handler = new PermissionRequirementHandler(checker);
            var requirement = new PermissionRequirement("test");
            var context = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, new System.Security.Claims.ClaimsPrincipal(), null);
            Should.NotThrow(() => handler.HandleAsync(context));
            context.HasFailed.ShouldBeFalse();
            context.HasSucceeded.ShouldBeTrue();
        }
    }
}
