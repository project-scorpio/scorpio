
using Microsoft.AspNetCore.Razor.TagHelpers;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Border
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement(HtmlTargetElementAttribute.ElementCatchAllTarget, Attributes = "border")]
    public class BorderTagHelper_Test : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<BorderTagHelper>((c, o) =>
            {
                o.ShouldJustHasClasses("border");
            });
        }

        [Fact]
        public void None()
        {
            this.Test<BorderTagHelper>(t => t.Border = BorderType.None, (c, o) =>
                {
                    o.ShouldJustHasClasses("border-0");
                });
        }
    }
}
