
using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Blockquote
{
    /// <summary>
    /// 
    /// </summary>
    public class BlockquoteTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Test()
        {
            this.Test<BlockquoteTagHelper>((a, c, o) =>
            {
                o.TagName.ShouldBe("blockquote");
                o.ShouldJustHasClasses("blockquote");
            });
        }
    }
}
