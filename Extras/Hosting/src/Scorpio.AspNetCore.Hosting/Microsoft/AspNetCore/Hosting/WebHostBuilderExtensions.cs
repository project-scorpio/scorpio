using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Scorpio;
using Scorpio.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
namespace Microsoft.AspNetCore.Hosting
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebHostBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStartupModule"></typeparam>
        /// <param name="webHostBuilder"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseBootstrapper<TStartupModule>(this IWebHostBuilder webHostBuilder)
            where TStartupModule : Scorpio.Modularity.IScorpioModule
        {
            return UseBootstrapper<TStartupModule>(webHostBuilder, o => { });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStartupModule"></typeparam>
        /// <param name="webHostBuilder"></param>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseBootstrapper<TStartupModule>(this IWebHostBuilder webHostBuilder, Action<BootstrapperCreationOptions> optionsAction)
            where TStartupModule : Scorpio.Modularity.IScorpioModule
        {
            return UseBootstrapper(webHostBuilder, typeof(TStartupModule), optionsAction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webHostBuilder"></param>
        /// <param name="startupModuleType"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseBootstrapper(this IWebHostBuilder webHostBuilder, Type startupModuleType)
        {
            return UseBootstrapper(webHostBuilder, startupModuleType, o => { });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webHostBuilder"></param>
        /// <param name="startupModuleType"></param>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseBootstrapper(this IWebHostBuilder webHostBuilder, Type startupModuleType, Action<BootstrapperCreationOptions> optionsAction)
        {
            webHostBuilder.ConfigureServices((context,services) =>
            {
                services.AddSingleton<IStartup>(new Startup(startupModuleType,context, optionsAction));
            });
            return webHostBuilder;
        }


    }
}
