using Microsoft.AspNetCore.Razor.TagHelpers;

using Scorpio.AspNetCore.UI.Bootstrap;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Button
{
    /// <summary>
    /// 
    public class LinkButtonTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<LinkButtonTagHelper>((a,c, o) =>
            {
                o.TagName.ShouldBe(a.Tag);
                o.JustHasClasses("btn");
                if (a.Tag == "a") {
                    o.JustHasAttributesAndValues(("role", "button")); 
                }
                else
                {
                    o.JustHasAttributesAndValues(("type", "button"), ("role", "button"));
                }
            });
        }

        [Fact]
        public void ButtonType()
        {
            this.Test<LinkButtonTagHelper>(t => t.ButtonType = Button.ButtonType.Primary, (a,c, o) =>
            {
                o.TagName.ShouldBe(a.Tag);
                o.JustHasClasses("btn", "btn-primary");
                if (a.Tag == "a")
                {
                    o.JustHasAttributesAndValues(("role", "button"));
                }
                else
                {
                    o.JustHasAttributesAndValues(("type", "button"), ("role", "button"));
                }
            });
        }

        [Fact]
        public void OutLine()
        {
            this.Test<LinkButtonTagHelper>(t =>
            {
                t.ButtonType = Button.ButtonType.Primary;
                t.OutLine = true;
            }, (a,c, o) =>
            {
                o.TagName.ShouldBe(a.Tag);
                o.JustHasClasses("btn", "btn-outline-primary");
                if (a.Tag == "a")
                {
                    o.JustHasAttributesAndValues(("role", "button"));
                }
                else
                {
                    o.JustHasAttributesAndValues(("type", "button"), ("role", "button"));
                }
            });
        }

        [Fact]
        public void Size()
        {
            this.Test<LinkButtonTagHelper>(t =>
            {
                t.Size = TagHelpers.Size.Large;
            }, (a, c, o) =>
            {
                o.TagName.ShouldBe(a.Tag);
                o.JustHasClasses("btn", "btn-lg");
                if (a.Tag == "a")
                {
                    o.JustHasAttributesAndValues(("role", "button"));
                }
                else
                {
                    o.JustHasAttributesAndValues(("type", "button"), ("role", "button"));
                }
            });
        }

        [Fact]
        public void Block()
        {
            this.Test<LinkButtonTagHelper>(t =>
            {
                t.Block = true;
            }, (a, c, o) =>
            {
                o.TagName.ShouldBe(a.Tag);
                o.JustHasClasses("btn", "btn-block");
                if (a.Tag == "a")
                {
                    o.JustHasAttributesAndValues(("role", "button"));
                }
                else
                {
                    o.JustHasAttributesAndValues(("type", "button"), ("role", "button"));
                }
            });
        }

        [Fact]
        public void Text()
        {
            this.Test<LinkButtonTagHelper>(t =>
            {
                t.Text = "TestButton";
            }, (a, c, o) =>
            {
                o.TagName.ShouldBe(a.Tag);
                o.JustHasClasses("btn");
                if (a.Tag == "a")
                {
                    o.JustHasAttributesAndValues(("role", "button"));
                }
                else
                {
                    o.JustHasAttributesAndValues(("type", "button"), ("role", "button"));
                }
                o.Content.GetContent().ShouldBe("<span>TestButton</span>");
            });
        }

        [Fact]
        public void Icon()
        {
            this.Test<LinkButtonTagHelper>(t =>
            {
                t.Icon = "dash";
            }, (a, c, o) =>
            {
                o.TagName.ShouldBe(a.Tag);
                o.JustHasClasses("btn");
                if (a.Tag == "a")
                {
                    o.JustHasAttributesAndValues(("role", "button"));
                }
                else
                {
                    o.JustHasAttributesAndValues(("type", "button"), ("role", "button"));
                }
                o.Content.GetContent().ShouldBe("<i class=\"fa fa-dash\"></i>");
            });
        }
        [Fact]
        public void IconAndText()
        {
            this.Test<LinkButtonTagHelper>(t =>
            {
                t.Icon = "dash";
                t.Text = "TestButton";
            }, (a, c, o) =>
            {
                o.TagName.ShouldBe(a.Tag);
                o.JustHasClasses("btn");
                if (a.Tag == "a")
                {
                    o.JustHasAttributesAndValues(("role", "button"));
                }
                else
                {
                    o.JustHasAttributesAndValues(("type", "button"), ("role", "button"));
                }
                o.Content.GetContent().ShouldBe("<i class=\"fa fa-dash\"></i><span>TestButton</span>");
            });
        }

        [Fact]
        public void Disable()
        {
            this.Test<LinkButtonTagHelper>(t =>
            {
                t.Disabled = true;
            }, (a, c, o) =>
            {
                o.TagName.ShouldBe(a.Tag);
                o.JustHasClasses("btn");
                if (a.Tag == "a")
                {
                    o.JustHasAttributesAndValues(("role", "button"), ("disabled", "disabled"));
                }
                else
                {
                    o.JustHasAttributesAndValues(("type", "button"), ("role", "button"), ("disabled", "disabled"));
                }
                o.Content.GetContent().ShouldBe("");
            });
        }
    }
}
