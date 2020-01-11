using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
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
        /// <returns></returns>
        public static BootstrapperCreationOptions UseAspectCore(this BootstrapperCreationOptions options)
        {
            options.UseServiceProviderFactory(new AspectCore.Extensions.DependencyInjection.ServiceContextProviderFactory());
            options.PostConfigureServices(context => DynamicProxy.InterceptorHelper.RegisterConventionalInterceptor(context.Services));
            return options;
        }
    }
}
