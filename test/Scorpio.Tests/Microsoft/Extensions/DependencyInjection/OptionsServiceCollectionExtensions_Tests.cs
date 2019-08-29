using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Shouldly;
using Scorpio.DependencyInjection;
using Scorpio.Conventional;
using Scorpio.DependencyInjection.Conventional;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public class OptionsServiceCollectionExtensions_Tests
    {
        [Fact]
        public void Options()
        {
            var services = new ServiceCollection();
            services.Options<ServiceProviderOptions>().ShouldBeOfType<OptionsBuilder<ServiceProviderOptions>>().ShouldNotBeNull();
        }
        [Fact]
        public void NameOptions()
        {
            var services = new ServiceCollection();
            services.Options<ServiceProviderOptions>("Test").ShouldBeOfType<OptionsBuilder<ServiceProviderOptions>>().Name.ShouldBe("Test");
        }

        [Fact]
        public void PreConfigure()
        {
            var services = new ServiceCollection();
            services.PreConfigure<ServiceProviderOptions>(c => { });
            services.ShouldContainSingleton(typeof(IPreConfigureOptions<ServiceProviderOptions>), typeof(PreConfigureOptions<ServiceProviderOptions>));
        }
        [Fact]
        public void PreConfigureAll()
        {
            var services = new ServiceCollection();
            services.PreConfigureAll<ServiceProviderOptions>(c => { });
            services.ShouldContainSingleton(typeof(IPreConfigureOptions<ServiceProviderOptions>), typeof(PreConfigureOptions<ServiceProviderOptions>));
        }
    }


}
