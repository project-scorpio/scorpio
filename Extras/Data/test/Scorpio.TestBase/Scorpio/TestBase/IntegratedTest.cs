using AspectCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.Modularity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.TestBase
{
    public abstract class IntegratedTest<TStartupModule> : TestBaseWithServiceProvider, IDisposable
        where TStartupModule : IScorpioModule
    {
        protected IBootstrapper Bootstrapper { get; }

        protected override IServiceProvider ServiceProvider => Bootstrapper.ServiceProvider;

        protected IServiceProvider RootServiceProvider { get; }

        protected IServiceScope TestServiceScope { get; }

        protected IntegratedTest()
        {
            var services = CreateServiceCollection();

            BeforeAddBootstrapper(services);

        var bootstrapper=    new InternalBootstrapper(typeof(TStartupModule),services,null,SetBootstrapperCreationOptions);
            Bootstrapper = bootstrapper;

            AfterAddBootstrapper(services);

            RootServiceProvider = CreateServiceProvider(services);
            TestServiceScope = RootServiceProvider.CreateScope();
            bootstrapper.SetServiceProvider(TestServiceScope.ServiceProvider);
            bootstrapper.Initialize();
        }

        protected virtual IServiceCollection CreateServiceCollection()
        {
            return new ServiceCollection();
        }

        protected virtual void BeforeAddBootstrapper(IServiceCollection services)
        {

        }

        protected virtual void SetBootstrapperCreationOptions(BootstrapperCreationOptions options)
        {

        }

        protected virtual void AfterAddBootstrapper(IServiceCollection services)
        {

        }

        protected virtual IServiceProvider CreateServiceProvider(IServiceCollection services)
        {
            return services.BuildDynamicProxyServiceProvider();
        }

        public virtual void Dispose()
        {
            Bootstrapper.Shutdown();
            TestServiceScope.Dispose();
            Bootstrapper.Dispose();
        }
    }
}
