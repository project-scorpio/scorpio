
using Microsoft.AspNetCore.Http;

using NSubstitute;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.Security.Claims
{
    public class HttpContextCurrentPrincipalAccessor_Tests
    {
        [Fact]
        public void Principal()
        {
            var contextAccessor = Substitute.For<IHttpContextAccessor>();
            contextAccessor.HttpContext.Returns(new DefaultHttpContext());
            var accessor = new HttpContextCurrentPrincipalAccessor(contextAccessor);
            accessor.Principal.ShouldBe(contextAccessor.HttpContext.User);
        }
    }
}
