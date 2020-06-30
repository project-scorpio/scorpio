using System;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Modularity;
using Scorpio.Modularity.Plugins;

using Shouldly;

using Xunit;

namespace Scorpio
{
    public class BootstrapperTest
    {
        [Fact]
        public void Should_Initialize_Single_Module()
        {
            Should.Throw<ArgumentException>(() => Bootstrapper.Create(typeof(BootstrapperTest)));
            using (var bootstrapper = Bootstrapper.Create<IndependentEmptyModule>())
            {
                var moduleContainer = bootstrapper.ServiceProvider.GetRequiredService<IModuleContainer>();
                moduleContainer.Modules.Count.ShouldBe(2);
                moduleContainer.Modules[1].Type.ShouldBe(typeof(IndependentEmptyModule));
                moduleContainer.Modules[1].IsLoadedAsPlugIn.ShouldBeFalse();
                var module = bootstrapper.ServiceProvider.GetRequiredService<IndependentEmptyModule>();
                module.PreConfigureServicesCalled.ShouldBeTrue();
                module.ConfigureServicesCalled.ShouldBeTrue();
                module.PostConfigureServicesCalled.ShouldBeTrue();
                bootstrapper.Initialize();
                module.PreInitializeCalled.ShouldBeTrue();
                module.InitializeCalled.ShouldBeTrue();
                module.PostInitializeCalled.ShouldBeTrue();
                bootstrapper.Shutdown();
                module.ShutdownCalled.ShouldBeTrue();
            }
        }

        [Fact]
        public void Should_Initialize_PlugIn()
        {
            using (var bootstrapper = Bootstrapper.Create<IndependentEmptyModule>(c =>
            {
                c.PlugInSources.AddType<IndependentEmptyPlugInModule>();
                c.UseServiceProviderFactory(new DefaultServiceProviderFactory());
            }))
            {
                var moduleContainer = bootstrapper.ServiceProvider.GetRequiredService<IModuleContainer>();
                moduleContainer.Modules.Count.ShouldBe(3);
                moduleContainer.Modules[0].Type.ShouldBe(typeof(KernelModule));
                moduleContainer.Modules[0].IsLoadedAsPlugIn.ShouldBeFalse();
                moduleContainer.Modules[1].Type.ShouldBe(typeof(IndependentEmptyPlugInModule));
                moduleContainer.Modules[1].IsLoadedAsPlugIn.ShouldBeTrue();
                moduleContainer.Modules[2].Type.ShouldBe(typeof(IndependentEmptyModule));
                moduleContainer.Modules[2].IsLoadedAsPlugIn.ShouldBeFalse();
                var module = bootstrapper.ServiceProvider.GetRequiredService<IndependentEmptyPlugInModule>();
                module.PreConfigureServicesCalled.ShouldBeTrue();
                module.ConfigureServicesCalled.ShouldBeTrue();
                module.PostConfigureServicesCalled.ShouldBeTrue();
                bootstrapper.Initialize();
                module.PreInitializeCalled.ShouldBeTrue();
                module.InitializeCalled.ShouldBeTrue();
                module.PostInitializeCalled.ShouldBeTrue();
                bootstrapper.Shutdown();
                module.ShutdownCalled.ShouldBeTrue();
            }
        }
    }
}
