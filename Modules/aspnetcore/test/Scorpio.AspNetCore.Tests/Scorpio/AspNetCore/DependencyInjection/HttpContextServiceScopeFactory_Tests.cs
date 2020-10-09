
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using NSubstitute;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.DependencyInjection
{
    public class HttpContextServiceScopeFactory_Tests
    {
        [Fact]
        public void CreateScope_1()
        {
            var accessor = Substitute.For<IHttpContextAccessor>();
            var scopeFactory = Substitute.For<IServiceScopeFactory>();
            var factory = new HttpContextServiceScopeFactory(accessor, scopeFactory);
            using (var scope = Should.NotThrow(() => factory.CreateScope()))
            {
                scope.ServiceProvider.ShouldBe(accessor.HttpContext.RequestServices);
            }
        }

        [Fact]
        public void CreateScope_2()
        {
            var accessor = Substitute.For<IHttpContextAccessor>();
            accessor.HttpContext.Returns((HttpContext)null);
            var scopeFactory = Substitute.For<IServiceScopeFactory>();
            var serviceScope = Substitute.For<IServiceScope>();
            scopeFactory.CreateScope().Returns(serviceScope);
            var factory = new HttpContextServiceScopeFactory(accessor, scopeFactory);
            Should.NotThrow(() => factory.CreateScope()).ShouldBe(serviceScope);
        }

    }
}
