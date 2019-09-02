using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace Scorpio.Modularity.Plugins
{
    internal class FolderPlugInSource : IPlugInSource
    {
        private readonly string _path;
        private readonly SearchOption _searchOption;

        public Func<string, bool> Filter { get; set; }

        public FolderPlugInSource(string path, SearchOption searchOption)
        {
            _path = path;
            _searchOption = searchOption;
        }

        public Type[] GetModules()
        {
            var modules = new List<Type>();

            foreach (var assembly in GetAssemblies())
            {
                try
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (ScorpioModule.IsModule(type))
                        {
                            modules.AddIfNotContains(type);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new ScorpioException("Could not get module types from assembly: " + assembly.FullName, ex);
                }
            }

            return modules.ToArray();
        }

        private IEnumerable<Assembly> GetAssemblies()
        {
            var assemblyFiles = Directory.EnumerateFiles(_path,"*.*",_searchOption).Where(f=>Path.GetExtension(f).IsIn("exe","dll"));

            if (Filter != null)
            {
                assemblyFiles = assemblyFiles.Where(Filter);
            }

            return assemblyFiles.Select(AssemblyLoadContext.Default.LoadFromAssemblyPath);
        }
    }
}
