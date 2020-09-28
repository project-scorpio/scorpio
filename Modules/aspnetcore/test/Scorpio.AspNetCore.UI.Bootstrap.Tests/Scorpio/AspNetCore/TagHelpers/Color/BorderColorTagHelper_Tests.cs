using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Color
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement(Attributes = "bd-color")]
    public class BorderColorTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<BorderColorTagHelper>(t =>
            {

            }, (c, o) =>
            {
                o.JustHasClasses("border-default");
            });
        }

        [Fact]
        public void Primary()
        {
            this.Test<BorderColorTagHelper>(t =>
            {
                t.BorderColor =  BorderColorType.Primary;
            }, (c, o) =>
            {
                o.JustHasClasses("border-primary");
            });
        }
    }
}
