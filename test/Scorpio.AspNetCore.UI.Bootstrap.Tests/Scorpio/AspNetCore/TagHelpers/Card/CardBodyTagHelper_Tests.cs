
using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Card
{
    public class CardBodyTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<CardBodyTagHelper>((c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.ShouldJustHasClasses("card-body");
                o.PreContent.GetContent().ShouldBe("");
            });
        }

        [Fact]
        public void Title()
        {
            this.Test<CardBodyTagHelper>(t => t.Title = "TestTitle", (c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.ShouldJustHasClasses("card-body");
                o.PreContent.GetContent().ShouldBe("<h5 class=\"card-title\">TestTitle</h5>");
            });
        }
        [Fact]
        public void SUbTitle()
        {
            this.Test<CardBodyTagHelper>(t => t.SubTilte = "TestSubTitle", (c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.ShouldJustHasClasses("card-body");
                o.PreContent.GetContent().ShouldBe("<h6 class=\"card-subtitle\">TestSubTitle</h6>");
            });
        }
        [Fact]
        public void TitleAndSubTitle()
        {
            this.Test<CardBodyTagHelper>(t =>
            {
                t.Title = "TestTitle";
                t.SubTilte = "TestSubTitle";
            }, (c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.ShouldJustHasClasses("card-body");
                o.PreContent.GetContent().ShouldBe("<h5 class=\"card-title\">TestTitle</h5><h6 class=\"card-subtitle\">TestSubTitle</h6>");
            });
        }
    }
}
