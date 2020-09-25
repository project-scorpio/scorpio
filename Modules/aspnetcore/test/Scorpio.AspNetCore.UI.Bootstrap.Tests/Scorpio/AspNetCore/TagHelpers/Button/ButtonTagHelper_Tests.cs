using Microsoft.AspNetCore.Razor.TagHelpers;

using Scorpio.AspNetCore.UI.Bootstrap;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Button
{
    /// <summary>
    /// 
    /// </summary>
    public class ButtonTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<ButtonTagHelper>((c, o) =>
            {
                o.TagName = "button";
                o.JustHasClasses("btn");
                o.JustHasAttributes("type", "button");
            });
        }
    }
}

