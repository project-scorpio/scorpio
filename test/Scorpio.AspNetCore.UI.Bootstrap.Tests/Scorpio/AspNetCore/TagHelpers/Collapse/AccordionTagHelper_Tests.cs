using System.Linq;

using Microsoft.AspNetCore.Razor.TagHelpers;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Collapse
{
    /// <summary>
    /// 
    /// </summary>
    [RestrictChildren("item")]
    public class AccordionTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<AccordionTagHelper>(t => t.Id = "id", (c, o) =>
             {
                 o.TagName.ShouldBe("div");
                 o.ShouldJustHasClasses("card");
                 o.ShouldJustHasAttributesAndValues(("id", "id"));
             });
        }

        [Fact]
        public void Item()
        {
            var tag = this.GetTagHelper<AccordionTagHelper>(t => t.Id = "id");
            var c = tag.GetContext();
            this.GetTagHelper<AccordionItemTagHelper>(t => t.Id = "id_id").Test(c, "div", (c, o) =>
            {
                o.TagName.ShouldBe(null);
                o.ShouldJustHasClasses("collapse");
                o.ShouldJustHasAttributesAndValues(("id", "id_id"), ("aria-labelledby", "head-id_id"), ("data-parent", $"__PARENT_ID__"));
                c.GetValue<AccordionItemList>().ShouldHaveSingleItem().Content.ShouldBe("<div class=\"card-header __LAST_CARD_HEADER__\" id=\"head-id_id\"><h5 class=\"mb-0\"><button aria-controls=\"id_id\" aria-expanded=\"true\" class=\"btn btn-link\" data-target=\"#id_id\" data-toggle=\"collapse\"></button></h5></div><div class=\"collapse\" id=\"id_id\" aria-labelledby=\"head-id_id\" data-parent=\"__PARENT_ID__\"><div class=\"card-body __LAST_CARD_BODY__\"></div></div>");
            });
            tag.Test(c, "div", (c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.ShouldJustHasClasses("card");
                o.ShouldJustHasAttributesAndValues(("id", "id"));
                o.Content.GetContent().ShouldBe("<div class=\"card-header border-bottom-0\" id=\"head-id_id\"><h5 class=\"mb-0\"><button aria-controls=\"id_id\" aria-expanded=\"true\" class=\"btn btn-link\" data-target=\"#id_id\" data-toggle=\"collapse\"></button></h5></div><div class=\"collapse\" id=\"id_id\" aria-labelledby=\"head-id_id\" data-parent=\"#id\"><div class=\"card-body border-top\"></div></div>");
            });
        }
        [Fact]
        public void Items()
        {
            var tag = this.GetTagHelper<AccordionTagHelper>(t => t.Id = "id");
            var c = tag.GetContext();
            this.GetTagHelper<AccordionItemTagHelper>(t => t.Id = "id_id").Test(c, "div", (c, o) =>
            {
                o.TagName.ShouldBe(null);
                o.ShouldJustHasClasses("collapse");
                o.ShouldJustHasAttributesAndValues(("id", "id_id"), ("aria-labelledby", "head-id_id"), ("data-parent", $"__PARENT_ID__"));
                c.GetValue<AccordionItemList>().ShouldHaveSingleItem().Content.ShouldBe("<div class=\"card-header __LAST_CARD_HEADER__\" id=\"head-id_id\"><h5 class=\"mb-0\"><button aria-controls=\"id_id\" aria-expanded=\"true\" class=\"btn btn-link\" data-target=\"#id_id\" data-toggle=\"collapse\"></button></h5></div><div class=\"collapse\" id=\"id_id\" aria-labelledby=\"head-id_id\" data-parent=\"__PARENT_ID__\"><div class=\"card-body __LAST_CARD_BODY__\"></div></div>");
            });
            this.GetTagHelper<AccordionItemTagHelper>(t => t.Id = "id_id_2").Test(c, "div", (c, o) =>
            {
                o.TagName.ShouldBe(null);
                o.ShouldJustHasClasses("collapse");
                o.ShouldJustHasAttributesAndValues(("id", "id_id_2"), ("aria-labelledby", "head-id_id_2"), ("data-parent", $"__PARENT_ID__"));
                c.GetValue<AccordionItemList>().Last().Content.ShouldBe("<div class=\"card-header __LAST_CARD_HEADER__\" id=\"head-id_id_2\"><h5 class=\"mb-0\"><button aria-controls=\"id_id_2\" aria-expanded=\"true\" class=\"btn btn-link\" data-target=\"#id_id_2\" data-toggle=\"collapse\"></button></h5></div><div class=\"collapse\" id=\"id_id_2\" aria-labelledby=\"head-id_id_2\" data-parent=\"__PARENT_ID__\"><div class=\"card-body __LAST_CARD_BODY__\"></div></div>");
            });
            tag.Test(c, "div", (c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.ShouldJustHasClasses("card");
                o.ShouldJustHasAttributesAndValues(("id", "id"));
                o.Content.GetContent().ShouldBe("<div class=\"card-header \" id=\"head-id_id\"><h5 class=\"mb-0\"><button aria-controls=\"id_id\" aria-expanded=\"true\" class=\"btn btn-link\" data-target=\"#id_id\" data-toggle=\"collapse\"></button></h5></div><div class=\"collapse\" id=\"id_id\" aria-labelledby=\"head-id_id\" data-parent=\"#id\"><div class=\"card-body border-bottom\"></div></div><div class=\"card-header border-bottom-0\" id=\"head-id_id_2\"><h5 class=\"mb-0\"><button aria-controls=\"id_id_2\" aria-expanded=\"true\" class=\"btn btn-link\" data-target=\"#id_id_2\" data-toggle=\"collapse\"></button></h5></div><div class=\"collapse\" id=\"id_id_2\" aria-labelledby=\"head-id_id_2\" data-parent=\"#id\"><div class=\"card-body border-top\"></div></div>");
            });
        }
    }
}
