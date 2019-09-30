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
            mock.Setup(b => b.ConfigureServices(It.IsAny<Action<HostBuilderContext, IServiceCollection>>()))
                .Callback<Action<HostBuilderContext, IServiceCollection>>(a => a(context, services));
            mock.Setup(b => b.UseServiceProviderFactory(It.IsAny<IServiceProviderFactory<IServiceCollection>>()))
                .Callback<IServiceProviderFactory<IServiceCollection>>(f => factory = f);
           var bootstrapper= mock.Object.AddBootstrapper<HosttingTestModule>();
            services.ShouldContainSingleton(typeof(IBootstrapper),typeof(InternalBootstrapper));
           var serviceProvider= factory.CreateServiceProvider(services);
            bootstrapper.ServiceProvider.ShouldBe(serviceProvider);
        }
    }
}
