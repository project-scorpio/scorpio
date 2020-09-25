using System.Collections.Generic;

using Scorpio.AspNetCore.UI.Bootstrap;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Breadcrumb
{

    /// <summary>
    /// 
    /// </summary>

    public class BreadcrumbItemTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<BreadcrumbItemTagHelper>((c, o) =>
            {
                o.TagName.ShouldBe(null);
                var list = c.GetValue<List<BreadcrumbItem>>(BreadcrumbItemTagHelperService.BreadcrumbItemsContent);
                list.ShouldHaveSingleItem().Html.ShouldBe("<li class=\"breadcrumb-item {_Breadcrumb_Active_Placeholder_}\" aria-current=\"page\"></li>");
            });
        }

        [Fact]
        public void Href()
        {
            this.Test<BreadcrumbItemTagHelper>(t => t.Href = "https://localhost", (c, o) =>
            {
                o.TagName.ShouldBe(null);
                var list = c.GetValue<List<BreadcrumbItem>>(BreadcrumbItemTagHelperService.BreadcrumbItemsContent);
                list.ShouldHaveSingleItem().Html.ShouldBe("<li class=\"breadcrumb-item {_Breadcrumb_Active_Placeholder_}\"><a href=\"https://localhost\" title=\"\"></a></li>");
            });
        }

        [Fact]
        public void Title()
        {
            this.Test<BreadcrumbItemTagHelper>(t =>
            {
                t.Href = "https://localhost";
                t.Title = "Test";
            }, (c, o) =>
            {
                o.TagName.ShouldBe(null);
                var list = c.GetValue<List<BreadcrumbItem>>(BreadcrumbItemTagHelperService.BreadcrumbItemsContent);
                list.ShouldHaveSingleItem().Html.ShouldBe("<li class=\"breadcrumb-item {_Breadcrumb_Active_Placeholder_}\"><a href=\"https://localhost\" title=\"Test\"></a></li>");
            });
        }

        [Fact]
        public void Active()
        {
            this.Test<BreadcrumbItemTagHelper>(t =>
            {
                t.Href = "https://localhost";
                t.Title = "Test";
                t.Active = true;
            }, (c, o) =>
            {
                o.TagName.ShouldBe(null);
                var list = c.GetValue<List<BreadcrumbItem>>(BreadcrumbItemTagHelperService.BreadcrumbItemsContent);
                list.ShouldHaveSingleItem().Active.ShouldBeTrue();
                list.ShouldHaveSingleItem().Html.ShouldBe("<li class=\"breadcrumb-item {_Breadcrumb_Active_Placeholder_}\"><a href=\"https://localhost\" title=\"Test\"></a></li>");
            });
        }
    }
}
