using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Card
{
    public class CardHeaderTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {

        [Fact]
        public void Default()
        {
            this.Test<CardHeaderTagHelper>((c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.ShouldJustHasClasses("card-header");
                o.PreContent.GetContent().ShouldBe("");
            });
        }

    }
}
