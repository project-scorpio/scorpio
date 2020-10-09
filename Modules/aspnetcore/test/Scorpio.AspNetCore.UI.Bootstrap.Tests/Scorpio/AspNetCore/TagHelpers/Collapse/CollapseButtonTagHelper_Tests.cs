
using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Collapse
{
    /// <summary>
    /// 
    /// </summary>
    public class CollapseButtonTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<CollapseButtonTagHelper>(t => t.Target = "target", (a, c, o) =>
                 {
                     switch (a.Tag)
                     {
                         case "a":
                             o.ShouldJustHasAttributesAndValues(("data-toggle", "collapse"), ("aria-expanded", "false"), ("aria-controls", "target"), ("href", "target"));
                             break;
                         case "button":
                             o.ShouldJustHasAttributesAndValues(("data-toggle", "collapse"), ("aria-expanded", "false"), ("aria-controls", "target"), ("data-target", "target"));
                             break;
                         default:
                             break;
                     }
                 });
        }
    }
}
