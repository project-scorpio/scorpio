using AspectCore.Extensions.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

using Shouldly;

using Xunit;
namespace Scorpio.DynamicProxy
{
    public class ServiceCollectionExtensions_Tests
    {
        [Fact]
        public void ConventionalRegistrar()
        {
            var services = new ServiceCollection();
            services.AddConventionalRegistrar<TestConventionalInterceptorRegistrar>();
            services.RegisterAssemblyByConvention();
            services.AddTransient<IInterceptorTestService, InterceptorTestService>();
            services.AddTransient<IInterceptorTestService2, InterceptorTestService2>();
            services.ShouldContainTransient(typeof(TestInterceptor));
            var serviceProvider = services.BuildServiceContextProvider();
            var service = serviceProvider.GetService<IInterceptorTestService>();
            service.InterceptorInvoked.ShouldBeFalse();
            service.TestInvoked.ShouldBeFalse();
            service.Test();
            service.InterceptorInvoked.ShouldBeTrue();
            service.TestInvoked.ShouldBeTrue();
            var service2 = serviceProvider.GetService<IInterceptorTestService2>();
            service2.InterceptorInvoked.ShouldBeFalse();
            service2.TestInvoked.ShouldBeFalse();
            service2.Test();
            service2.InterceptorInvoked.ShouldBeFalse();
            service2.TestInvoked.ShouldBeTrue();

        }
    }
}
