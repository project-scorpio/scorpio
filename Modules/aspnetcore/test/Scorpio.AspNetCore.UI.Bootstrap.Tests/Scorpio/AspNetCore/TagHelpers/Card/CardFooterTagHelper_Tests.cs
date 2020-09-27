using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;

using Scorpio.AspNetCore.UI.Bootstrap;
using Xunit;
using Shouldly;

namespace Scorpio.AspNetCore.TagHelpers.Card
{
    public class CardFooterTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {

        [Fact]
        public void Default()
        {
            this.Test<CardFooterTagHelper>((c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.JustHasClasses("card-footer");
                o.PreContent.GetContent().ShouldBe("");
            });
        }

    }
}
