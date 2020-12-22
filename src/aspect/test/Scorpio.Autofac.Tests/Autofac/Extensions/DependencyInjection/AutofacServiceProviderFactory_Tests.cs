using System;

using Microsoft.Extensions.DependencyInjection;

using NSubstitute;

using Scorpio.DynamicProxy.TestClasses;
using Scorpio.Modularity;

using Shouldly;

using Xunit;

namespace Autofac.Extensions.DependencyInjection
{
    public class AutofacServiceProviderFactory_Tests
    {
        [Fact]
        public void CreateBuilder()
        {
            var services = new ServiceCollection();
            services.AddSingleton(Substitute.For<IModuleContainer>());
            services.AddTransient<IProxiedService, TestProxiedService>();
            var factory = new AutofacServiceProviderFactory();
            Should.Throw<ArgumentNullException>(() => factory.CreateBuilder(null));
            factory.CreateBuilder(services).Build().Resolve<IProxiedService>().ShouldBeOfType<TestProxiedService>();
        }

        [Fact]
        public void CreateServiceProvider()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TestProxiedService>().As<IProxiedService>();
            var factory = new AutofacServiceProviderFactory();
            Should.Throw<ArgumentNullException>(() => factory.CreateServiceProvider(null));
            factory.CreateServiceProvider(builder).GetService<IProxiedService>().ShouldBeOfType<TestProxiedService>();
        }
    }
}
