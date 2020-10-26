
using Microsoft.Extensions.DependencyInjection;

using Scorpio.Modularity;

using Shouldly;

using Xunit;

namespace Scorpio
{
    public class BootstrapperCreationOptions_Tests
    {
        [Fact]
        public void PreConfigureServices()
        {
            var services = new ServiceCollection();
            var options = new BootstrapperCreationOptions();
            options.PreConfigureServices(c => c.Properties.Add("test", "value"));
            var context = new ConfigureServicesContext(null, services, null);
            context.Properties.ShouldBeEmpty();
            options.PreConfigureServices(context);
            context.Properties.ShouldHaveSingleItem().Key.ShouldBe("test");
        }

        [Fact]
        public void ConfigureServices()
        {
            var services = new ServiceCollection();
            var options = new BootstrapperCreationOptions();
            options.ConfigureServices(c => c.Properties.Add("test", "value"));
            var context = new ConfigureServicesContext(null, services, null);
            context.Properties.ShouldBeEmpty();
            options.ConfigureServices(context);
            context.Properties.ShouldHaveSingleItem().Key.ShouldBe("test");
        }

        [Fact]
        public void PostConfigureServices()
        {
            var services = new ServiceCollection();
            var options = new BootstrapperCreationOptions();
            options.PostConfigureServices(c => c.Properties.Add("test", "value"));
            var context = new ConfigureServicesContext(null, services, null);
            context.Properties.ShouldBeEmpty();
            options.PostConfigureServices(context);
            context.Properties.ShouldHaveSingleItem().Key.ShouldBe("test");
        }
    }
}
