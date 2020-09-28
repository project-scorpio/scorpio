using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;

using Scorpio.AspNetCore.UI.Bootstrap;

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
                o.JustHasClasses("mb-0");
            });
        }
    }
}
