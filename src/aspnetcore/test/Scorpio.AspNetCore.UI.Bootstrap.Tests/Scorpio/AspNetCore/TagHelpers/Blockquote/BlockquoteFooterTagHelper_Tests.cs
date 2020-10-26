
using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Blockquote
{
    /// <summary>
    /// 
    /// </summary>
    public class BlockquoteFooterTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Test()
        {
            this.Test<BlockquoteFooterTagHelper>((a, c, o) =>
            {
                a.ParentTag.ShouldBe("blockquote");
                o.TagName.ShouldBe(a.Tag);
                o.ShouldJustHasClasses("blockquote-footer");
            });
        }

    }
}
