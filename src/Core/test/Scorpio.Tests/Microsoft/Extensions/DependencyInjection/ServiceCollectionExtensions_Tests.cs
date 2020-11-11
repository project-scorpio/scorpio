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
            services.RegisterConventionalDependencyInject(typeof(ServiceCollectionExtensions_Tests).Assembly.GetTypes(), config =>
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
            services.RegisterConventionalDependencyInject(typeof(ServiceCollectionExtensions_Tests).Assembly.GetTypes(), config => config.Where(t => t.Name == nameof(Service1)).AsSelf());
            services.ShouldNotContainService(typeof(IService1));
            services.ShouldContainTransient(typeof(Service1));
            services.ShouldNotContainService(typeof(IService2));
        }

        [Fact]
        public void RegisterAssembly_3()
        {
            var services = new ServiceCollection();
            services.RegisterConventionalDependencyInject(typeof(ServiceCollectionExtensions_Tests).Assembly.GetTypes(), config =>
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
            services.RegisterConventionalDependencyInject(typeof(ServiceCollectionExtensions_Tests).Assembly.GetTypes(), config => config.Where(t => t.Name == nameof(Service1)).AsAll().Lifetime(ServiceLifetime.Transient));
            services.ShouldContainTransient(typeof(IService1), typeof(Service1));
            services.ShouldContainTransient(typeof(Service1), typeof(Service1));
            services.ShouldContainTransient(typeof(IService2), typeof(Service1));
            services.ShouldContainTransient(typeof(IService3), typeof(Service1));
            services.ShouldContainTransient(typeof(IService4), typeof(Service1));
        }


        [Fact]
        public void RegisterAssembly_5()
        {
            var services = new ServiceCollection();
            services.RegisterConventionalDependencyInject(typeof(ServiceCollectionExtensions_Tests).Assembly.GetTypes(), config => config.Where(t => t.Name == nameof(ExposeService)).AsExposeService());
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
        public void GetSingletonInstanceOrAdd()
        {
            var services = new ServiceCollection();
            services.GetSingletonInstanceOrAdd<IService1>(new Service1()).ShouldBeOfType<Service1>().ShouldNotBeNull();
            services.GetSingletonInstanceOrAdd<IService1>(new Service2()).ShouldBeOfType<Service1>().ShouldNotBeNull();
        }

        [Fact]
        public void GetSingletonInstanceOrAdd_T()
        {
            var services = new ServiceCollection();
            services.GetSingletonInstanceOrAdd<IService1>(s => new Service1()).ShouldBeOfType<Service1>().ShouldNotBeNull();
            services.GetSingletonInstanceOrAdd<IService1>(s => new Service2()).ShouldBeOfType<Service1>().ShouldNotBeNull();
        }

        [Fact]
        public void GetSingletonInstance()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IService1>(new Service1());
            services.GetSingletonInstance<IService1>().ShouldBeOfType<Service1>().ShouldNotBeNull();
            Should.Throw<InvalidOperationException>(() => services.GetSingletonInstance<IService2>());
        }

        [Fact]
        public void ReplaceSingleton()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IService1>(new Service1());
            services.ShouldContainSingleton(typeof(IService1), typeof(Service1));
            services.ShouldNotContainService(typeof(IService1), typeof(Service2));
            services.ReplaceSingleton<IService1>(new Service2());
            services.ShouldContainSingleton(typeof(IService1), typeof(Service2));
            services.ShouldNotContainService(typeof(IService1), typeof(Service1));
        }

        [Fact]
        public void ReplaceSingleton_T()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IService1>(new Service1());
            services.ShouldContainSingleton(typeof(IService1), typeof(Service1));
            services.ShouldNotContainService(typeof(IService1), typeof(Service2));
            services.ReplaceSingleton<IService1, Service2>();
            services.ShouldContainSingleton(typeof(IService1), typeof(Service2));
            services.ShouldNotContainService(typeof(IService1), typeof(Service1));
        }

        [Fact]
        public void ReplaceTransient()
        {
            var services = new ServiceCollection();
            services.AddTransient<IService1, Service1>();
            services.ShouldContainTransient(typeof(IService1), typeof(Service1));
            services.ShouldNotContainService(typeof(IService1), typeof(Service2));
            services.ReplaceTransient<IService1, Service2>();
            services.ShouldContainTransient(typeof(IService1), typeof(Service2));
            services.ShouldNotContainService(typeof(IService1), typeof(Service1));
        }

        [Fact]
        public void ShouldContainScoped()
        {
            var services = new ServiceCollection();
            services.AddScoped<IService1, Service1>();
            services.ShouldContainScoped(typeof(IService1), typeof(Service1));
            services.ShouldNotContainService(typeof(IService1), typeof(Service2));
            services.ReplaceScoped<IService1, Service2>();
            services.ShouldContainScoped(typeof(IService1), typeof(Service2));
            services.ShouldNotContainService(typeof(IService1), typeof(Service1));
        }



        [Fact]
        public void ReplaceEnumerable()
        {
            var services = new ServiceCollection();
            services.AddTransient<IService1, Service1>();
            services.ShouldContainTransient(typeof(IService1), typeof(Service1));
            services.ShouldNotContainService(typeof(IService1), typeof(Service2));
            Should.Throw<ArgumentException>(() => services.ReplaceEnumerable<IService1, IService1, Service2>());
            services.ReplaceEnumerable<IService1, Service1, Service2>();
            services.ShouldContainTransient(typeof(IService1), typeof(Service2));
            services.ShouldNotContainService(typeof(IService1), typeof(Service1));

        }

        [Fact]
        public void RemoveEnumerable()
        {
            var services = new ServiceCollection();
            services.AddTransient<IService1, Service1>();
            services.ShouldContainTransient(typeof(IService1), typeof(Service1));
            Should.Throw<ArgumentException>(() => services.RemoveEnumerable<IService1, IService1>());
            services.RemoveEnumerable<IService1, Service2>();
            services.ShouldContainTransient(typeof(IService1), typeof(Service1));
            services.RemoveEnumerable<IService1, Service1>();
            services.ShouldNotContainService(typeof(IService1));
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

    public interface IService4
    {

    }
    public interface IService5
    {

    }

    internal class Service1 : IService1, IService2, IService3, IService4
    {

    }

    internal class Service2 : IService1, IService2
    {

    }

    [ExposeServices(typeof(IExposeService), ServiceLifetime = ServiceLifetime.Singleton)]
    internal class ExposeService : IExposeService
    {

    }
}
