using Microsoft.Extensions.Hosting;
using System;
using Xunit;
using Shouldly;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

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
            services.ShouldContainSingleton(typeof(IBootstrapper), typeof(InternalBootstrapper));
            var serviceProvider = factory.CreateServiceProvider(services);
            var bootstrapper = serviceProvider.GetRequiredService<IBootstrapper>().ShouldBeOfType<InternalBootstrapper>();
            bootstrapper.ServiceProvider.ShouldBe(serviceProvider);
        }
    }
}
