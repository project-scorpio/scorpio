using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;
using Shouldly;

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
                o.JustHasClasses("card-title");
                o.PreContent.GetContent().ShouldBe("");
            });
        }

        [Fact]
        public void H1()
        {
            this.Test<CardTitleTagHelper>(t=>t.Heading= HtmlHeadingType.H1,(c, o) =>
            {
                o.TagName.ShouldBe("h1");
                o.JustHasClasses("card-title");
                o.PreContent.GetContent().ShouldBe("");
            });
        }

    }
}
