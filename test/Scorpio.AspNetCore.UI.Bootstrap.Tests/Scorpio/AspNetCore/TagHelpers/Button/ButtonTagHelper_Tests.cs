using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Button
{
    /// <summary>
    /// 
    /// </summary>
    public class ButtonTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<ButtonTagHelper>((c, o) =>
            {
                o.TagName.ShouldBe("button");
                o.ShouldJustHasClasses("btn");
                o.ShouldHasAttributeAndJustContainsValues("type", "button");
                o.ShouldHasAttributeAndJustContainsValues("data-busy-text", "...");
            });
        }
        [Fact]
        public void ExistsType()
        {
            var tag = this.GetTagHelper<ButtonTagHelper>();
            var (c, o) = tag.GetContextAndOutput("button");
            o.Attributes.Add("type", "button");
            tag.Test(c, o, (c, o) =>
              {
                  o.TagName.ShouldBe("button");
                  o.ShouldJustHasClasses("btn");
                  o.ShouldHasAttributeAndJustContainsValues("type", "button");
                  o.ShouldHasAttributeAndJustContainsValues("data-busy-text", "...");
              });
        }

        [Fact]
        public void EmptyBusyText()
        {
            this.Test<ButtonTagHelper>(t => t.BusyText = "", (c, o) =>
            {
                o.TagName.ShouldBe("button");
                o.ShouldJustHasClasses("btn");
                o.ShouldJustHasAttributesAndValues(("type", "button"));
            });
        }

        [Fact]
        public void BusyText()
        {
            this.Test<ButtonTagHelper>(t => t.BusyText = "busy", (c, o) =>
            {
                o.TagName.ShouldBe("button");
                o.ShouldJustHasClasses("btn");
                o.ShouldJustHasAttributesAndValues(("type", "button"), ("data-busy-text", "busy"));
            });
        }

        [Fact]
        public void ButtonType()
        {
            this.Test<ButtonTagHelper>(t => t.ButtonType = Button.ButtonType.Primary, (c, o) =>
                {
                    o.TagName.ShouldBe("button");
                    o.ShouldJustHasClasses("btn", "btn-primary");
                    o.ShouldJustHasAttributesAndValues(("type", "button"), ("data-busy-text", "..."));
                });
        }

        [Fact]
        public void OutLine()
        {
            this.Test<ButtonTagHelper>(t =>
            {
                t.ButtonType = Button.ButtonType.Primary;
                t.OutLine = true;
            }, (c, o) =>
            {
                o.TagName.ShouldBe("button");
                o.ShouldJustHasClasses("btn", "btn-outline-primary");
                o.ShouldJustHasAttributesAndValues(("type", "button"), ("data-busy-text", "..."));
            });
        }

        [Fact]
        public void Size()
        {
            this.Test<ButtonTagHelper>(t => t.Size = TagHelpers.Size.Large, (c, o) =>
            {
                o.TagName.ShouldBe("button");
                o.ShouldJustHasClasses("btn", "btn-lg");
                o.ShouldJustHasAttributesAndValues(("type", "button"), ("data-busy-text", "..."));
            });
        }

        [Fact]
        public void Block()
        {
            this.Test<ButtonTagHelper>(t => t.Block = true, (c, o) =>
            {
                o.TagName.ShouldBe("button");
                o.ShouldJustHasClasses("btn", "btn-block");
                o.ShouldJustHasAttributesAndValues(("type", "button"), ("data-busy-text", "..."));
            });
        }

        [Fact]
        public void Text()
        {
            this.Test<ButtonTagHelper>(t => t.Text = "TestButton", (c, o) =>
            {
                o.TagName.ShouldBe("button");
                o.ShouldJustHasClasses("btn");
                o.ShouldJustHasAttributesAndValues(("type", "button"), ("data-busy-text", "..."));
                o.Content.GetContent().ShouldBe("<span>TestButton</span>");
            });
        }
        [Fact]
        public void Icon()
        {
            this.Test<ButtonTagHelper>(t => t.Icon = "dash", (c, o) =>
            {
                o.TagName.ShouldBe("button");
                o.ShouldJustHasClasses("btn");
                o.ShouldJustHasAttributesAndValues(("type", "button"), ("data-busy-text", "..."));
                o.Content.GetContent().ShouldBe("<i class=\"fa fa-dash\"></i>");
            });
        }

        [Fact]
        public void OtherIcon()
        {
            this.Test<ButtonTagHelper>(t =>
            {
                t.IconType = FontIconType.Other;
                t.Icon = "dash";
            }, (c, o) =>
            {
                o.TagName.ShouldBe("button");
                o.ShouldJustHasClasses("btn");
                o.ShouldJustHasAttributesAndValues(("type", "button"), ("data-busy-text", "..."));
                o.Content.GetContent().ShouldBe("<i class=\"dash\"></i>");
            });
        }
        [Fact]
        public void IconAndText()
        {
            this.Test<ButtonTagHelper>(t =>
            {
                t.Icon = "dash";
                t.Text = "TestButton";
            }, (c, o) =>
            {
                o.TagName.ShouldBe("button");
                o.ShouldJustHasClasses("btn");
                o.ShouldJustHasAttributesAndValues(("type", "button"), ("data-busy-text", "..."));
                o.Content.GetContent().ShouldBe("<i class=\"fa fa-dash\"></i><span>TestButton</span>");
            });
        }

        [Fact]
        public void Disable()
        {
            this.Test<ButtonTagHelper>(t => t.Disabled = true, (c, o) =>
            {
                o.TagName.ShouldBe("button");
                o.ShouldJustHasClasses("btn");
                o.ShouldJustHasAttributesAndValues(("type", "button"), ("data-busy-text", "..."), ("disabled", "disabled"));
                o.Content.GetContent().ShouldBe("");
            });
        }

    }
}

