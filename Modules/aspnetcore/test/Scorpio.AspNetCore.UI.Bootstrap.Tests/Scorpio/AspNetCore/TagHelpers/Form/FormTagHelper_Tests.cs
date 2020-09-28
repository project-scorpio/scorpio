using Microsoft.AspNetCore.Razor.TagHelpers;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Form
{
    /// <summary>
    /// 
    /// </summary>
    public class FormTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<FormTagHelper>(t =>
            {
            }, c => { }, o => o.AddAttribute("id", "id"), (a, c, o) =>
            {
                //o.TagName.ShouldBe("input");
                //o.JustHasClasses("form-check-input");
                //o.PreElement.GetContent().ShouldBe("<div class=\"form-check\">");
                //o.PostElement.GetContent().ShouldBe("<label class=\"form-check-label\" for=\"id\">Text</label></div>");
            });
        }


    }
}
