
using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.UI.Bootstrap
{
    public class AspNet_Tests : AspNetCoreUiBootstrapTestBase
    {
        protected override void SetBootstrapperCreationOptions(BootstrapperCreationOptions options) => options.UseAspectCore();

        [Fact]
        public async System.Threading.Tasks.Task HostAsync()
        {
            var act = await Should.NotThrow(() => Client.GetAsync("")).Content.ReadAsStringAsync();
            act.ShouldBe("test");
        }
    }
}
