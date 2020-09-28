using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;

using Scorpio.AspNetCore.UI.Bootstrap;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Alerts
{
    public class AlertHeaderTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Test()
        {
            this.Test<AlertHeaderTagHelper>((a, c, o) =>{
                o.JustHasClasses("alert-heading");
                o.TagName.ShouldBe(a.Tag);
                a.ParentTag.ShouldBe("alert");
            });
        }
    }
}
