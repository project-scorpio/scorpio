using Microsoft.AspNetCore.Razor.TagHelpers;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("dropdown-menu", ParentTag = "dropdown")]
    public class DropdownMenuTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<DropdownMenuTagHelper>(t =>
            {

            }, (a, c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.ShouldJustHasClasses("dropdown-menu");
            });
        }

    }
}
