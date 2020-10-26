
using Shouldly;

using Xunit;
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
                o.ShouldJustHasClasses("dropdown-toggle");
                o.ShouldJustHasAttributesAndValues(("data-toggle", "dropdown"));
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
                o.ShouldNotContainsClasses("dropdown-toggle");
                o.PostElement.GetContent().ShouldBe($"<{a.Tag} class=\"dropdown-toggle-split dropdown-toggle\" data-toggle=\"dropdown\" title=\"title\"></{a.Tag}>");
            });
        }
    }
}
