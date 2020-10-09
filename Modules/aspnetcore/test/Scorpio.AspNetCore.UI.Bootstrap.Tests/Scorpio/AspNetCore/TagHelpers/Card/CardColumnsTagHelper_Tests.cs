
using Shouldly;

using Xunit;
namespace Scorpio.AspNetCore.TagHelpers.Card
{
    public class CardColumnsTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<CardColumnsTagHelper>((c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.ShouldJustHasClasses("card-columns");
                o.PreContent.GetContent().ShouldBe("");
            });
        }

    }
}
