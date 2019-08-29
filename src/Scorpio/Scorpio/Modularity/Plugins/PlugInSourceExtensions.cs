using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scorpio.Modularity.Plugins
{
   internal static class PlugInSourceExtensions
    {
        public static Type[] GetModulesWithAllDependencies( this IPlugInSource plugInSource)
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
