using System;
using System.Collections.Generic;
using System.Text;

using Autofac.Core;

using Scorpio.DynamicProxy.TestClasses;

using Shouldly;

using Xunit;

namespace Autofac.Extensions.DependencyInjection
{
    public class AutofacServiceProvider_Tests
    {
        [Fact]
        public void GetService()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TestProxiedService>().As<IProxiedService>();
            var container = builder.Build();
            var serviceProvider = new AutofacServiceProvider(container);
            Should.NotThrow(() => serviceProvider.GetService(typeof(TestProxiedService))).ShouldBeNull();
            Should.NotThrow(() => serviceProvider.GetService(typeof(IProxiedService))).ShouldNotBeNull();
        }

        [Fact]
        public void GetRequireService()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TestProxiedService>().As<IProxiedService>();
            var container = builder.Build();
            var serviceProvider = new AutofacServiceProvider(container);
            Should.Throw<DependencyResolutionException>(() => serviceProvider.GetRequiredService(typeof(TestProxiedService)));
            Should.NotThrow(() => serviceProvider.GetRequiredService(typeof(IProxiedService))).ShouldNotBeNull();
        }

        [Fact]
        public void Dispose()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TestProxiedService>().As<IProxiedService>();
            var container = builder.Build();
            var serviceProvider = new AutofacServiceProvider(container);
            Should.NotThrow(() => serviceProvider.GetRequiredService(typeof(IProxiedService))).ShouldNotBeNull();
            serviceProvider.Dispose();
            Should.Throw<ObjectDisposedException>(() => serviceProvider.GetRequiredService(typeof(IProxiedService)));
        }
    }
}
