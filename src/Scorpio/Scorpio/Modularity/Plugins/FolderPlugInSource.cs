using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.FileSystemGlobbing;

namespace Scorpio.Modularity.Plugins
{
    internal class FolderPlugInSource : IPlugInSource
    {
        private readonly string _path;
        private readonly PlugInSourceList _plugInSourceLists;

        public Func<string, bool> Filter { get; set; }

        public FolderPlugInSource(PlugInSourceList plugInSourceLists, string path)
        {
            _plugInSourceLists = plugInSourceLists;
            _path = path;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S3626:Jump statements should not be redundant", Justification = "<Pending>")]
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
                catch (ReflectionTypeLoadException)
                {
                    continue;
                }
                catch (Exception ex)
                {
                    throw new ScorpioException("Could not get module types from assembly: " + assembly.FullName, ex);
                }
            }

            return modules.ToArray();
        }

        internal IEnumerable<Assembly> GetAssemblies()
        {
            var matcher = new Matcher(StringComparison.OrdinalIgnoreCase);
            matcher.AddInclude("./**/*.dll").AddInclude("./**/*.exe");
            var assemblyFiles = _plugInSourceLists.FileProvider.GetDirectoryContents(_path)
                .Where(f => matcher.Match(f.PhysicalPath).HasMatches);

            if (Filter != null)
            {
                assemblyFiles = assemblyFiles.Where(f => Filter(f.Name));
            }
            return assemblyFiles.Select(f => _plugInSourceLists.AssemblyLoadContext.LoadFromStream(f.CreateReadStream()));
        }
    }
}
