
using Microsoft.AspNetCore.Razor.TagHelpers;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Card
{
    /// <summary>
    /// 
    /// </summary>
    public class CardSubTitleTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<CardSubTitleTagHelper>((c, o) =>
            {
                o.TagName.ShouldBe("h6");
                o.ShouldJustHasClasses("card-subtitle");
                o.PreContent.GetContent().ShouldBe("");
            });
        }

        [Fact]
        public void H1()
        {
            this.Test<CardSubTitleTagHelper>(t => t.Heading = HtmlHeadingType.H1, (c, o) =>
               {
                   o.TagName.ShouldBe("h1");
                   o.ShouldJustHasClasses("card-subtitle");
                   o.PreContent.GetContent().ShouldBe("");
               });
        }

    }
}
