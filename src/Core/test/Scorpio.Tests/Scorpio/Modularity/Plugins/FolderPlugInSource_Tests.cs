using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Internal;

using NSubstitute;

using Shouldly;

using Xunit;

namespace Scorpio.Modularity.Plugins
{
    public class FolderPlugInSource_Tests
    {
        [Fact]
        public void GetModules()
        {
            var fileProvider = Substitute.For<IFileProvider>();
            var file = Substitute.For<IFileInfo>();
            file.PhysicalPath.Returns(@"d:\test\plugs\test.dll");
            file.CreateReadStream().Returns(new FileStream(Assembly.GetExecutingAssembly().Location, FileMode.Open, FileAccess.Read));
            var contents = new PhysicalDirectoryContents(AppDomain.CurrentDomain.BaseDirectory);
            fileProvider.GetDirectoryContents(default).ReturnsForAnyArgs(contents);
            var context = Substitute.For<AssemblyLoadContext>();
            var list = new PlugInSourceList(fileProvider, context);
            var source = new FolderPlugInSource(list, "plugs") { Filter = f => f == "Scorpio.Tests.dll" };
            source.GetModules().Count().ShouldBe(5);
        }
    }
}
