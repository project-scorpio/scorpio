using System.Collections.Generic;
using System.Text;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Carousel
{
    /// <summary>
    /// 
    /// </summary>
    public class CarouselTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<CarouselTagHelper>(t => t.Id = "id", (c, o) =>
                 {
                     o.TagName.ShouldBe("div");
                     o.ShouldJustHasClasses("carousel", "slide");
                     o.ShouldJustHasAttributesAndValues(("id", "id"), ("data-ride", "carousel"));
                     o.PreContent.GetContent().ShouldBe("<div class=\"carousel-inner\">");
                     o.PostContent.GetContent().ShouldBe("</div>");
                 });
        }

        [Fact]
        public void Fade()
        {
            this.Test<CarouselTagHelper>(t =>
            {
                t.Id = "id";
                t.Crossfade = true;
            }, (c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.ShouldJustHasClasses("carousel", "slide", "carousel-fade");
                o.ShouldJustHasAttributesAndValues(("id", "id"), ("data-ride", "carousel"));
                o.PreContent.GetContent().ShouldBe("<div class=\"carousel-inner\">");
                o.PostContent.GetContent().ShouldBe("</div>");
            });
        }

        [Fact]
        public void Indicators()
        {
            this.Test<CarouselTagHelper>(t =>
            {
                t.Id = "id";
                t.Indicators = true;
            }, (c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.ShouldJustHasClasses("carousel", "slide");
                o.ShouldJustHasAttributesAndValues(("id", "id"), ("data-ride", "carousel"));
                o.PreContent.GetContent().ShouldBe("<ol class=\"carousel-indicators\"></ol>\r\n<div class=\"carousel-inner\">");
                o.PostContent.GetContent().ShouldBe("</div>");
            });
        }
        [Fact]
        public void Controls()
        {
            var html = new StringBuilder("");
            html.AppendLine("<a class=\"carousel-control-prev\" href=\"#id\" role=\"button\" data-slide=\"prev\">");
            html.AppendLine("<span class=\"carousel-control-prev-icon\" aria-hidden=\"true\"></span>");
            html.AppendLine("<span class=\"sr-only\">Previous</span>");
            html.AppendLine("</a>");
            html.AppendLine("<a class=\"carousel-control-next\" href=\"#id\" role=\"button\" data-slide=\"next\">");
            html.AppendLine("<span class=\"carousel-control-next-icon\" aria-hidden=\"true\"></span>");
            html.AppendLine("<span class=\"sr-only\">Next</span>");
            html.AppendLine("</a>");

            this.Test<CarouselTagHelper>(t =>
            {
                t.Id = "id";
                t.Controls = true;
            }, (c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.ShouldJustHasClasses("carousel", "slide");
                o.ShouldJustHasAttributesAndValues(("id", "id"), ("data-ride", "carousel"));
                o.PreContent.GetContent().ShouldBe("<div class=\"carousel-inner\">");
                o.PostContent.GetContent().ShouldBe("</div>" + html.ToString());
            });
        }

        [Fact]
        public void Items()
        {
            var tag = this.GetTagHelper<CarouselTagHelper>(t => t.Id = "id");
            var c = tag.GetContext();
            this.GetTagHelper<CarouselItemTagHelper>().Test(c, "div", (c, o) =>
              {
                  o.TagName.ShouldBe("div");
                  o.ShouldJustHasClasses("carousel-item");
                  o.PreContent.GetContent().ShouldBe("");
              });
            tag.Test(c, "div", (c, o) =>
              {
                  o.TagName.ShouldBe("div");
                  o.ShouldJustHasClasses("carousel", "slide");
                  o.ShouldJustHasAttributesAndValues(("id", "id"), ("data-ride", "carousel"));
                  o.PreContent.GetContent().ShouldBe("<div class=\"carousel-inner\">");
                  o.PostContent.GetContent().ShouldBe("</div>");
                  c.GetValue<List<CarouselItem>>(CarouselTagHelper.CAROUSEL_ITEMS_CONTENT).ShouldHaveSingleItem().IsActive.ShouldBeTrue();
              });
        }

        [Fact]
        public void ItemsAndIndicatorsAndControls()
        {
            var html = new StringBuilder("");
            html.AppendLine("<a class=\"carousel-control-prev\" href=\"#id\" role=\"button\" data-slide=\"prev\">");
            html.AppendLine("<span class=\"carousel-control-prev-icon\" aria-hidden=\"true\"></span>");
            html.AppendLine("<span class=\"sr-only\">Previous</span>");
            html.AppendLine("</a>");
            html.AppendLine("<a class=\"carousel-control-next\" href=\"#id\" role=\"button\" data-slide=\"next\">");
            html.AppendLine("<span class=\"carousel-control-next-icon\" aria-hidden=\"true\"></span>");
            html.AppendLine("<span class=\"sr-only\">Next</span>");
            html.AppendLine("</a>");
            var tag = this.GetTagHelper<CarouselTagHelper>(t =>
            {
                t.Id = "id";
                t.Indicators = true;
                t.Controls = true;
            });
            var c = tag.GetContext();
            this.GetTagHelper<CarouselItemTagHelper>().Test(c, "div", (c, o) =>
              {
                  o.TagName.ShouldBe("div");
                  o.ShouldJustHasClasses("carousel-item");
                  o.PreContent.GetContent().ShouldBe("");
              });
            tag.Test(c, "div", (c, o) =>
              {
                  o.TagName.ShouldBe("div");
                  o.ShouldJustHasClasses("carousel", "slide");
                  o.ShouldJustHasAttributesAndValues(("id", "id"), ("data-ride", "carousel"));
                  o.PreContent.GetContent().ShouldBe("<ol class=\"carousel-indicators\"><li data-target=\"#id\" data-slide-to=\"0\" class=\"active\"></li>\r\n</ol>\r\n<div class=\"carousel-inner\">");
                  o.PostContent.GetContent().ShouldBe("</div>" + html.ToString());
              });
        }
    }
}
