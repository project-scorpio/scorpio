using System;

using Castle.DynamicProxy;

using NSubstitute;

using Scorpio.DependencyInjection.TestClasses;

using Shouldly;

using Xunit;

namespace Scorpio.DynamicProxy
{
    public class AutofacProxyTargetProvider_Tests
    {
        [Fact]
        public void GetTarget()
        {
            var proxyGenerator = new ProxyGenerator();
            var service = new PropertyInjectionService();
            var provider = new AutofacProxyTargetProvider();
            Should.NotThrow(() => provider.GetTarget(null)).ShouldBe(null);
            Should.NotThrow(() => provider.GetTarget(service)).ShouldBe(null);
            var serviceProvider=Substitute.For<IServiceProvider>();
            Should.NotThrow(() => provider.GetTarget(proxyGenerator.CreateInterfaceProxyWithTarget(serviceProvider))).ShouldBe(serviceProvider);

        }

         [Fact]
        public void IsProxy()
        {
            var proxyGenerator = new ProxyGenerator();
            var service = new PropertyInjectionService();
            var provider = new AutofacProxyTargetProvider();
            Should.NotThrow(() => provider.IsProxy(null)).ShouldBeFalse();
            Should.NotThrow(() => provider.IsProxy(service)).ShouldBeFalse();
            var serviceProvider=Substitute.For<IServiceProvider>();
            Should.NotThrow(() => provider.IsProxy(proxyGenerator.CreateInterfaceProxyWithTarget(serviceProvider))).ShouldBeTrue();

        }
    }
}
