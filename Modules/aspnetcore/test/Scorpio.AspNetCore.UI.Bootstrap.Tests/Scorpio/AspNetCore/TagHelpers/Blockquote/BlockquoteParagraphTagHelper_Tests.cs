
using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Blockquote
{
    /// <summary>
    /// 
    /// </summary>
    public class BlockquoteParagraphTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Test()
        {
            this.Test<BlockquoteParagraphTagHelper>((a, c, o) =>
            {
                a.ParentTag.ShouldBe("blockquote");
                o.TagName.ShouldBe(a.Tag);
                o.ShouldJustHasClasses("mb-0");
            });
        }
    }
}
