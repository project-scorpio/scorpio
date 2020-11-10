using Microsoft.AspNetCore.Razor.TagHelpers;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Form
{
    /// <summary>
    /// 
    /// </summary>

    public class CheckInputTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<CheckInputTagHelper>(t =>
            {

            }, c => { }, o => o.AddAttribute("id", "id"), (a, c, o) =>
            {
                o.TagName.ShouldBe("input");
                o.ShouldJustHasClasses("form-check-input");
                o.PreElement.GetContent().ShouldBe("<div class=\"form-check\">");
                o.PostElement.GetContent().ShouldBe("<label class=\"form-check-label\" for=\"id\"></label></div>");
            });
        }

        [Fact]
        public void Title()
        {
            this.Test<CheckInputTagHelper>(t => t.Title = "Title", c => { }, o => o.AddAttribute("id", "id"), (a, c, o) =>
            {
                o.TagName.ShouldBe("input");
                o.ShouldJustHasClasses("form-check-input");
                o.PreElement.GetContent().ShouldBe("<div class=\"form-check\">");
                o.PostElement.GetContent().ShouldBe("<label class=\"form-check-label\" for=\"id\">Title</label></div>");
            });
        }

        [Fact]
        public void Text()
        {
            this.Test<CheckInputTagHelper>(t => t.Text = "Text", c => { }, o => o.AddAttribute("id", "id"), (a, c, o) =>
            {
                o.TagName.ShouldBe("input");
                o.ShouldJustHasClasses("form-check-input");
                o.PreElement.GetContent().ShouldBe("<div class=\"form-check\">");
                o.PostElement.GetContent().ShouldBe("<label class=\"form-check-label\" for=\"id\">Text</label></div>");
            });
        }

        [Fact]
        public void TitleAndText()
        {
            this.Test<CheckInputTagHelper>(t =>
            {
                t.Title = "Title";
                t.Text = "Text";
            }, c => { }, o => o.AddAttribute("id", "id"), (a, c, o) =>
            {
                o.TagName.ShouldBe("input");
                o.ShouldJustHasClasses("form-check-input");
                o.PreElement.GetContent().ShouldBe("<div class=\"form-check\">");
                o.PostElement.GetContent().ShouldBe("<label class=\"form-check-label\" for=\"id\">Text</label></div>");
            });
        }

    }
}
