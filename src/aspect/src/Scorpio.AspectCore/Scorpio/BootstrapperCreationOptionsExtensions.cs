
using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using AspectCore.Extensions.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

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
        /// <returns></returns>
        public static BootstrapperCreationOptions UseAspectCore(this BootstrapperCreationOptions options)
        {
            options.PreConfigureServices(context =>
            {
                ProxyTargetProvider.Default.Add(new AspectCoreProxyTargetProvider());
                context.Services.AddSingleton<IProxyConventionalAction>(new ProxyConventionalAction())
                            .AddSingleton<IAdditionalInterceptorSelector,ConfigureAdditionalInterceptorSelector>()
                            .ConfigureDynamicProxy(c=>c.ValidationHandlers.Add(new ConfigureAdditionalAspectValidationHandler(c)));
            });
            options.ConfigureServices(context => context.Services.ReplaceSingleton<IPropertyInjectorFactory, DependencyInjection.PropertyInjectorFactory>()
            .AddScoped<IServiceResolveCallback, DependencyInjection.PropertyInjectorCallback>().AddTransient(typeof(AspectCoreInterceptorAdapter<>)));
            options.UseServiceProviderFactory(new AspectCore.Extensions.DependencyInjection.ServiceContextProviderFactory());
            return options;
        }
    }
}
