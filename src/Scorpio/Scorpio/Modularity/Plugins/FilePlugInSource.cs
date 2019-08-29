using System;
using System.Collections.Generic;
using System.Runtime.Loader;
using System.Text;
using System.Linq;
namespace Scorpio.Modularity.Plugins
{
    internal class FilePlugInSource : IPlugInSource
    {
        private readonly string[] _filePaths;

        public FilePlugInSource(params string[] filePaths)
        {
            _filePaths = filePaths;
        }

        public Type[] GetModules()
        {
            var modules = new List<Type>();

            foreach (var filePath in _filePaths)
            {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(filePath);

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
    }
}
