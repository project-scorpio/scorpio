using System;

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
        public static void AddType<TModule>(this IPlugInSourceList plugs) where TModule : IScorpioModule
        {
            plugs.AddType(typeof(TModule));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plugs"></param>
        /// <param name="moduleType"></param>
        public static void AddType(this IPlugInSourceList plugs, params Type[] moduleType)
        {
            plugs.Add(new TypePlugInSource(moduleType));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plugs"></param>
        /// <param name="filePaths"></param>
        public static void AddFile(this IPlugInSourceList plugs, params string[] filePaths)
        {
            plugs.Add(new FilePlugInSource(plugs.As<PlugInSourceList>(), filePaths));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="plugs"></param>
        /// <param name="path"></param>
        /// <param name="predicate"></param>
        public static void AddFolder(this IPlugInSourceList plugs, string path, Func<string, bool> predicate=default)
        {
            plugs.Add(new FolderPlugInSource(plugs.As<PlugInSourceList>(), path) { Filter = predicate });
        }

        private static void Add(this IPlugInSourceList plugs,IPlugInSource plug)
        {
            (plugs as PlugInSourceList)?.Add(plug);
        }
    }
}
