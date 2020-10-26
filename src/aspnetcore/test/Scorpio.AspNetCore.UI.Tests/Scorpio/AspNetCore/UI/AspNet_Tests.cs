
using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.UI
{
    public class AspNet_Tests : AspNetCoreUiTestBase
    {
        protected override void SetBootstrapperCreationOptions(BootstrapperCreationOptions options)
        {
            options.UseAspectCore();
        }

        [Fact]
        public async System.Threading.Tasks.Task HostAsync()
        {
            var act = await Should.NotThrow(() => Client.GetAsync("")).Content.ReadAsStringAsync();
            act.ShouldBe("test");
        }
    }
}
