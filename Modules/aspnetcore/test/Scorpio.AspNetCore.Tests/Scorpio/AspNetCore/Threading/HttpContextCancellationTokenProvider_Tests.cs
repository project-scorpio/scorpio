using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Http;

using NSubstitute;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.Threading
{
    public class HttpContextCancellationTokenProvider_Tests
    {
        [Fact]
        public void Token()
        {
            var accessor = Substitute.For<IHttpContextAccessor>();
            accessor.HttpContext.Returns(new DefaultHttpContext());
            var provider = new HttpContextCancellationTokenProvider(accessor);
            provider.Token.ShouldBe(accessor.HttpContext.RequestAborted);
        }
    }
}
