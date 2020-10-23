using Scorpio.Modularity;

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
            return options;
        }
    }
}
