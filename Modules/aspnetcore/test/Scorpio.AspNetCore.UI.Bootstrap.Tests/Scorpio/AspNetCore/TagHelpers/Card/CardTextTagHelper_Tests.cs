using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;
using Shouldly;

namespace Scorpio.AspNetCore.TagHelpers.Card
{
    /// <summary>
    /// 
    /// </summary>
    public class CardTextTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<CardTextTagHelper>((c, o) =>
            {
                o.TagName.ShouldBe("p");
                o.JustHasClasses("card-text");
                o.PreContent.GetContent().ShouldBe("");
            });
        }



    }
}
