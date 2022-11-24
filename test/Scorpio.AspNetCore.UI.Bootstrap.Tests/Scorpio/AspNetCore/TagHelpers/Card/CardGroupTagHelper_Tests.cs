using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Card
{
    public class CardGroupTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<CardGroupTagHelper>((c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.ShouldJustHasClasses("card-group");
                o.PreContent.GetContent().ShouldBe("");
            });
        }

    }
}
