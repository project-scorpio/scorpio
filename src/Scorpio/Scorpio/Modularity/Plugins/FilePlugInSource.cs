using System;
using System.Collections.Generic;
namespace Scorpio.Modularity.Plugins
{
    internal class FilePlugInSource : IPlugInSource
    {
        private readonly string[] _filePaths;
        private readonly PlugInSourceList _plugInSourceLists;

        public FilePlugInSource(PlugInSourceList plugInSourceLists, string[] filePaths)
        {
            _plugInSourceLists = plugInSourceLists;
            _filePaths = filePaths;
        }

        public Type[] GetModules()
        {
            var modules = new List<Type>();

            foreach (var filePath in _filePaths)
            {

                var assembly = _plugInSourceLists.AssemblyLoadContext.LoadFromStream(
                    _plugInSourceLists.FileProvider.GetFileInfo(filePath)
                                                   .CreateReadStream());
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
