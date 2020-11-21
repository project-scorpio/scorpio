
using AspectCore.DynamicProxy;

using Shouldly;

using Xunit;

namespace Scorpio.DynamicProxy
{
    public class ProxyHelper_Tests : AspectIntegratedTest
    {
        [Fact]
        public void UnProxy()
        {
            var service = GetService<IInterceptorTestService>();
            var service2 = GetService<INonInterceptorTestService>();
            service.IsProxy().ShouldBeTrue();
            service2.IsProxy().ShouldBeFalse();
            service.ShouldNotBeOfType<InterceptorTestService>();
            service.UnProxy().ShouldBeOfType<InterceptorTestService>();
            service2.ShouldBeOfType<NonInterceptorTestService>();
            service2.UnProxy().ShouldBeOfType<NonInterceptorTestService>();
        }
    }
}
