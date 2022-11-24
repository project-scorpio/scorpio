using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Card
{
    /// <summary>
    /// 
    /// </summary>
    public class CardTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<CardTagHelper>((c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.ShouldJustHasClasses("card");
                o.PreContent.GetContent().ShouldBe("");
            });
        }

    }
}
