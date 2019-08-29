using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.Modularity;
using Scorpio.DependencyInjection;
using AspectCore.Extensions;
using AspectCore.Injector;
using AspectCore.Extensions.DependencyInjection;
using System.Reflection;
using AspectCore.DynamicProxy;
using AspectCore.Configuration;
using Microsoft.Extensions.Configuration;

namespace Scorpio
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Bootstrapper : IBootstrapper, IModuleContainer
    {

        /// <summary>
        /// 
        /// </summary>
        /// <localize>
        /// 
        /// </localize>
        public Type StartupModuleType { get; }

        private readonly BootstrapperCreationOptions _options;

        /// <summary>
        /// 
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// The <see cref="IConfiguration" /> containing the merged configuration of the application.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// A central location for sharing state between components during the host building process.
        /// </summary>
        public IDictionary<string, object> Properties { get; }


        /// <summary>
        /// 
        /// </summary>
        public IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyList<IModuleDescriptor> Modules { get; }

        /// <summary>
        /// 
        /// </summary>
        internal protected IModuleLoader ModuleLoader { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startupModuleType"></param>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="optionsAction"></param>
        protected Bootstrapper(Type startupModuleType, IServiceCollection services, IConfiguration configuration, Action<BootstrapperCreationOptions> optionsAction)
        {
            Services = services;
            StartupModuleType = startupModuleType;
            Properties = new Dictionary<string, object>();
            _options = new BootstrapperCreationOptions(services);
            ModuleLoader = new ModuleLoader();
            optionsAction(_options);
            var configBuilder = new ConfigurationBuilder();
            if (configuration != null)
            {
                configBuilder.AddConfiguration(configuration);
            }
            _options.ConfigurationActions.ForEach(a => a(configBuilder));
            this.Configuration = configBuilder.Build();
            ConfigureCoreService(services);
            Modules = LoadModules();
            ConfigureServices();
        }

        private void ConfigureCoreService(IServiceCollection services)
        {
            services.AddLogging();
            services.AddSingleton<IBootstrapper>(this);
            services.AddSingleton<IModuleContainer>(this);
            services.AddSingleton(ModuleLoader);
            services.AddSingleton<IModuleManager, ModuleManager>();
        }

        private void ConfigureServices()
        {
            var context = new ConfigureServicesContext(this, Services) { Configuration = Configuration };
            Services.AddSingleton(context);
            Modules.ForEach(m => m.Instance.PreConfigureServices(context));
            Modules.ForEach(m => m.Instance.ConfigureServices(context));
            Modules.ForEach(m => m.Instance.PostConfigureServices(context));
        }

        private IReadOnlyList<IModuleDescriptor> LoadModules()
        {
            return ModuleLoader.LoadModules(Services, StartupModuleType, _options.PlugInSources);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        internal void SetServiceProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }



        /// <summary>
        /// 
        /// </summary>
        public virtual void Initialize(params object[] initializeParams)
        {
            InitializeModules(initializeParams);
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void InitializeModules(params object[] initializeParams)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                scope.ServiceProvider
                    .GetRequiredService<IModuleManager>()
                    .InitializeModules(new ApplicationInitializationContext(scope.ServiceProvider, initializeParams));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Shutdown()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                scope.ServiceProvider
                    .GetRequiredService<IModuleManager>()
                    .ShutdownModules(new ApplicationShutdownContext(scope.ServiceProvider));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStartupModule"></typeparam>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static IBootstrapper Create<TStartupModule>(Action<BootstrapperCreationOptions> optionsAction) where TStartupModule : IScorpioModule
        {
            return Create(typeof(TStartupModule), optionsAction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsAction"></param>
        /// <param name="startupModuleType"></param>
        /// <returns></returns>
        public static IBootstrapper Create(Type startupModuleType, Action<BootstrapperCreationOptions> optionsAction)
        {
            if (!startupModuleType.IsAssignableTo<IScorpioModule>())
            {
                throw new ArgumentException($"{nameof(startupModuleType)} should be derived from {typeof(IScorpioModule)}");
            }
            var services = new ServiceCollection();
            var configBuilder = new ConfigurationBuilder();
            var config = configBuilder.Build();
            var bootstrapper = new InternalBootstrapper(startupModuleType, services, config, optionsAction);
            bootstrapper.SetServiceProvider(services.BuildAspectInjectorProvider());
            return bootstrapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startupModuleType"></param>
        /// <returns></returns>
        public static IBootstrapper Create(Type startupModuleType)
        {
            return Create(startupModuleType, o => {});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStartupModule"></typeparam>
        /// <returns></returns>
        public static IBootstrapper Create<TStartupModule>() where TStartupModule : IScorpioModule
        {
            return Create(typeof(TStartupModule));
        }


        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Shutdown();
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Bootstrapper() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
