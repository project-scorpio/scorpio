
using Microsoft.Extensions.DependencyInjection;

using NSubstitute;

using Shouldly;

using Xunit;

namespace Scorpio
{
    public class ServiceFactoryAdapter_Tests
    {
        [Fact]
        public void CreateBuilder()
        {
            var services = new ServiceCollection();
            var factory = Substitute.For<IServiceProviderFactory<IServiceCollection>>();
            factory.CreateBuilder(services).Returns(services);
            var adapter = new ServiceFactoryAdapter<IServiceCollection>(factory);
            adapter.CreateBuilder(services).ShouldBe(services);
            factory.Received(1).CreateBuilder(services).ShouldBe(services);
        }
        [Fact]
        public void CreateServiceProvider()
        {
            var services = new ServiceCollection();
            var factory = Substitute.For<IServiceProviderFactory<IServiceCollection>>();
            factory.CreateBuilder(services).Returns(services);
            var adapter = new ServiceFactoryAdapter<IServiceCollection>(factory);
            adapter.CreateBuilder(services).ShouldBe(services);
            adapter.CreateServiceProvider(services).ShouldNotBeNull();
            factory.Received(1).CreateBuilder(services).ShouldBe(services);
        }
    }
}
