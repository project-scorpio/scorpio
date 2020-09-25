using Scorpio.AspNetCore.UI.Bootstrap;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Button
{
    /// <summary>
    /// 
    /// </summary>
    public class ButtonGroupTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<ButtonGroupTagHelper>((c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.JustHasClasses("btn-group");
                o.JustHasAttributes("role", "group");
            });
        }

        [Fact]
        public void Direction()
        {
            this.Test<ButtonGroupTagHelper>(t => t.Direction = ButtonGroupDirection.Vertical, (c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.JustHasClasses("btn-group-vertical");
                o.JustHasAttributes("role", "group");
            });
        }
        [Fact]
        public void Size()
        {
            this.Test<ButtonGroupTagHelper>(t => t.Size= TagHelpers.Size.Large, (c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.JustHasClasses("btn-group", "btn-group-lg");
                o.JustHasAttributes("role", "group");
            });
        }
    }
}
