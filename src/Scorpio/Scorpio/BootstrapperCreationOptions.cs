using System;
using System.Collections.Generic;
using System.Runtime.Loader;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

using Scorpio.Modularity;
using Scorpio.Modularity.Plugins;

namespace Scorpio
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BootstrapperCreationOptions
    {

        private readonly HashSet<Action<ConfigureServicesContext>> _preConfigureServiceActions = new HashSet<Action<ConfigureServicesContext>>();

        private readonly HashSet<Action<ConfigureServicesContext>> _configureServiceActions = new HashSet<Action<ConfigureServicesContext>>();

        private readonly HashSet<Action<ConfigureServicesContext>> _postConfigureServiceActions = new HashSet<Action<ConfigureServicesContext>>();

        private readonly ICollection<Action<IConfigurationBuilder>> _configurationActions = new HashSet<Action<IConfigurationBuilder>>();



        /// <summary>
        /// 
        /// </summary>
        public IPlugInSourceList PlugInSources { get; }



        internal Func<IServiceFactoryAdapter> ServiceFactory { get; set; } = () => new ServiceFactoryAdapter<IServiceCollection>(new DefaultServiceProviderFactory());


        internal BootstrapperCreationOptions() => PlugInSources = new PlugInSourceList(new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory), AssemblyLoadContext.Default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configureDelegate"></param>
        /// <returns></returns>
        public BootstrapperCreationOptions PreConfigureServices(Action<ConfigureServicesContext> configureDelegate)
        {
            _preConfigureServiceActions.Add(configureDelegate);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configureDelegate"></param>
        /// <returns></returns>
        public BootstrapperCreationOptions ConfigureServices(Action<ConfigureServicesContext> configureDelegate)
        {
            _configureServiceActions.Add(configureDelegate);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configureDelegate"></param>
        /// <returns></returns>
        public BootstrapperCreationOptions PostConfigureServices(Action<ConfigureServicesContext> configureDelegate)
        {
            _postConfigureServiceActions.Add(configureDelegate);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void Configuration(Action<IConfigurationBuilder> action) => _configurationActions.Add(action);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TContainerBuilder"></typeparam>
        /// <param name="factory"></param>
        public void UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory) => ServiceFactory = () => new ServiceFactoryAdapter<TContainerBuilder>(factory);

        internal void ConfigureConfiguration(IConfigurationBuilder configurationBuilder) => _configurationActions.ForEach(a => a(configurationBuilder));

        internal void PreConfigureServices(ConfigureServicesContext context) => _preConfigureServiceActions.ForEach(a => a(context));

        internal void ConfigureServices(ConfigureServicesContext context) => _configureServiceActions.ForEach(a => a(context));
        internal void PostConfigureServices(ConfigureServicesContext context) => _postConfigureServiceActions.ForEach(a => a(context));
    }
}
