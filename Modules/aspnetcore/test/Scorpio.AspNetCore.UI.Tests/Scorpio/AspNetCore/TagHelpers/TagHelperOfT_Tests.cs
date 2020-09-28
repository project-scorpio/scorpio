using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;

using Moq;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers
{
    public class TagHelperOfT_Tests
    {
        [Fact]
        public void TagHelper()
        {
            var service = new TestTagHelperService();
            var tagHelper = new TestTagHelper(service);
            service.TagHelper.ShouldBe(tagHelper);
        }

        [Fact]
        public void RenderTagHelperOutput()
        {
            var service = new TestTagHelperService();
            var tagHelper = new TestTagHelper(service);
            var tagHelperContext = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                Guid.NewGuid().ToString("N"));
            var tagHelperOutput = new TagHelperOutput("test",
                new TagHelperAttributeList(),
                (result, encoder) =>
                {
                    var tagHelperContent = new DefaultTagHelperContent();
                    tagHelperContent.SetHtmlContent(string.Empty);
                    return Task.FromResult<TagHelperContent>(tagHelperContent);
                });
            Should.NotThrow(() => service.InvokeRenderTagHelperOutput(tagHelperContext, tagHelperOutput)).ShouldBe("<div>test</div>");
            tagHelperOutput.TagName.ShouldBe("div");
            tagHelperOutput.Content.GetContent().ShouldBe("test");
        }
        [Fact]
        public void Order()
        {
            var tagHelper = new TestTagHelper(new TestTagHelperService());
            tagHelper.Order.ShouldBe(0);
        }

        [Fact]
        public void Init()
        {
            var tagHelper = new TestTagHelper(new TestTagHelperService());   
            var tagHelperContext = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                Guid.NewGuid().ToString("N"));
            Should.NotThrow(() => tagHelper.Init(tagHelperContext));
            tagHelperContext.GetValue<List<string>>().ShouldBeEmpty();
        }

        [Fact]
        public void Process()
        {
            var tagHelper =new TestTagHelper(new TestTagHelperService());
            var tagHelperContext = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                Guid.NewGuid().ToString("N"));
            var tagHelperOutput = new TagHelperOutput("test",
                new TagHelperAttributeList(),
                (result, encoder) =>
                {
                    var tagHelperContent = new DefaultTagHelperContent();
                    tagHelperContent.SetHtmlContent(string.Empty);
                    return Task.FromResult<TagHelperContent>(tagHelperContent);
                });
            Should.NotThrow(()=> tagHelper.ProcessAsync(tagHelperContext, tagHelperOutput));
            tagHelperOutput.TagName.ShouldBe("div");
            tagHelperOutput.Content.GetContent().ShouldBe("test");
        }
    }

    class TestTagHelper : TagHelper<TestTagHelper, TestTagHelperService>
    {
        public TestTagHelper(TestTagHelperService service) : base(service)
        {
        }
    }

    class TestTagHelperService : TagHelperService<TestTagHelper>
    {
        public override void Init(TagHelperContext context)
        {
            context.InitValue<List<string>>();
            base.Init(context);
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Content.Append("test");
            base.Process(context, output);
        }

        public string InvokeRenderTagHelperOutput(TagHelperContext context, TagHelperOutput output)
        {
            Process( context,  output);
            return RenderTagHelperOutput(output, HtmlEncoder.Default);
        }
    }
}
