using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;
using Shouldly;

namespace Scorpio.AspNetCore.TagHelpers.Carousel
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement(ParentTag = "carousel")]
    public class CarouselItemTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<CarouselItemTagHelper>((c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.JustHasClasses("carousel-item");
                o.PreContent.GetContent().ShouldBe("");
            });
        }

        [Fact]
        public void Active()
        {
            this.Test<CarouselItemTagHelper>(t => t.IsActive = true,(c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.JustHasClasses("carousel-item","active");
                o.PreContent.GetContent().ShouldBe("");
            });
        }

    }
}
