using System;
using System.Collections.Generic;
using System.Linq;

namespace Scorpio.Modularity.Plugins
{
    /// <summary>
    /// 
    /// </summary>
    public class PlugInSourceList:List<IPlugInSource>
    {
        internal Type[] GetAllModules()
        {
            return this
                .SelectMany(pluginSource => pluginSource.GetModulesWithAllDependencies())
                .Distinct()
                .ToArray();
        }
    }
}