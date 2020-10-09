
using Microsoft.AspNetCore.Razor.TagHelpers;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Card
{
    /// <summary>
    /// 
    /// </summary>
    public class CardTitleTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<CardTitleTagHelper>((c, o) =>
            {
                o.TagName.ShouldBe("h5");
                o.ShouldJustHasClasses("card-title");
                o.PreContent.GetContent().ShouldBe("");
            });
        }

        [Fact]
        public void H1()
        {
            this.Test<CardTitleTagHelper>(t => t.Heading = HtmlHeadingType.H1, (c, o) =>
                {
                    o.TagName.ShouldBe("h1");
                    o.ShouldJustHasClasses("card-title");
                    o.PreContent.GetContent().ShouldBe("");
                });
        }

    }
}
