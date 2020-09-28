using Microsoft.AspNetCore.Razor.TagHelpers;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    /// <summary>
    /// 
    /// </summary>
    public class DropdownMenuHeaderTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<DropdownMenuHeaderTagHelper>(t =>
            {

            }, (a, c, o) =>
            {
                o.TagName.ShouldBe("h6");
                o.JustHasClasses("dropdown-header");
            });
        }

    }
}
