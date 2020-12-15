
using System;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Castle.DynamicProxy;
using Scorpio.DynamicProxy;

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
        /// <param name="configurationAction"></param>
        /// <returns></returns>
        public static BootstrapperCreationOptions UseAutofac(this BootstrapperCreationOptions options, Action<ContainerBuilder> configurationAction = null)
        {
            options.PreConfigureServices(context =>
            {
                ProxyTargetProvider.Default.Add(new AutofacProxyTargetProvider());
                context.Services.AddTransient(typeof(AsyncDeterminationInterceptor<>),typeof(AsyncDeterminationInterceptor<>));
                context.Services.AddSingleton<IProxyConventionalAction>(new ProxyConventionalAction());
            });
            options.UseServiceProviderFactory(new AutofacServiceProviderFactory(configurationAction));
            return options;
        }
    }
}
