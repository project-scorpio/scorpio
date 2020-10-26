using Shouldly;

using Xunit;

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
                o.ShouldJustHasClasses("card-footer");
                o.PreContent.GetContent().ShouldBe("");
            });
        }

    }
}
