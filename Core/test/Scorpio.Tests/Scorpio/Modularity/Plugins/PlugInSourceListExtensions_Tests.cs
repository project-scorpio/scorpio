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
    public class PlugInSourceListExtensions_Tests
    {
        [Fact]
        public void AddFile()
        {
            var fileProvider = Substitute.For<IFileProvider>();
            var file = Substitute.For<IFileInfo>();
            file.CreateReadStream().Returns(new FileStream(Assembly.GetExecutingAssembly().Location, FileMode.Open, FileAccess.Read));
            fileProvider.GetFileInfo(default).ReturnsForAnyArgs(file);
            var context = Substitute.For<AssemblyLoadContext>();
            var list = new PlugInSourceList(fileProvider, context);
            list.AddFile("test.dll");
            list.GetAllModules().Count().ShouldBe(6);

        }
        [Fact]
        public void AddFolder()
        {
            var fileProvider = Substitute.For<IFileProvider>();
            var contents = new PhysicalDirectoryContents(AppDomain.CurrentDomain.BaseDirectory);
            fileProvider.GetDirectoryContents(default).ReturnsForAnyArgs(contents);
            var context = Substitute.For<AssemblyLoadContext>();
            var list = new PlugInSourceList(fileProvider, context);
            list.AddFolder("plugs", f => f == "Scorpio.Tests.dll");

            list.GetAllModules().Count().ShouldBe(6);

        }
    }
}
