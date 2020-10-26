using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Conventional;
using Scorpio.DependencyInjection;

using Shouldly;

using Xunit;

namespace Scorpio.Modularity
{
    public class ConfigureServicesContextExtensions_Tests
    {
        [Fact]
        public void AddConventionalRegistrar()
        {

            var context = new ConfigureServicesContext(null, new ServiceCollection(), null);
            context.AddConventionalRegistrar(new EmptyConventionalDependencyRegistrar());
            context.Services.GetSingletonInstanceOrNull<ConventionalRegistrarList>().Any(c => c.GetType() == typeof(EmptyConventionalDependencyRegistrar)).ShouldBeTrue();

        }
        [Fact]
        public void AddConventionalRegistrar_T()
        {

            var context = new ConfigureServicesContext(null, new ServiceCollection(), null);
            context.AddConventionalRegistrar<EmptyConventionalDependencyRegistrar>();
            context.Services.GetSingletonInstanceOrNull<ConventionalRegistrarList>().Any(c => c.GetType() == typeof(EmptyConventionalDependencyRegistrar)).ShouldBeTrue();

        }

        [Fact]
        public void RegisterAssemblyByConvention()
        {
            var context = new ConfigureServicesContext(null, new ServiceCollection(), null);
            var registrar = new EmptyConventionalDependencyRegistrar();
            context.AddConventionalRegistrar(registrar);
            context.Services.GetSingletonInstanceOrNull<ConventionalRegistrarList>().Contains(registrar).ShouldBeTrue();
            var assembly = typeof(ConfigureServicesContextExtensions_Tests).Assembly;
            context.RegisterAssemblyByConvention(assembly);
            registrar.RegisterAssemblyInvoked.ShouldBeTrue();
            registrar.Types.ShouldBe(assembly.GetTypes());
        }
        [Fact]
        public void RegisterAssemblyByConvention2()
        {
            var context = new ConfigureServicesContext(null, new ServiceCollection(), null);
            var registrar = new EmptyConventionalDependencyRegistrar();
            context.AddConventionalRegistrar(registrar);
            context.Services.GetSingletonInstanceOrNull<ConventionalRegistrarList>().Contains(registrar).ShouldBeTrue();
            var assembly = typeof(ConfigureServicesContextExtensions_Tests).Assembly.GetTypes();
            context.RegisterAssemblyByConvention();
            registrar.RegisterAssemblyInvoked.ShouldBeTrue();
            registrar.Types.ShouldBe(assembly);
        }

        [Fact]
        public void RegisterAssemblyByConvention3()
        {
            var context = new ConfigureServicesContext(null, new ServiceCollection(), null);
            var registrar = new EmptyConventionalDependencyRegistrar();
            context.AddConventionalRegistrar(registrar);
            context.Services.GetSingletonInstanceOrNull<ConventionalRegistrarList>().Contains(registrar).ShouldBeTrue();
            var assembly = typeof(ConfigureServicesContextExtensions_Tests).Assembly.GetTypes();
            context.RegisterAssemblyByConventionOfType<ConfigureServicesContextExtensions_Tests>();
            registrar.RegisterAssemblyInvoked.ShouldBeTrue();
            registrar.Types.ShouldBe(assembly);
        }
    }
}
