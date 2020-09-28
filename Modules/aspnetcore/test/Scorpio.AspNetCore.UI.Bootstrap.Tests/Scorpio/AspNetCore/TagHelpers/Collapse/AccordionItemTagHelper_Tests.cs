using System;
using System.Security.Permissions;

using Microsoft.AspNetCore.Razor.TagHelpers;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Collapse
{
    /// <summary>
    /// 
    /// </summary>
    public class AccordionItemTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<AccordionItemTagHelper>(t => t.Id = "id", (c, o) =>
            {
                o.TagName.ShouldBe(null);
                o.JustHasClasses("collapse");
                o.JustHasAttributesAndValues(("id", "id"), ("aria-labelledby", "head-id"), ("data-parent", $"__PARENT_ID__"));
                c.GetValue<AccordionItemList>().ShouldHaveSingleItem().Content.ShouldBe("<div class=\"__LAST_CARD_HEADER__ card-header\" id=\"head-id\"><h5 class=\"mb-0\"><button aria-controls=\"id\" aria-expanded=\"true\" class=\"btn btn-link\" data-target=\"#id\" data-toggle=\"collapse\"></button></h5></div><div class=\"collapse\" id=\"id\" aria-labelledby=\"head-id\" data-parent=\"__PARENT_ID__\"><div class=\"__LAST_CARD_BODY__ card-body\"></div></div>");
            });
        }

        [Fact]
        public void Active()
        {
            this.Test<AccordionItemTagHelper>(t => { t.Id = "id"; t.Active = true; }, (c, o) =>
            {
                o.TagName.ShouldBe(null);
                o.JustHasClasses("collapse", "show");
                o.JustHasAttributesAndValues(("id", "id"), ("aria-labelledby", "head-id"), ("data-parent", $"__PARENT_ID__"));
                c.GetValue<AccordionItemList>().ShouldHaveSingleItem().Order.ShouldBe(0);
                c.GetValue<AccordionItemList>().ShouldHaveSingleItem().Content.ShouldBe("<div class=\"__LAST_CARD_HEADER__ card-header\" id=\"head-id\"><h5 class=\"mb-0\"><button aria-controls=\"id\" aria-expanded=\"true\" class=\"btn btn-link\" data-target=\"#id\" data-toggle=\"collapse\"></button></h5></div><div class=\"collapse show\" id=\"id\" aria-labelledby=\"head-id\" data-parent=\"__PARENT_ID__\"><div class=\"__LAST_CARD_BODY__ card-body\"></div></div>");
            });
        }

        [Fact]
        public void Title()
        {
            this.Test<AccordionItemTagHelper>(t =>
            {
                t.Id = "id";
                t.Title = "title";
            }, (c, o) =>
            {
                o.TagName.ShouldBe(null);
                o.JustHasClasses("collapse");
                o.JustHasAttributesAndValues(("id", "id"), ("aria-labelledby", "head-id"), ("data-parent", $"__PARENT_ID__"));
                c.GetValue<AccordionItemList>().ShouldHaveSingleItem().Content.ShouldBe("<div class=\"__LAST_CARD_HEADER__ card-header\" id=\"head-id\"><h5 class=\"mb-0\"><button aria-controls=\"id\" aria-expanded=\"true\" class=\"btn btn-link\" data-target=\"#id\" data-toggle=\"collapse\">title</button></h5></div><div class=\"collapse\" id=\"id\" aria-labelledby=\"head-id\" data-parent=\"__PARENT_ID__\"><div class=\"__LAST_CARD_BODY__ card-body\"></div></div>");
            });
        }
    }
}
