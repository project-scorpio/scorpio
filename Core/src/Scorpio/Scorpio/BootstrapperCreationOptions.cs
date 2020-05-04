using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.Modularity;
using Scorpio.Modularity.Plugins;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BootstrapperCreationOptions
    {

        private readonly HashSet<Action<ConfigureServicesContext>> _preConfigureServiceActions=new HashSet<Action<ConfigureServicesContext>>();

        private readonly HashSet<Action<ConfigureServicesContext>> _configureServiceActions = new HashSet<Action<ConfigureServicesContext>>();

        private readonly HashSet<Action<ConfigureServicesContext>> _postConfigureServiceActions=new HashSet<Action<ConfigureServicesContext>>();

        /// <summary>
        /// 
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// 
        /// </summary>
        public PlugInSourceList PlugInSources { get; }



        internal Func<IServiceFactoryAdapter> ServiceFactory { get; set; } = () => new ServiceFactoryAdapter<IServiceCollection>(new DefaultServiceProviderFactory());

        internal ICollection<Action<IConfigurationBuilder>> ConfigurationActions { get; }

        internal BootstrapperCreationOptions(IServiceCollection services)
        {
            ConfigurationActions = new HashSet<Action<IConfigurationBuilder>>();
            Services = Check.NotNull(services, nameof(services));
            PlugInSources = new PlugInSourceList();
        }

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
        public  void Configuration( Action<IConfigurationBuilder> action)
        {

            ConfigurationActions.Add(action);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TContainerBuilder"></typeparam>
        /// <param name="factory"></param>
        public  void UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
        {
            ServiceFactory = () => new ServiceFactoryAdapter<TContainerBuilder>(factory);
        }

        internal void PreConfigureServices(ConfigureServicesContext context)
        {
            _preConfigureServiceActions.ForEach(a => a(context));
        }

        internal void ConfigureServices(ConfigureServicesContext context)
        {
            _configureServiceActions.ForEach(a => a(context));
        }
        internal void PostConfigureServices(ConfigureServicesContext context)
        {
            _postConfigureServiceActions.ForEach(a => a(context));
        }
    }
}
