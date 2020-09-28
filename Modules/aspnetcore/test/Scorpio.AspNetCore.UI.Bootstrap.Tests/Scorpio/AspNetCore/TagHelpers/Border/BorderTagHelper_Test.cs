using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;

using Scorpio.AspNetCore.UI.Bootstrap;

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
                o.JustHasClasses("border");
            });
        }

        [Fact]
        public void None()
        {
            this.Test<BorderTagHelper>(t=>t.Border= BorderType.None,(c, o) =>
            {
                o.JustHasClasses("border-0");
            });
        }
    }
}
