using System;

using NSubstitute;
using NSubstitute.Extensions;

using Shouldly;

using Xunit;

namespace Autofac.Extensions.DependencyInjection
{
    public class AutofacServiceScope_Tests
    {
        [Fact]
        public void CreateInstance()
        {
            Should.Throw<ArgumentNullException>(() => new AutofacServiceScope(null));
            var scope = Substitute.For<ILifetimeScope>();
            scope.Configure().ComponentRegistry.TryGetRegistration(default, out var registration).ReturnsForAnyArgs(true);
            scope.Configure().ResolveComponent(default).ReturnsForAnyArgs(Substitute.For<IServiceProvider>());
            Should.NotThrow(() => new AutofacServiceScope(scope)).ShouldNotBeNull();
        }

        [Fact]
        public void ServiceProvider()
        {
            var scope = Substitute.For<ILifetimeScope>();
            scope.Configure().ComponentRegistry.TryGetRegistration(default, out var registration).ReturnsForAnyArgs(true);
            scope.Configure().ResolveComponent(default).ReturnsForAnyArgs(Substitute.For<IServiceProvider>());
            Should.NotThrow(() => new AutofacServiceScope(scope)).ServiceProvider.ShouldNotBeNull();
        }

        [Fact]
        public void Dispose()
        {
            var scope = Substitute.For<ILifetimeScope>();
            scope.Configure().ComponentRegistry.TryGetRegistration(default, out var registration).ReturnsForAnyArgs(true);
            scope.Configure().ResolveComponent(default).ReturnsForAnyArgs(Substitute.For<IServiceProvider>());
            Should.NotThrow(() => new AutofacServiceScope(scope)).Dispose();
            scope.Received(1).Dispose();
        }
    }
}
