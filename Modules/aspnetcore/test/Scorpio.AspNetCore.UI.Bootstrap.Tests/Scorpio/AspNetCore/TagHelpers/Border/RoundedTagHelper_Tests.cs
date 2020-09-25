using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;

using Scorpio.AspNetCore.UI.Bootstrap;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Border
{
    /// <summary>
    /// 
    /// </summary>
    public class RoundedTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<RoundedTagHelper>((c, o) =>
            {
                o.JustHasClasses("rounded");
            });
        }

        [Fact]
        public void None()
        {
            this.Test<RoundedTagHelper>(t => t.Rounded =  RoundedType.None, (c, o) =>
            {
                o.JustHasClasses("rounded-0");
            });
        }
    }
}
