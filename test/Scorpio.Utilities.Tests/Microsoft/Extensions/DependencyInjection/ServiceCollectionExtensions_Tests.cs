

using Shouldly;

using Xunit;

namespace Microsoft.Extensions.DependencyInjection
{
    public class ServiceCollectionExtensions_Tests
    {
        [Fact]
        public void GetService()
        {
            var services = new ServiceCollection();
            services.AddScoped<IService1, Service1>();
            var serviceProvider = services.BuildServiceProvider();
            var service1 = new Service1();
            serviceProvider.GetService<IService1>(() => service1).ShouldNotBe(service1);
            var service2 = new Service2();
            serviceProvider.GetService<IService2>(() => service2).ShouldBe(service2);
        }

    }

    public interface IService1
    {

    }
    public interface IService2
    {

    }

    internal class Service1 : IService1
    {

    }

    internal class Service2 : IService2
    {
        public Service1 Service1 { get; set; }
    }


}
