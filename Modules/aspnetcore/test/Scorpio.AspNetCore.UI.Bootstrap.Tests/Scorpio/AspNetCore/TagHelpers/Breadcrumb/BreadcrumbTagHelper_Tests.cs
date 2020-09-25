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
                o.JustHasAttributes("aria-label", "breadcrumb");
            });
        }

        [Fact]
        public void Item()
        {
            var tag = this.GetTagHelper<BreadcrumbTagHelper>();
            var (c, o) = tag.GetContext("breadcrumb");
            this.GetTagHelper<BreadcrumbItemTagHelper>().Test(c, o, (c, d) => { });
            tag.Test(c, o, (c, o) =>
            {
                o.TagName.ShouldBe("nav");
                o.JustHasAttributes("aria-label", "breadcrumb");
                o.Content.GetContent().ShouldBe("<li class=\"breadcrumb-item active\" aria-current=\"page\"></li>\r\n");
            });
        }
    }
}
