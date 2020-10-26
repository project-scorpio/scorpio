
using Scorpio.Modularity;
using Scorpio.TestBase;

using Shouldly;

using Xunit;

namespace Scorpio.Runtime
{
    public class AmbientDataContextAmbientScopeProvider_Test : IntegratedTest<IndependentEmptyModule>
    {
        [Fact]
        public void GetValue()
        {
            var provider = GetRequiredService<IAmbientScopeProvider<string>>();
            provider.GetValue("GetValue").ShouldBeNull();
        }

        [Fact]
        public void BeginScope()
        {
            var provider = GetRequiredService<IAmbientScopeProvider<string>>();
            using (provider.BeginScope("Scope", "OuterValue"))
            {
                provider.GetValue("Scope").ShouldBe("OuterValue");
                using (provider.BeginScope("Scope", "InnerValue"))
                {
                    provider.GetValue("Scope").ShouldBe("InnerValue");
                }
                provider.GetValue("Scope").ShouldBe("OuterValue");
            }
            provider.GetValue("GetValue").ShouldBeNull();
        }
    }
}
