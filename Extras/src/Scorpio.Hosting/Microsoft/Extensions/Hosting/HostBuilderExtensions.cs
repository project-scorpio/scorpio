using Scorpio;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// 
    /// </summary>
    public static class HostBuilderExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStartupModule"></typeparam>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static IBootstrapper AddBootstrapper<TStartupModule>(this IHostBuilder hostBuilder)
                 where TStartupModule : Scorpio.Modularity.IScorpioModule
        {
            return AddBootstrapper<TStartupModule>(hostBuilder, o => { });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStartupModule"></typeparam>
        /// <param name="hostBuilder"></param>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static IBootstrapper AddBootstrapper<TStartupModule>(this IHostBuilder hostBuilder, Action<BootstrapperCreationOptions> optionsAction)
            where TStartupModule : Scorpio.Modularity.IScorpioModule
        {
            return AddBootstrapper(hostBuilder, typeof(TStartupModule), optionsAction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="startupModuleType"></param>
        /// <returns></returns>
        public static IBootstrapper AddBootstrapper(this IHostBuilder builder, Type startupModuleType)
        {
            return AddBootstrapper(builder, startupModuleType, o => { });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="startupModuleType"></param>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static IBootstrapper AddBootstrapper(this IHostBuilder builder, Type startupModuleType, Action<BootstrapperCreationOptions> optionsAction)
        {
            InternalBootstrapper bootstrapper = null;
            builder.ConfigureServices((context, services) =>
            {
                bootstrapper = new InternalBootstrapper(startupModuleType, services,context.Configuration, optionsAction);
                bootstrapper.Properties["HostingEnvironment"] = context.HostingEnvironment;
                services.AddSingleton(bootstrapper);
            });
            builder.UseServiceProviderFactory(new DynamicProxyServiceProviderFactory());
            return bootstrapper;
        }
    }
}
