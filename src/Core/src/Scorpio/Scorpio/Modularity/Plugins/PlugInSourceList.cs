using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;

using Microsoft.Extensions.FileProviders;

namespace Scorpio.Modularity.Plugins
{
    /// <summary>
    /// 
    /// </summary>
    class PlugInSourceList : List<IPlugInSource>, IPlugInSourceList
    {
        public PlugInSourceList(IFileProvider fileProvider, AssemblyLoadContext assemblyLoadContext)
        {
            FileProvider = fileProvider;
            AssemblyLoadContext = assemblyLoadContext;
        }

        public IFileProvider FileProvider { get; }

        public AssemblyLoadContext AssemblyLoadContext { get; }

        internal Type[] GetAllModules()
        {
            return this
                .SelectMany(pluginSource => pluginSource.GetModulesWithAllDependencies())
                .Distinct()
                .ToArray();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IPlugInSourceList
    {

    }
}