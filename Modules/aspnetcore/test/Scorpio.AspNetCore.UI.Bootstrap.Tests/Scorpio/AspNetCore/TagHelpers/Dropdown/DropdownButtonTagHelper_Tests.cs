using Microsoft.AspNetCore.Razor.TagHelpers;

using Xunit;
using Shouldly;
namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    /// <summary>
    /// 
    /// </summary>
    public class DropdownButtonTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<DropdownButtonTagHelper>(t =>
            {

            }, (a, c, o) =>
            {
                o.TagName.ShouldBe(a.Tag);
                o.JustHasClasses("dropdown-toggle");
                o.JustHasAttributesAndValues(("data-toggle", "dropdown"));
            });
        }

        [Fact]
        public void Split()
        {
            this.Test<DropdownButtonTagHelper>(t =>
            {
                t.DropdownButtonType = DropdownButtonType.Split;
            }, c => { }, o =>
            {
                o.Attributes.Add("title", "title");
            }, (a, c, o) =>
            {
                o.TagName.ShouldBe(a.Tag);
                o.NotContainsClasses("dropdown-toggle");
                o.PostElement.GetContent().ShouldBe($"<{a.Tag} class=\"dropdown-toggle-split dropdown-toggle\" data-toggle=\"dropdown\" title=\"title\"></{a.Tag}>");
            });
        }
    }
}
