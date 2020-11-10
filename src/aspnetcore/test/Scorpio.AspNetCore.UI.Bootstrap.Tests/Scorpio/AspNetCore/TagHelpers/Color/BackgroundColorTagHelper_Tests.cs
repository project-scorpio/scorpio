
using Microsoft.AspNetCore.Razor.TagHelpers;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Color
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement(Attributes = "bg-color")]
    public class BackgroundColorTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<BackgroundColorTagHelper>(t =>
            {

            }, (c, o) => o.ShouldJustHasClasses("bg-default"));
        }

        [Fact]
        public void Primary() => this.Test<BackgroundColorTagHelper>(t => t.BackgroundColor = BackgroundColorType.Primary, (c, o) => o.ShouldJustHasClasses("bg-primary"));
    }
}
