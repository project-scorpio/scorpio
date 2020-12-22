using System;

using AspectCore.DynamicProxy;
using AspectCore.Extensions.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

using NSubstitute;

using Scorpio.DependencyInjection.TestClasses;

using Shouldly;

using Xunit;

namespace Scorpio.DynamicProxy
{
    public class AspectCoreProxyTargetProvider_Tests
    {
        [Fact]
        public void GetTarget()
        {
            var proxyGenerator = new ServiceCollection().BuildDynamicProxyProvider().GetRequiredService<IProxyGenerator>();
            var service = new PropertyInjectionService();
            var provider = new AspectCoreProxyTargetProvider();
            Should.NotThrow(() => provider.GetTarget(null)).ShouldBe(null);
            Should.NotThrow(() => provider.GetTarget(service)).ShouldBe(null);
            var serviceProvider=Substitute.For<IServiceProvider>();
            Should.NotThrow(() => provider.GetTarget(proxyGenerator.CreateInterfaceProxy<IServiceProvider>(serviceProvider))).ShouldBe(serviceProvider);

        }

        [Fact]
        public void IsProxy()
        {
            var proxyGenerator = new ServiceCollection().BuildDynamicProxyProvider().GetRequiredService<IProxyGenerator>();
            var service = new PropertyInjectionService();
            var provider = new AspectCoreProxyTargetProvider();
            Should.NotThrow(() => provider.IsProxy(null)).ShouldBeFalse();
            Should.NotThrow(() => provider.IsProxy(service)).ShouldBeFalse();
            var serviceProvider=Substitute.For<IServiceProvider>();
            Should.NotThrow(() => provider.IsProxy(proxyGenerator.CreateInterfaceProxy<IServiceProvider>(serviceProvider))).ShouldBeTrue();

        }
    }
}
