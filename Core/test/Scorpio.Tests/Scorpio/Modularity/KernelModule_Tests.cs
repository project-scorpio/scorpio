using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Scorpio.DependencyInjection;

namespace Scorpio.Modularity
{
    public class KernelModule_Tests
    {
        [Fact]
        public void ConfigureServices()
        {
            using (var bootstrapeer = Bootstrapper.Create<MyStartupModule>())
            {
                bootstrapeer.Services.Where(s => s.ServiceType == typeof(ISingletonUserService)).SingleOrDefault().Lifetime.ShouldBe(ServiceLifetime.Singleton);
                bootstrapeer.Services.Where(s => s.ServiceType == typeof(IScopedUserService)).SingleOrDefault().Lifetime.ShouldBe(ServiceLifetime.Scoped);
                bootstrapeer.Services.Where(s => s.ServiceType == typeof(ITransientUserService)).SingleOrDefault().Lifetime.ShouldBe(ServiceLifetime.Transient);
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

    class SingletonUserService : ISingletonUserService, ISingletonDependency
    {

    }
    public interface IScopedUserService
    {

    }

    class ScopedUserService : IScopedUserService, IScopedDependency
    {

    }
    public interface ITransientUserService
    {

    }

    class TransientUserService : ITransientUserService, ITransientDependency
    {

    }
}
