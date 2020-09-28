using Microsoft.AspNetCore.Razor.TagHelpers;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Collapse
{
    /// <summary>
    /// 
    /// </summary>
    public class CollapseTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<CollapseTagHelper>((a,c, o) =>
            {
                if (a.Tag== "collapse")
                {
                    o.TagName.ShouldBe("div");
                }
                else
                {
                    o.TagName.ShouldBe("*");
                }
                o.JustHasClasses("collapse");
            });
        }

        [Fact]
        public void Show()
        {
            this.Test<CollapseTagHelper>(t => t.Collapse= CollapseType.Show, (a, c, o) =>
            {
                if (a.Tag == "collapse")
                {
                    o.TagName.ShouldBe("div");
                }
                else
                {
                    o.TagName.ShouldBe("*");
                }
                o.JustHasClasses("collapse","show");
            });
        }

    }
}
