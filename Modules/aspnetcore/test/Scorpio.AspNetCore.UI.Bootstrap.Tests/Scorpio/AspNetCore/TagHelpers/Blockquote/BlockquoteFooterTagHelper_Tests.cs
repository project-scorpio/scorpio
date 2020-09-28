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
    public class BlockquoteFooterTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Test()
        {
            this.Test<BlockquoteFooterTagHelper>((a, c, o) =>
            {
                a.ParentTag.ShouldBe("blockquote");
                o.TagName.ShouldBe(a.Tag);
                o.JustHasClasses("blockquote-footer");
            });
        }
       
    }
}
