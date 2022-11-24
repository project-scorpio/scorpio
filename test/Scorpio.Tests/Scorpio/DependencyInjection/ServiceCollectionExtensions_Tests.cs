using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Conventional;

using Shouldly;

using Xunit;

namespace Scorpio.DependencyInjection
{
    public class ServiceCollectionExtensions_Tests
    {
        [Fact]
        public void AddConventionalRegistrar()
        {
            var services = new ServiceCollection();
            services.AddConventionalRegistrar<EmptyConventionalDependencyRegistrar>();
            services.GetSingletonInstanceOrNull<ConventionalRegistrarList>().Any(c => c.GetType() == typeof(EmptyConventionalDependencyRegistrar)).ShouldBeTrue();
        }

        [Fact]
        public void RegisterAssemblyByConvention()
        {
            var services = new ServiceCollection();
            var registrar = new EmptyConventionalDependencyRegistrar();
            services.AddConventionalRegistrar(registrar);
            services.GetSingletonInstanceOrNull<ConventionalRegistrarList>().Contains(registrar).ShouldBeTrue();
            var assembly = typeof(ServiceCollectionExtensions_Tests).Assembly;
            services.RegisterAssemblyByConvention(assembly);
            registrar.RegisterAssemblyInvoked.ShouldBeTrue();
            registrar.Types.ShouldBe(assembly.GetTypes());
        }
        [Fact]
        public void RegisterAssemblyByConvention2()
        {
            var services = new ServiceCollection();
            var registrar = new EmptyConventionalDependencyRegistrar();
            services.AddConventionalRegistrar(registrar);
            services.GetSingletonInstanceOrNull<ConventionalRegistrarList>().Contains(registrar).ShouldBeTrue();
            var assembly = typeof(ServiceCollectionExtensions_Tests).Assembly.GetTypes();
            services.RegisterAssemblyByConvention();
            registrar.RegisterAssemblyInvoked.ShouldBeTrue();
            registrar.Types.ShouldBe(assembly);
        }

        [Fact]
        public void RegisterAssemblyByConvention3()
        {
            var services = new ServiceCollection();
            var registrar = new EmptyConventionalDependencyRegistrar();
            services.AddConventionalRegistrar(registrar);
            services.GetSingletonInstanceOrNull<ConventionalRegistrarList>().Contains(registrar).ShouldBeTrue();
            var assembly = typeof(ServiceCollectionExtensions_Tests).Assembly.GetTypes();
            services.RegisterAssemblyByConventionOfType<ServiceCollectionExtensions_Tests>();
            registrar.RegisterAssemblyInvoked.ShouldBeTrue();
            registrar.Types.ShouldBe(assembly);
        }
    }

    internal class EmptyConventionalDependencyRegistrar : IConventionalRegistrar
    {
        public bool RegisterAssemblyInvoked { get; set; }

        public IEnumerable<Type> Types { get; set; }

        public void Register(IConventionalRegistrationContext context)
        {
            Types = context.Types;
            RegisterAssemblyInvoked = true;
        }

    }
}
