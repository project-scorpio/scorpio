using System;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Modularity;

namespace Scorpio.TestBase
{
    public abstract class IntegratedTest<TStartupModule> : TestBaseWithServiceProvider, IDisposable
        where TStartupModule : IScorpioModule
    {
        private bool _disposedValue;

        protected IBootstrapper Bootstrapper { get; }

        protected override IServiceProvider ServiceProvider => Bootstrapper.ServiceProvider;

        protected IServiceProvider RootServiceProvider { get; }

        protected IServiceScope TestServiceScope { get; }

        protected IntegratedTest()
        {
            var services = CreateServiceCollection();

            BeforeAddBootstrapper(services);

            var bootstrapper = CreateBootstrapper(services);
            Bootstrapper = bootstrapper;

            AfterAddBootstrapper(services);

            RootServiceProvider = CreateServiceProvider(bootstrapper);
            TestServiceScope = RootServiceProvider.CreateScope();
            bootstrapper.SetServiceProvider(TestServiceScope.ServiceProvider);
            bootstrapper.Initialize();
        }

        protected virtual Bootstrapper CreateBootstrapper(IServiceCollection services)
        {
            return new InternalBootstrapper(typeof(TStartupModule), services, null, SetBootstrapperCreationOptions);
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

        protected virtual IServiceProvider CreateServiceProvider(Bootstrapper bootstrapper)
        {
            var builder = bootstrapper.ServiceFactoryAdapter.CreateBuilder(bootstrapper.Services);
            return bootstrapper.ServiceFactoryAdapter.CreateServiceProvider(builder);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Bootstrapper.Shutdown();
                    TestServiceScope.Dispose();
                    Bootstrapper.Dispose();
                }

                
                _disposedValue = true;
            }
        }

       
        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
