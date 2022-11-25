using System;
using System.Reflection;

using Scorpio.Conventional;

using Shouldly;

using Xunit;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public class DependencyInjectionServiceCollectionExtensions_Test
    {
        private class TestConventionalRegistrar : IConventionalRegistrar
        {
            private readonly Action<IConventionalRegistrationContext> _action;

            public TestConventionalRegistrar()
            {

            }

            public TestConventionalRegistrar(Action<IConventionalRegistrationContext> action)
            {
                _action = action;
            }
            public void Register(IConventionalRegistrationContext context) => _action?.Invoke(context);
        }

        [Fact]
        public void AddConventionalRegistrar()
        {
            var registrar = new TestConventionalRegistrar();
            var services = new ServiceCollection();
            Should.NotThrow(() => services.AddConventionalRegistrar(registrar)).ShouldContainSingleton(typeof(ConventionalRegistrarList)).ImplementationInstance.ShouldBeOfType<ConventionalRegistrarList>().ShouldHaveSingleItem().ShouldBe(registrar);
        }

        [Fact]
        public void AddConventionalRegistrar_T()
        {
            var services = new ServiceCollection();
            Should.NotThrow(() => services.AddConventionalRegistrar<TestConventionalRegistrar>()).ShouldContainSingleton(typeof(ConventionalRegistrarList)).ImplementationInstance.ShouldBeOfType<ConventionalRegistrarList>().ShouldHaveSingleItem().ShouldBeOfType<TestConventionalRegistrar>();
        }

        [Fact]
        public void RegisterAssemblyByConvention()
        {
            var services = new ServiceCollection();
            var registrar = new TestConventionalRegistrar(context =>
            {
                context.Services.ShouldBe(services);
                context.Types.ShouldContain(GetType());
            });
            Should.NotThrow(() => services.AddConventionalRegistrar(registrar));
            Should.NotThrow(() => services.RegisterAssemblyByConvention(GetType().Assembly));
        }


        [Fact]
        public void RegisterAssemblyByConvention_2()
        {
            var services = new ServiceCollection();
            var registrar = new TestConventionalRegistrar(context =>
            {
                context.Services.ShouldBe(services);
                context.Types.ShouldContain(GetType());
            });
            Should.NotThrow(() => services.AddConventionalRegistrar(registrar));
            Should.NotThrow(() => services.RegisterAssemblyByConvention());
        }

        [Fact]
        public void RegisterAssemblyByConventionOfType()
        {
            var services = new ServiceCollection();
            var registrar = new TestConventionalRegistrar(context =>
            {
                context.Services.ShouldBe(services);
                context.Types.ShouldContain(GetType());
            });
            Should.NotThrow(() => services.AddConventionalRegistrar(registrar));
            Should.NotThrow(() => services.RegisterAssemblyByConventionOfType<DependencyInjectionServiceCollectionExtensions_Test>());
        }

    }
}
