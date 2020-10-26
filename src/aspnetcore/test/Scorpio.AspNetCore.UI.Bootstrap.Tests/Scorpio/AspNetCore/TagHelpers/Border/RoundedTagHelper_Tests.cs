
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
                o.ShouldJustHasClasses("rounded");
            });
        }

        [Fact]
        public void None()
        {
            this.Test<RoundedTagHelper>(t => t.Rounded = RoundedType.None, (c, o) =>
           {
               o.ShouldJustHasClasses("rounded-0");
           });
        }
    }
}
