using Microsoft.AspNetCore.Razor.TagHelpers;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("divider", ParentTag = "dropdown-menu")]
    public class DropdownMenuDividersTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<DropdownMenuDividersTagHelper>(t =>
            {

            }, (a, c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.JustHasClasses("dropdown-divider");
            });
        }

    }
}
