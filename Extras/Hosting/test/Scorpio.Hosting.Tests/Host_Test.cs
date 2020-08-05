using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;

using Moq;

using Shouldly;

using Xunit;

namespace Scorpio.Hosting.Tests
{
    public class Host_Test
    {
        [Fact]
        public void Test1()
        {
            var context = new HostBuilderContext(new Dictionary<object, object>());
            var services = new ServiceCollection();
            var mock = new Mock<IHostBuilder>();
            var factory = default(IServiceProviderFactory<IServiceCollection>);
            mock.Setup(b => b.UseServiceProviderFactory(It.IsAny<Func<HostBuilderContext, IServiceProviderFactory<IServiceCollection>>>()))
                .Callback<Func<HostBuilderContext, IServiceProviderFactory<IServiceCollection>>>(f => factory = f(context));
            mock.Object.AddScorpio<HosttingTestModule>();
            factory.CreateBuilder(services);
            services.AddSingleton<IHostLifetime, ConsoleLifetime>();
            services.AddSingleton<IHostApplicationLifetime, ApplicationLifetime>();
            services.ShouldContainSingleton(typeof(IBootstrapper), typeof(InternalBootstrapper));
            var serviceProvider = factory.CreateServiceProvider(services);
            var bootstrapper = serviceProvider.GetRequiredService<IBootstrapper>().ShouldBeOfType<InternalBootstrapper>();
            bootstrapper.ServiceProvider.ShouldBe(serviceProvider);
        }

        [Fact]
        public void Test2()
        {
            var context = new HostBuilderContext(new Dictionary<object, object>());
            var services = new ServiceCollection();
            var mock = new Mock<IHostBuilder>();
            var factory = default(IServiceProviderFactory<IServiceCollection>);
            mock.Setup(b => b.UseServiceProviderFactory(It.IsAny<Func<HostBuilderContext, IServiceProviderFactory<IServiceCollection>>>()))
                .Callback<Func<HostBuilderContext, IServiceProviderFactory<IServiceCollection>>>(f => factory = f(context));
            mock.Object.AddScorpio(typeof(HosttingTestModule));
            factory.CreateBuilder(services);
            services.AddSingleton<IHostLifetime, ConsoleLifetime>();
            services.AddSingleton<IHostApplicationLifetime, ApplicationLifetime>();
            services.ShouldContainSingleton(typeof(IBootstrapper), typeof(InternalBootstrapper));
            var serviceProvider = factory.CreateServiceProvider(services);
            var bootstrapper = serviceProvider.GetRequiredService<IBootstrapper>().ShouldBeOfType<InternalBootstrapper>();
            bootstrapper.ServiceProvider.ShouldBe(serviceProvider);
        }
    }
}
