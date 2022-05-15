
using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Alerts
{
    public class AlertHeaderTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Test()
        {
            this.Test<AlertHeaderTagHelper>((a, c, o) =>
            {
                o.ShouldJustHasClasses("alert-heading");
                o.TagName.ShouldBe(a.Tag);
                a.ParentTag.ShouldBe("alert");
            });
        }
    }
}
