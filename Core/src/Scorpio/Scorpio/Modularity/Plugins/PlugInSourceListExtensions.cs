using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Modularity.Plugins
{
    /// <summary>
    /// 
    /// </summary>
    public static class PlugInSourceListExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <param name="plugs"></param>
        public static void AddType<TModule>(this PlugInSourceList plugs) where TModule : IScorpioModule
        {
            plugs.AddType(typeof(TModule));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plugs"></param>
        /// <param name="moduleType"></param>
        public static void AddType(this PlugInSourceList plugs, params Type[] moduleType)
        {
            plugs.Add(new TypePlugInSource(moduleType));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plugs"></param>
        /// <param name="filePaths"></param>
        public static void AddFile(this PlugInSourceList plugs, params string[] filePaths)
        {
            plugs.Add(new FilePlugInSource(filePaths));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plugs"></param>
        /// <param name="path"></param>
        /// <param name="searchOption"></param>
        public static void AddFolder(this PlugInSourceList plugs, string path, System.IO.SearchOption searchOption)
        {
            plugs.Add(new FolderPlugInSource(path, searchOption));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plugs"></param>
        /// <param name="path"></param>
        /// <param name="searchOption"></param>
        /// <param name="predicate"></param>
        public static void AddFolder(this PlugInSourceList plugs, string path, System.IO.SearchOption searchOption, Func<string, bool> predicate)
        {
            plugs.Add(new FolderPlugInSource(path, searchOption) { Filter = predicate });
        }
    }
}
