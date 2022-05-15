
using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Badge
{
    /// <summary>
    /// 
    /// </summary>
    public class BadgeTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<BadgeTagHelper>((a, c, o) =>
            {
                o.TagName.ShouldBe(a.Tag);
                o.ShouldJustHasClasses("badge");
            });
        }

        [Fact]
        public void BadgeType()
        {
            this.Test<BadgeTagHelper>(t => t.BadgeType = Badge.BadgeType.Primary, (a, c, o) =>
            {
                o.TagName.ShouldBe(a.Tag);
                o.ShouldJustHasClasses("badge", "badge-primary");
            });
        }

        [Fact]
        public void BadgePill()
        {
            this.Test<BadgeTagHelper>(t => t.BadgePill = true, (a, c, o) =>
            {
                o.TagName.ShouldBe(a.Tag);
                o.ShouldJustHasClasses("badge", "badge-pill");
            });
        }

        [Fact]
        public void All()
        {
            this.Test<BadgeTagHelper>(t =>
            {
                t.BadgePill = true;
                t.BadgeType = Badge.BadgeType.Primary;
            }, (a, c, o) =>
            {
                o.TagName.ShouldBe(a.Tag);
                o.ShouldJustHasClasses("badge", "badge-pill", "badge-primary");
            });
        }
    }
}
