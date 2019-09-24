using System;
using Xunit;
using Shouldly;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;

namespace Scorpio.AspNetCore.Hosting.Tests
{
    public class Host_Test
    {
        [Fact]
        public void Test1()
        {
            var context = new WebHostBuilderContext();
            var services = new ServiceCollection();
            var applicationBuilder = new Mock<IApplicationBuilder>();
            var env = new Mock<Microsoft.AspNetCore.Hosting.IHostingEnvironment>();
            context.HostingEnvironment = env.Object;
            services.AddSingleton(env.Object);
            env.SetupGet(m => m.EnvironmentName).Returns("");
            var mock = new Mock<IWebHostBuilder>();
            mock.Setup(b => b.ConfigureServices(It.IsAny<Action<WebHostBuilderContext, IServiceCollection>>()))
                .Callback<Action<WebHostBuilderContext, IServiceCollection>>(a => a(context, services));
            mock.Object.UseBootstrapper<HosttingTestModule>();
            services.ShouldContainSingleton(typeof(IStartup), typeof(Startup));
            var startup = services.BuildServiceProvider().GetRequiredService<IStartup>().ShouldBeOfType<Startup>();
            startup.ConfigureServices(services);
            var serviceProvider = startup.CreateServiceProvider(services);
            applicationBuilder.SetupGet(m => m.ApplicationServices).Returns(serviceProvider);
            startup.Configure(applicationBuilder.Object);
            var bootstrapper= serviceProvider.GetRequiredService<IBootstrapper>().ShouldBeOfType<InternalBootstrapper>();
            bootstrapper.ServiceProvider.ShouldBe(serviceProvider);
        }
    }
}
