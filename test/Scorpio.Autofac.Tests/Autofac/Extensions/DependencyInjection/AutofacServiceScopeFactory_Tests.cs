using System;

using NSubstitute;
using NSubstitute.Extensions;

using Shouldly;

using Xunit;

namespace Autofac.Extensions.DependencyInjection
{
    public class AutofacServiceScopeFactory_Tests
    {
        [Fact]
        public void CreateInstance()
        {
            Should.Throw<ArgumentNullException>(() => new AutofacServiceScopeFactory(null));
            Should.NotThrow(() => new AutofacServiceScopeFactory(Substitute.For<ILifetimeScope>())).ShouldNotBeNull();
        }

        [Fact]
        public void CreateScope()
        {
            var scope = Substitute.For<ILifetimeScope>();
            scope.Configure().ComponentRegistry.TryGetServiceRegistration(default, out var registration).ReturnsForAnyArgs(true);
            scope.Configure().ResolveComponent(default).ReturnsForAnyArgs(Substitute.For<IServiceProvider>());
            scope.Configure().BeginLifetimeScope().Returns(scope);
            Should.NotThrow(() => new AutofacServiceScopeFactory(scope)).CreateScope().ShouldBeOfType<AutofacServiceScope>().ShouldNotBeNull();
        }
    }
}
