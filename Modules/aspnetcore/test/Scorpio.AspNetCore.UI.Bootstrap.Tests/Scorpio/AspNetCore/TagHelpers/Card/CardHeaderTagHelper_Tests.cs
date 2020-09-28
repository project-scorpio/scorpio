using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;
using Shouldly;

namespace Scorpio.AspNetCore.TagHelpers.Card
{
    public class CardHeaderTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {

        [Fact]
        public void Default()
        {
            this.Test<CardHeaderTagHelper>((c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.JustHasClasses("card-header");
                o.PreContent.GetContent().ShouldBe("");
            });
        }

    }
}
