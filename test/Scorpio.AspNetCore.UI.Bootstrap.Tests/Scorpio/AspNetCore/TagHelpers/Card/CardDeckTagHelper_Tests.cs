
using Shouldly;

using Xunit;
namespace Scorpio.AspNetCore.TagHelpers.Card
{
    public class CardDeckTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<CardDeckTagHelper>((c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.ShouldJustHasClasses("card-deck");
                o.PreContent.GetContent().ShouldBe("");
            });
        }


    }
}
