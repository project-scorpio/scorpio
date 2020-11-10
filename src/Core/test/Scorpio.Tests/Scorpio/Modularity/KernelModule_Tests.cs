using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.DependencyInjection;

using Shouldly;

using Xunit;

namespace Scorpio.Modularity
{
    public class KernelModule_Tests
    {
        [Fact]
        public void ConfigureServices()
        {
            using (var bootstrapeer = Bootstrapper.Create<MyStartupModule>())
            {
                bootstrapeer.Services.SingleOrDefault(s => s.ServiceType == typeof(ISingletonUserService)).Lifetime.ShouldBe(ServiceLifetime.Singleton);
                bootstrapeer.Services.SingleOrDefault(s => s.ServiceType == typeof(IScopedUserService)).Lifetime.ShouldBe(ServiceLifetime.Scoped);
                bootstrapeer.Services.SingleOrDefault(s => s.ServiceType == typeof(ITransientUserService)).Lifetime.ShouldBe(ServiceLifetime.Transient);
                bootstrapeer.ServiceProvider.GetService<ISingletonUserService>().ShouldBeOfType<SingletonUserService>().ShouldNotBeNull();
                bootstrapeer.ServiceProvider.GetService<IScopedUserService>().ShouldBeOfType<ScopedUserService>().ShouldNotBeNull();
                bootstrapeer.ServiceProvider.GetService<ITransientUserService>().ShouldBeOfType<TransientUserService>().ShouldNotBeNull();
                bootstrapeer.ServiceProvider.GetService<IScopedDependency>().ShouldBeNull();
            }
        }
    }

    public interface ISingletonUserService
    {

    }

    internal class SingletonUserService : ISingletonUserService, ISingletonDependency
    {

    }
    public interface IScopedUserService
    {

    }

    internal class ScopedUserService : IScopedUserService, IScopedDependency
    {

    }
    public interface ITransientUserService
    {

    }

    internal class TransientUserService : ITransientUserService, ITransientDependency
    {

    }
}
