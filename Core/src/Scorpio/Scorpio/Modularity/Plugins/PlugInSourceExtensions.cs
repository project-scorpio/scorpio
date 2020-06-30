using System;
using System.Linq;

namespace Scorpio.Modularity.Plugins
{
    internal static class PlugInSourceExtensions
    {
        public static Type[] GetModulesWithAllDependencies(this IPlugInSource plugInSource)
        {
            Check.NotNull(plugInSource, nameof(plugInSource));

            return plugInSource
                .GetModules()
                .SelectMany(ModuleHelper.FindAllModuleTypes)
                .Distinct()
                .ToArray();
        }
    }
}
