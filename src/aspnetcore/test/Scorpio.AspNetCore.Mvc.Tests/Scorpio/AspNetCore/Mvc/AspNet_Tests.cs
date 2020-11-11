
using Scorpio.AspNetCore.TestBase;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.Mvc
{
    public class AspNet_Tests : AspNetCoreIntegratedTestBase<TestModule, Startup>
    {

        [Fact]
        public void Host() => Should.NotThrow(() => Client.GetAsync("/Test")).StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
    }
}
