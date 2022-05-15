using Scorpio.Options;

using Shouldly;

using Xunit;

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
