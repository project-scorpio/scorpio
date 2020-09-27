using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;
using Shouldly;

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
                o.JustHasClasses("card-img");
                o.PreContent.GetContent().ShouldBe("");
            });
        }

        [Fact]
        public void Position()
        {
            this.Test<CardImageTagHelper>(t=>t.Position= CardImagePosition.Top,(c, o) =>
            {
                o.TagName.ShouldBe("img");
                o.JustHasClasses("card-img-top");
                o.PreContent.GetContent().ShouldBe("");
            });
        }

    }
}
