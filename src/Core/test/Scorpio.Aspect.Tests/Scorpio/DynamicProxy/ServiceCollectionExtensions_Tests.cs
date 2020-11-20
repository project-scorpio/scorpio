
using Shouldly;

using Xunit;
namespace Scorpio.DynamicProxy
{
    public class ServiceCollectionExtensions_Tests : AspectIntegratedTest
    {
        [Fact]
        public void ConventionalRegistrar()
        {

            var service = GetService<IInterceptorTestService>();
            service.InterceptorInvoked.ShouldBeFalse();
            service.TestInvoked.ShouldBeFalse();
            service.Test();
            service.InterceptorInvoked.ShouldBeTrue();
            service.TestInvoked.ShouldBeTrue();
            var service2 = GetService<INonInterceptorTestService>();
            service2.InterceptorInvoked.ShouldBeFalse();
            service2.TestInvoked.ShouldBeFalse();
            service2.Test();
            service2.InterceptorInvoked.ShouldBeFalse();
            service2.TestInvoked.ShouldBeTrue();

        }
    }
}
