
using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Button
{
    /// <summary>
    /// 
    /// </summary>
    public class ButtonToolbarTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<ButtonToolbarTagHelper>((c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.ShouldJustHasClasses("btn-toolbar");
                o.ShouldJustHasAttributesAndValues(("role", "toolbar"));
            });
        }
    }
}
