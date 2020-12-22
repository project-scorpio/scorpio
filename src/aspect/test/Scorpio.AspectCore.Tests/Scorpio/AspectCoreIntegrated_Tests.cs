
using Microsoft.Extensions.DependencyInjection;

using Scorpio.DependencyInjection.TestClasses;
using Scorpio.DynamicProxy;
using Scorpio.DynamicProxy.TestClasses;

using Shouldly;

using Xunit;

namespace Scorpio
{
    public class AspectCoreIntegrated_Tests:AspectTestBase
    {
        [Fact]
        public void Proxy()
        {
            var service=ServiceProvider.GetService<IProxiedService>();
            service.IsProxy().ShouldBeTrue();
            service.UnProxy().ShouldBeOfType<TestProxiedService>();
        }

        [Fact]
        public void PropertyInject()
        {
            ServiceProvider.GetService<PropertyInjectionService>().PropertyService.ShouldNotBeNull();
            ServiceProvider.GetService<NonPropertyInjectionService>().PropertyService.ShouldBeNull();
            ServiceProvider.GetService<ReadOnlyPropertyInjectionService>().PropertyService.ShouldBeNull();
        }
    }
}
