using Scorpio.AspNetCore.UI.Bootstrap;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Breadcrumb
{
    /// <summary>
    /// 
    /// </summary>
    public class BreadcrumbTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<BreadcrumbTagHelper>((c, o) =>
            {
                o.TagName.ShouldBe("nav");
                o.HasAttributeAndJustContainsValues("aria-label", "breadcrumb");
            });
        }

        [Fact]
        public void Item()
        {
            var tag = this.GetTagHelper<BreadcrumbTagHelper>();
            var c = tag.GetContext();
            this.GetTagHelper<BreadcrumbItemTagHelper>().Test(c, "breadcrumb-item", (c, d) => { });
            tag.Test(c, "breadcrumb", (c, o) =>
            {
                o.TagName.ShouldBe("nav");
                o.HasAttributeAndJustContainsValues("aria-label", "breadcrumb");
                o.Content.GetContent().ShouldBe("<li class=\"breadcrumb-item active\" aria-current=\"page\"></li>\r\n");
            });
        }
    }
}
