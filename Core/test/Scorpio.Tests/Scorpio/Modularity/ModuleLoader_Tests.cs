using Microsoft.Extensions.DependencyInjection;
using Scorpio.Modularity.Plugins;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Scorpio.DependencyInjection;

namespace Scorpio.Modularity
{
    public class ModuleLoader_Tests
    {
        [Fact]
        public void Should_Load_Modules_By_Dependency_Order()
        {
            var services = new ServiceCollection();
            var moduleLoader = new ModuleLoader();
            var modules = moduleLoader.LoadModules(services, typeof(MyStartupModule), new PlugInSourceList());
            modules.Length.ShouldBe(3);
            modules[0].Type.ShouldBe(typeof(KernelModule));
            modules[1].Type.ShouldBe(typeof(IndependentEmptyModule));
            modules[2].Type.ShouldBe(typeof(MyStartupModule));
            modules[0].IsLoadedAsPlugIn.ShouldBeFalse();
        }


    }

    [DependsOn(typeof(IndependentEmptyModule))]
    public class MyStartupModule : ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterAssemblyByConvention();
        }
    }


}
