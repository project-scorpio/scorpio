
using Microsoft.Extensions.DependencyInjection;

using NSubstitute;

using Scorpio.DependencyInjection.TestClasses;
using Scorpio.DynamicProxy.TestClasses;
using Scorpio.Modularity;

using Shouldly;

using Xunit;

namespace Autofac.Extensions.DependencyInjection
{
    public class AutofacRegistration_Tests
    {
        [Fact]
        public void Register_Type()
        {
            var services = new ServiceCollection();
            services.AddTransient<IProxiedService, TestProxiedService>();
            services.AddSingleton(Substitute.For<IModuleContainer>());
            var builder = Substitute.For<ContainerBuilder>();
            builder.Populate(services);
            builder.Build().Resolve<IProxiedService>().ShouldBeOfType<TestProxiedService>();
        }

        [Fact]
        public void Register_Instance()
        {
            var services = new ServiceCollection();
            services.AddTransient<IProxiedService, TestProxiedService>();
            var module = Substitute.For<IModuleContainer>();
            services.AddSingleton(module);
            var builder = Substitute.For<ContainerBuilder>();
            builder.Populate(services);
            builder.Build().Resolve<IModuleContainer>().ShouldBe(module);
        }


        [Fact]
        public void Register_Generic()
        {
            var services = new ServiceCollection();
            services.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddSingleton(Substitute.For<IModuleContainer>());
            var builder = Substitute.For<ContainerBuilder>();
            builder.Populate(services);
            builder.Build().Resolve<IGenericService<string>>().ShouldBeOfType<GenericService<string>>();
        }

        [Fact]
        public void Register_Factory()
        {
            var services = new ServiceCollection();
            services.AddTransient<IProxiedService>(sp => new TestProxiedService());
            services.AddSingleton(Substitute.For<IModuleContainer>());
            var builder = Substitute.For<ContainerBuilder>();
            builder.Populate(services);
            builder.Build().Resolve<IProxiedService>().ShouldBeOfType<TestProxiedService>();
        }

        [Fact]
        public void Register_LifeTime_Singleton()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IProxiedService, TestProxiedService>();
            services.AddSingleton(Substitute.For<IModuleContainer>());
            var builder = Substitute.For<ContainerBuilder>();
            builder.Populate(services);
            var provider = builder.Build();
            var exp = provider.Resolve<IProxiedService>().ShouldBeOfType<TestProxiedService>();
            var act = provider.Resolve<IProxiedService>().ShouldBeOfType<TestProxiedService>();
            act.ShouldBe(exp);
        }

        [Fact]
        public void Register_LifeTime_Transient()
        {
            var services = new ServiceCollection();
            services.AddTransient<IProxiedService, TestProxiedService>();
            services.AddSingleton(Substitute.For<IModuleContainer>());
            var builder = Substitute.For<ContainerBuilder>();
            builder.Populate(services);
            var provider = builder.Build();
            var exp = provider.Resolve<IProxiedService>().ShouldBeOfType<TestProxiedService>();
            var act = provider.Resolve<IProxiedService>().ShouldBeOfType<TestProxiedService>();
            act.ShouldNotBe(exp);
        }

        [Fact]
        public void Register_LifeTime_Scoped()
        {
            var services = new ServiceCollection();
            services.AddScoped<IProxiedService, TestProxiedService>();
            services.AddSingleton(Substitute.For<IModuleContainer>());
            var builder = Substitute.For<ContainerBuilder>();
            builder.Populate(services);
            var provider = builder.Build();
            var exp = provider.Resolve<IProxiedService>().ShouldBeOfType<TestProxiedService>();
            using (var scope= provider.BeginLifetimeScope())
            {
                var act = scope.Resolve<IProxiedService>().ShouldBeOfType<TestProxiedService>();
                act.ShouldNotBe(exp);
                scope.Resolve<IProxiedService>().ShouldBeOfType<TestProxiedService>().ShouldBe(act);
            }
        }
    }
}
