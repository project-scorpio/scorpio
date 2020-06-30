using System;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Modularity.Plugins;

namespace Scorpio.Modularity
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModuleLoader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="startupModuleType"></param>
        /// <param name="plugInSources"></param>
        /// <returns></returns>
        IModuleDescriptor[] LoadModules(
            IServiceCollection services,
            Type startupModuleType,
            PlugInSourceList plugInSources
            );
    }
}
