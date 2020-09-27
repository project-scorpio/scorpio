using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;
using Shouldly;

namespace Scorpio.AspNetCore.TagHelpers.Card
{
    public class CardGroupTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<CardGroupTagHelper>((c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.JustHasClasses("card-group");
                o.PreContent.GetContent().ShouldBe("");
            });
        }

    }
}
