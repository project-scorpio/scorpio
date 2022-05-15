using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Card
{
    /// <summary>
    /// 
    /// </summary>
    public class CardTextTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<CardTextTagHelper>((c, o) =>
            {
                o.TagName.ShouldBe("p");
                o.ShouldJustHasClasses("card-text");
                o.PreContent.GetContent().ShouldBe("");
            });
        }



    }
}
