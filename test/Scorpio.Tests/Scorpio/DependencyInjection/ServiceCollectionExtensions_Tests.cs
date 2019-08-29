using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;
using Scorpio.Conventional;

namespace Scorpio.DependencyInjection
{
   public class ServiceCollectionExtensions_Tests
    {
        [Fact]
        public void AddConventionalRegistrar()
        {
            var services= new ServiceCollection();
            services.AddConventionalRegistrar<EmptyConventionalDependencyRegistrar>();
            ConventionalRegistrarList.Registrars.Any(c=>c.GetType()==typeof(EmptyConventionalDependencyRegistrar)).ShouldBeTrue();
        }

        [Fact]
        public void RegisterAssemblyByConvention()
        {
            var services = new ServiceCollection();
            var registrar = new EmptyConventionalDependencyRegistrar();
            services.AddConventionalRegistrar(registrar);
            ConventionalRegistrarList.Registrars.Contains(registrar).ShouldBeTrue();
            var assembly = typeof(ServiceCollectionExtensions_Tests).Assembly;
            services.RegisterAssemblyByConvention(assembly);
            registrar.RegisterAssemblyInvoked.ShouldBeTrue();
            registrar.Assembly.ShouldBe(assembly);
        }
        [Fact]
        public void RegisterAssemblyByConvention2()
        {
            var services = new ServiceCollection();
            var registrar = new EmptyConventionalDependencyRegistrar();
            services.AddConventionalRegistrar(registrar);
            ConventionalRegistrarList.Registrars.Contains(registrar).ShouldBeTrue();
            var assembly = typeof(ServiceCollectionExtensions_Tests).Assembly;
            services.RegisterAssemblyByConvention();
            registrar.RegisterAssemblyInvoked.ShouldBeTrue();
            registrar.Assembly.ShouldBe(assembly);
        }
    }

    class EmptyConventionalDependencyRegistrar : IConventionalRegistrar
    {
        public bool RegisterAssemblyInvoked { get; set; }

        public Assembly Assembly { get; set; }

        public void Register(IConventionalRegistrationContext context)
        {
            Assembly = context.Assembly;
            RegisterAssemblyInvoked = true;
        }

    }
}
