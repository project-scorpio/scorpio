using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.TagHelpers;

using Shouldly;

using Xunit;

namespace Microsoft.AspNetCore.Razor.TagHelpers
{
    /// <summary>
    /// 
    /// </summary>
    public class TagHelperAttributeListExtensions_Test
    {
        [Fact]
        public static void AddClass()
        {
            var output=new TagHelperOutput("div",new TagHelperAttributeList(), (result, encoder) =>
            {
                var tagHelperContent = new DefaultTagHelperContent();
                tagHelperContent.SetHtmlContent(string.Empty);
                return Task.FromResult<TagHelperContent>(tagHelperContent);
            });
            output.AddClass("");
            output.Attributes.ContainsName("class").ShouldBeFalse();
            output.AddClass("test");
            output.Attributes["class"].Value.ShouldBe("test");
        }

        [Fact]
        public void RemoveClass()
        {
            var output = new TagHelperOutput("div", new TagHelperAttributeList(), (result, encoder) =>
            {
                var tagHelperContent = new DefaultTagHelperContent();
                tagHelperContent.SetHtmlContent(string.Empty);
                return Task.FromResult<TagHelperContent>(tagHelperContent);
            });
            output.AddClass("");
            output.Attributes.ContainsName("class").ShouldBeFalse();
            output.AddClass("test");
            output.Attributes["class"].Value.ShouldBe("test");
            output.RemoveClass("");
            output.Attributes["class"].Value.ShouldBe("test");
            output.RemoveClass("test");
            output.Attributes.ContainsName("class").ShouldBeFalse();
        }

        [Fact]
        public void AddAttribute()
        {
            var output = new TagHelperOutput("div", new TagHelperAttributeList(), (result, encoder) =>
            {
                var tagHelperContent = new DefaultTagHelperContent();
                tagHelperContent.SetHtmlContent(string.Empty);
                return Task.FromResult<TagHelperContent>(tagHelperContent);
            });
            output.Attributes.ContainsName("attr").ShouldBeFalse();
            output.AddAttribute("", "");
            output.Attributes.ContainsName("").ShouldBeFalse();
            output.AddAttribute("attr", "test");
            output.Attributes["attr"].Value.ShouldBe("test");

        }
    }
}
