using System;

using Scorpio.Conventional;
using Scorpio.DependencyInjection;
using Scorpio.DependencyInjection.Conventional;

using Shouldly;

using Xunit;

namespace Microsoft.Extensions.DependencyInjection
{
    public class ServiceCollectionExtensions_Tests
    {
        [Fact]
        public void RegisterAssembly_1()
        {
            var services = new ServiceCollection();
            services.DoConventionalAction<ConventionalDependencyAction>(typeof(ServiceCollectionExtensions_Tests).Assembly.GetTypes(), config =>
            {
                config.Where(t => t.Name == nameof(Service1)).AsDefault();
                config.Where(t => t.Name == nameof(Service2)).AsDefault();
            });
            services.ShouldNotContainService(typeof(IService1), typeof(Service2));
            services.ShouldContainTransient(typeof(IService1), typeof(Service1));
            services.ShouldContainTransient(typeof(Service1));
            services.ShouldNotContainService(typeof(IService2), typeof(Service1));
            services.ShouldContainTransient(typeof(IService2), typeof(Service2));
        }
        [Fact]
        public void RegisterAssembly_2()
        {
            var services = new ServiceCollection();
            services.DoConventionalAction<ConventionalDependencyAction>(typeof(ServiceCollectionExtensions_Tests).Assembly.GetTypes(), config =>
            {
                config.Where(t => t.Name == nameof(Service1)).AsSelf();
            });
            services.ShouldNotContainService(typeof(IService1));
            services.ShouldContainTransient(typeof(Service1));
            services.ShouldNotContainService(typeof(IService2));
        }

        [Fact]
        public void RegisterAssembly_3()
        {
            var services = new ServiceCollection();
            services.DoConventionalAction<ConventionalDependencyAction>(typeof(ServiceCollectionExtensions_Tests).Assembly.GetTypes(), config =>
            {
                config.Where(t => t.Name == nameof(Service1)).As<IService2>().Lifetime(ServiceLifetime.Singleton);
                config.Where(t => t.Name == nameof(Service1)).As<IService1>().Lifetime(ServiceLifetime.Transient);
            });
            services.ShouldContainTransient(typeof(IService1), typeof(Service1));
            services.ShouldNotContainService(typeof(Service1));
            services.ShouldContainSingleton(typeof(IService2), typeof(Service1));
            services.ShouldNotContainService(typeof(IService3));
        }

        [Fact]
        public void RegisterAssembly_4()
        {
            var services = new ServiceCollection();
            services.DoConventionalAction<ConventionalDependencyAction>(typeof(ServiceCollectionExtensions_Tests).Assembly.GetTypes(), config =>
            {
                config.Where(t => t.Name == nameof(ExposeService)).AsExposeService();
            });
            services.ShouldContainSingleton(typeof(IExposeService), typeof(ExposeService));
        }

        [Fact]
        public void GetSingletonInstanceOrNull()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IService1>(new Service1());
            services.GetSingletonInstanceOrNull<IService1>().ShouldBeOfType<Service1>().ShouldNotBeNull();
            services.GetSingletonInstanceOrNull<IService2>().ShouldBeNull();
        }

        [Fact]
        public void GetSingletonInstance()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IService1>(new Service1());
            services.GetSingletonInstance<IService1>().ShouldBeOfType<Service1>().ShouldNotBeNull();
            Should.Throw<InvalidOperationException>(() => services.GetSingletonInstance<IService2>());
        }
    }

    public interface IService1
    {

    }
    public interface IService2
    {

    }

    public interface IExposeService
    {

    }

    public interface IService3
    {

    }

    class Service1 : IService1, IService2, IService3
    {

    }

    class Service2 : IService1, IService2
    {

    }

    [ExposeServices(typeof(IExposeService), ServiceLifetime = ServiceLifetime.Singleton)]
    class ExposeService : IExposeService
    {

    }
}
