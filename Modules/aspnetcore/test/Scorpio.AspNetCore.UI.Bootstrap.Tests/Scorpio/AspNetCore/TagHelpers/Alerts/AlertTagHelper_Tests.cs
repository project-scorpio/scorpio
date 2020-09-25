using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;

using Scorpio.AspNetCore.UI.Bootstrap;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Alerts
{
    public class AlertTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<AlertTagHelper>((c, o) =>
            {
                o.TagName.ShouldBe("div");
                o.JustHasAttributes("role", "alert");
                o.JustHasClasses("alert");
            });
        }

        [Fact]
        public void Type()
        {
            this.Test<AlertTagHelper>(t => t.Type = AlertType.Primary, (c, o) =>
             {
                 o.TagName.ShouldBe("div");
                 o.JustHasAttributes("role", "alert");
                 o.JustHasClasses("alert", "alert-primary");
             });
        }

        [Fact]
        public void Dismissible()
        {
            var buttonAsHtml =
            "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">" + Environment.NewLine +
            "    <span aria-hidden=\"true\">&times;</span>" + Environment.NewLine +
            " </button>";
            this.Test<AlertTagHelper>(t => t.Dismissible = true, (c, o) =>
             {
                 o.TagName.ShouldBe("div");
                 o.JustHasAttributes("role", "alert");
                 o.JustHasClasses("alert", "alert-dismissible", "fade", "show");
                 o.PostContent.GetContent().ShouldBe(buttonAsHtml);
             });
        }
    }
}
