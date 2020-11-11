using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    /// <summary>
    /// 
    /// </summary>
    public class DropdownTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {

        [Fact]
        public void Default()
        {
            this.Test<DropdownTagHelper>(t =>
            {

            }, (a, c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.ShouldJustHasClasses("dropdown", "btn-group");
            });
        }

        [Fact]
        public void Left()
        {
            this.Test<DropdownTagHelper>(t => t.Direction = Direction.Left, (a, c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.ShouldJustHasClasses("dropdown", "btn-group", "dropleft");
            });
        }

        [Fact]
        public void Right()
        {
            this.Test<DropdownTagHelper>(t => t.Direction = Direction.Right, (a, c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.ShouldJustHasClasses("dropdown", "btn-group", "dropright");
            });
        }

        [Fact]
        public void Up()
        {
            this.Test<DropdownTagHelper>(t => t.Direction = Direction.Up, (a, c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.ShouldJustHasClasses("dropdown", "btn-group", "dropup");
            });
        }


    }
}
