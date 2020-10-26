using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

using Microsoft.Extensions.FileProviders;

using NSubstitute;

using Shouldly;

using Xunit;

namespace Scorpio.Modularity.Plugins
{
    public class FilePlugInSource_Tests
    {
        [Fact]
        public void GetModules()
        {
            var fileProvider = Substitute.For<IFileProvider>();
            var file = Substitute.For<IFileInfo>();
            file.CreateReadStream().Returns(new FileStream(Assembly.GetExecutingAssembly().Location, FileMode.Open, FileAccess.Read));
            fileProvider.GetFileInfo(default).ReturnsForAnyArgs(file);
            var context = Substitute.For<AssemblyLoadContext>();
            var list = new PlugInSourceList(fileProvider, context);
            var source = new FilePlugInSource(list, new string[] { "Test.dll" });
            source.GetModules().Count().ShouldBe(5);
        }
    }
}
