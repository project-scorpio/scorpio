
using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore
{
    public class AspNet_Tests : AspNetCoreTestBase
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
