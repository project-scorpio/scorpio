using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;
using Shouldly;

namespace Scorpio.AspNetCore.TagHelpers.Card
{
    /// <summary>
    /// 
    /// </summary>
    public class CardTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<CardTagHelper>((c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.JustHasClasses("card");
                o.PreContent.GetContent().ShouldBe("");
            });
        }

    }
}
