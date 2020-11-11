using System.Threading.Tasks;

using Scorpio.AspNetCore.App;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.Mvc
{
    public class SimpleController_Tests : AspNetCoreMvcTestBase
    {

        [Fact]
        public async Task ActionResult_ContentResult()
        {
            var result = await GetResponseAsStringAsync(
                GetUrl<SimpleController>(nameof(SimpleController.Index))
            );

            result.ShouldBe("Index-Result");
        }

        [Fact]
        public async Task ActionResult_ViewResult()
        {
            var result = await GetResponseAsStringAsync(
                GetUrl<SimpleController>(nameof(SimpleController.About))
            );

            result.Trim().ShouldBe("<h2>About</h2>");
        }


    }
}
