using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Color
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement(Attributes = "txt-color")]
    public class TextColorTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<TextColorTagHelper>(t =>
            {

            }, (c, o) =>
            {
                o.JustHasClasses("text-default");
            });
        }

        [Fact]
        public void Primary()
        {
            this.Test<TextColorTagHelper>(t =>
            {
                t.TextColor =  TextColorType.Primary;
            }, (c, o) =>
            {
                o.JustHasClasses("text-primary");
            });
        }
    }
}
