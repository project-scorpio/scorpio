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
    public class BlockquoteTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Test()
        {
            this.Test<BlockquoteTagHelper>((a, c, o) =>
            {
                o.TagName.ShouldBe("blockquote");
                o.JustHasClasses("blockquote");
            });
        }
    }
}
