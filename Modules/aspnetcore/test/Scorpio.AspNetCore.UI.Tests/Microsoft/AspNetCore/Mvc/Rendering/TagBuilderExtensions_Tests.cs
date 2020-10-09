using System.IO;
using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Mvc.ViewFeatures;

using Shouldly;

using Xunit;

namespace Microsoft.AspNetCore.Mvc.Rendering
{
    /// <summary>
    /// 
    /// </summary>
    public class TagBuilderExtensions_Tests
    {
        [Fact]
        public void AddClass()
        {
            var tagBuilder = new TagBuilder("div");
            tagBuilder.AddClass("test").Attributes["class"].ShouldBe("test");
        }

        [Fact]
        public void AddAttribute()
        {
            var tagBuilder = new TagBuilder("div");
            tagBuilder.AddAttribute("attr", "test").Attributes["attr"].ShouldBe("test");

        }

        [Fact]
        public void Id()
        {
            var tagBuilder = new TagBuilder("div");
            tagBuilder.Id("test1").Attributes["id"].ShouldBe("test1");
            tagBuilder.Id("test2").Attributes["id"].ShouldBe("test1");
        }

        [Fact]
        public void Content_1()
        {
            var tagBuilder = new TagBuilder("div");
            var writer = new StringWriter();
            tagBuilder.Content(new StringHtmlContent("test")).InnerHtml.WriteTo(writer, HtmlEncoder.Default);
            writer.GetStringBuilder().ToString().ShouldBe("test");
        }

        [Fact]
        public void Content_2()
        {
            var tagBuilder = new TagBuilder("div");
            var writer = new StringWriter();
            tagBuilder.Content("test").InnerHtml.WriteTo(writer, HtmlEncoder.Default);
            writer.GetStringBuilder().ToString().ShouldBe("test");
        }

        [Fact]
        public void AddChild()
        {
            var tagBuilder = new TagBuilder("div");
            var writer = new StringWriter();
            tagBuilder.AddChild(h => new TagBuilder("div")).InnerHtml.WriteTo(writer, HtmlEncoder.Default);
            writer.GetStringBuilder().ToString().ShouldBe("<div></div>");
        }
    }
}
