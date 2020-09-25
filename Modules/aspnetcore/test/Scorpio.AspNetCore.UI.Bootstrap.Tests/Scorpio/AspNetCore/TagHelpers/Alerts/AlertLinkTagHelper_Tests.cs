using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;

using Scorpio.AspNetCore.UI.Bootstrap;

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
            this.Test<AlertLinkTagHelper>((c,o)=>
            {
                o.JustHasClasses("alert-link");
                o.TagName.ShouldBe("a");
            });
        }
    }
}
