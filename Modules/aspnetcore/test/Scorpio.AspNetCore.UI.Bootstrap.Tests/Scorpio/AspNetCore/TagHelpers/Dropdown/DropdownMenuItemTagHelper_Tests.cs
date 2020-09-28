using Microsoft.AspNetCore.Razor.TagHelpers;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("a", ParentTag = "dropdown-menu")]
    [HtmlTargetElement("button", ParentTag = "dropdown-menu")]
    public class DropdownMenuItemTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<DropdownMenuItemTagHelper>(t =>
            {

            }, (a, c, o) =>
            {
                o.TagName.ShouldBe(a.Tag);
                o.JustHasClasses("dropdown-item");
            });
        }

        [Fact]
        public void Active()
        {
            this.Test<DropdownMenuItemTagHelper>(t =>
            {
                t.Status = DropdownItemStatus.Active;
            }, (a, c, o) =>
            {
                o.TagName.ShouldBe(a.Tag);
                o.JustHasClasses("dropdown-item", "active");
            });
        }

        [Fact]
        public void Disabled()
        {
            this.Test<DropdownMenuItemTagHelper>(t =>
            {
                t.Status = DropdownItemStatus.Disabled;
            }, (a, c, o) =>
            {
                o.TagName.ShouldBe(a.Tag);
                o.JustHasClasses("dropdown-item", "disabled");
            });
        }


    }
}
