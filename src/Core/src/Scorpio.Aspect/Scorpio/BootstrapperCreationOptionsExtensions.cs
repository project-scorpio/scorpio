
using AspectCore.DependencyInjection;

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
            options.ConfigureServices(context => context.Services.ReplaceSingleton<IPropertyInjectorFactory, DependencyInjection.PropertyInjectorFactory>()
            .AddScoped<IServiceResolveCallback, DependencyInjection.PropertyInjectorCallback>());
            options.UseServiceProviderFactory(new AspectCore.Extensions.DependencyInjection.ServiceContextProviderFactory());
            return options;
        }
    }
}
