using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio
{
    /// <summary>
    /// 
    /// </summary>
    public static class BootstrapperCreationOptionsExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="action"></param>
        public static void Configuration(this BootstrapperCreationOptions options, Action<IConfigurationBuilder> action)
        {

            options.ConfigurationActions.Add(action);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TContainerBuilder"></typeparam>
        /// <param name="options"></param>
        /// <param name="factory"></param>
        public static void UseServiceProviderFactory<TContainerBuilder>(this BootstrapperCreationOptions options,IServiceProviderFactory<TContainerBuilder> factory)
        {
            options.ServiceFactory = () => new ServiceFactoryAdapter<TContainerBuilder>(factory);
        }
    }
}
