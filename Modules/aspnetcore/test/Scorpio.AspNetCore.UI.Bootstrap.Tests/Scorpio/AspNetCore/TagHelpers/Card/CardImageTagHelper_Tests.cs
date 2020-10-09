using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Card
{
    /// <summary>
    /// 
    /// </summary>
    public class CardImageTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<CardImageTagHelper>((c, o) =>
            {
                o.TagName.ShouldBe("img");
                o.ShouldJustHasClasses("card-img");
                o.PreContent.GetContent().ShouldBe("");
            });
        }

        [Fact]
        public void Position()
        {
            this.Test<CardImageTagHelper>(t => t.Position = CardImagePosition.Top, (c, o) =>
                {
                    o.TagName.ShouldBe("img");
                    o.ShouldJustHasClasses("card-img-top");
                    o.PreContent.GetContent().ShouldBe("");
                });
        }

    }
}
