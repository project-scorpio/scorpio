
using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Alerts
{
    /// <summary>
    /// 
    /// </summary>
    public class AlertLinkTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Process()
        {
            this.Test<AlertLinkTagHelper>((c, o) =>
            {
                o.ShouldJustHasClasses("alert-link");
                o.TagName.ShouldBe("a");
            });
        }
    }
}
