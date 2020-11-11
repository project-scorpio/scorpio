using Microsoft.AspNetCore.Razor.TagHelpers;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Form
{
    /// <summary>
    /// 
    /// </summary>
    public class InputTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default()
        {
            this.Test<InputTagHelper>(t =>
            {
            }, c => { }, o =>
            {
                o.AddAttribute("id", "id");
                o.AddAttribute("type", "text");
            }, (a, c, o) =>
            {
                o.TagName.ShouldBe("input");
                o.ShouldJustHasClasses("form-control");
                o.ShouldJustHasAttributesAndValues(("id", "id"), ("type", "text"), ("placeholder", $"请输入"));
                o.PreElement.GetContent().ShouldBe("<div class=\"form-group\"><label for=\"id\"></label>");
                o.PostElement.GetContent().ShouldBe($"</div>");
            });
        }

        [Fact]
        public void Placeholder()
        {
            this.Test<InputTagHelper>(t =>
            {

            }, c => { }, o =>
            {
                o.AddAttribute("placeholder", "placeholder");
                o.AddAttribute("id", "id");
                o.AddAttribute("type", "text");
            }, (a, c, o) =>
            {
                o.TagName.ShouldBe("input");
                o.ShouldJustHasAttributesAndValues(("id", "id"), ("type", "text"), ("placeholder", $"placeholder"));
                o.ShouldJustHasClasses("form-control");
                o.PreElement.GetContent().ShouldBe("<div class=\"form-group\"><label for=\"id\"></label>");
                o.PostElement.GetContent().ShouldBe($"</div>");
            });
        }

        [Fact]
        public void Horizontal()
        {
            this.Test<InputTagHelper>(t => t.Orientation = Orientation.Horizontal, c => { }, o =>
            {
                o.AddAttribute("id", "id");
                o.AddAttribute("type", "text");
            }, (a, c, o) =>
            {
                o.TagName.ShouldBe("input");
                o.ShouldJustHasClasses("form-control");
                o.ShouldJustHasAttributesAndValues(("id", "id"), ("type", "text"), ("placeholder", $"请输入"));
                o.PreElement.GetContent().ShouldBe($"<div class=\"form-group row\"><label class=\"col-md-2 col-form-label\" for=\"id\"></label><div class=\"col-md-10\">");
                o.PostElement.GetContent().ShouldBe($"</div></div>");
            });
        }

        [Fact]
        public void HorizontalAndMedium()
        {
            this.Test<InputTagHelper>(t =>
            {
                t.Orientation = Orientation.Horizontal;
                t.Size = Size.Medium;
            }, c => { }, o =>
            {
                o.AddAttribute("id", "id");
                o.AddAttribute("type", "text");
            }, (a, c, o) =>
            {
                o.TagName.ShouldBe("input");
                o.ShouldJustHasClasses("form-control");
                o.ShouldJustHasAttributesAndValues(("id", "id"), ("type", "text"), ("placeholder", $"请输入"));
                o.PreElement.GetContent().ShouldBe($"<div class=\"form-group row\"><label class=\"col-md-2 col-form-label\" for=\"id\"></label><div class=\"col-md-10\">");
                o.PostElement.GetContent().ShouldBe($"</div></div>");
            });
        }

        [Fact]
        public void Title()
        {
            this.Test<InputTagHelper>(t => t.Title = "Title", c => { }, o =>
            {
                o.AddAttribute("id", "id");
                o.AddAttribute("type", "text");
            }, (a, c, o) =>
            {
                o.TagName.ShouldBe("input");
                o.ShouldJustHasClasses("form-control");
                o.ShouldJustHasAttributesAndValues(("id", "id"), ("type", "text"), ("placeholder", $"请输入Title"));
                o.PreElement.GetContent().ShouldBe("<div class=\"form-group\"><label for=\"id\">Title</label>");
                o.PostElement.GetContent().ShouldBe($"</div>");
            });
        }

        [Fact]
        public void Large()
        {
            this.Test<InputTagHelper>(t => t.Size = TagHelpers.Size.Large, c => { }, o =>
            {
                o.AddAttribute("id", "id");
                o.AddAttribute("type", "text");
            }, (a, c, o) =>
            {
                o.TagName.ShouldBe("input");
                o.ShouldJustHasClasses("form-control", "form-control-lg");
                o.ShouldJustHasAttributesAndValues(("id", "id"), ("type", "text"), ("placeholder", $"请输入"));
                o.PreElement.GetContent().ShouldBe("<div class=\"form-group\"><label for=\"id\"></label>");
                o.PostElement.GetContent().ShouldBe($"</div>");
            });
        }

        [Fact]
        public void Small()
        {
            this.Test<InputTagHelper>(t => t.Size = Size.Small, c => { }, o =>
            {
                o.AddAttribute("id", "id");
                o.AddAttribute("type", "text");
            }, (a, c, o) =>
            {
                o.TagName.ShouldBe("input");
                o.ShouldJustHasClasses("form-control", "form-control-sm");
                o.ShouldJustHasAttributesAndValues(("id", "id"), ("type", "text"), ("placeholder", $"请输入"));
                o.PreElement.GetContent().ShouldBe("<div class=\"form-group\"><label for=\"id\"></label>");
                o.PostElement.GetContent().ShouldBe($"</div>");
            });
        }

        [Fact]
        public void HorizontalAndLarge()
        {
            this.Test<InputTagHelper>(t =>
            {
                t.Orientation = Orientation.Horizontal;
                t.Size = Size.Large;
            }, c => { }, o =>
            {
                o.AddAttribute("id", "id");
                o.AddAttribute("type", "text");
            }, (a, c, o) =>
            {
                o.TagName.ShouldBe("input");
                o.ShouldJustHasClasses("form-control", "form-control-lg");
                o.ShouldJustHasAttributesAndValues(("id", "id"), ("type", "text"), ("placeholder", $"请输入"));
                o.PreElement.GetContent().ShouldBe($"<div class=\"form-group row\"><label class=\"col-md-2 col-form-label col-form-label-lg\" for=\"id\"></label><div class=\"col-md-10\">");
                o.PostElement.GetContent().ShouldBe($"</div></div>");
            });
        }

        [Fact]
        public void HorizontalAndSmall()
        {
            this.Test<InputTagHelper>(t =>
            {
                t.Orientation = Orientation.Horizontal;
                t.Size = Size.Small;
            }, c => { }, o =>
            {
                o.AddAttribute("id", "id");
                o.AddAttribute("type", "text");
            }, (a, c, o) =>
            {
                o.TagName.ShouldBe("input");
                o.ShouldJustHasClasses("form-control", "form-control-sm");
                o.ShouldJustHasAttributesAndValues(("id", "id"), ("type", "text"), ("placeholder", $"请输入"));
                o.PreElement.GetContent().ShouldBe($"<div class=\"form-group row\"><label class=\"col-md-2 col-form-label col-form-label-sm\" for=\"id\"></label><div class=\"col-md-10\">");
                o.PostElement.GetContent().ShouldBe($"</div></div>");
            });
        }

        [Fact]
        public void CheckBox()
        {
            this.Test<InputTagHelper>(t =>
            {
            }, c => { }, o =>
            {
                o.AddAttribute("id", "id");
                o.AddAttribute("type", "checkbox");
            }, (a, c, o) =>
            {
                o.TagName.ShouldBe("input");
                o.ShouldJustHasClasses();
                o.ShouldJustHasAttributesAndValues(("id", "id"), ("type", "checkbox"), ("placeholder", $"请输入"));
                o.PreElement.GetContent().ShouldBe("<div class=\"form-group\"><label for=\"id\"></label>");
                o.PostElement.GetContent().ShouldBe($"</div>");
            });
        }

    }
}
