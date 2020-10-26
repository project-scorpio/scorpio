using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;

using Moq;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers
{
    public class TagHelper_Tests
    {
        [Fact]
        public void Order()
        {
            var mock = new Mock<TagHelper>();
            mock.Setup(t => t.Order).CallBase();
            var tagHelper = mock.Object;
            tagHelper.Order.ShouldBe(0);
        }

        [Fact]
        public void Init()
        {
            var mock = new Mock<TagHelper>();
            mock.Setup(t => t.Process(It.IsAny<TagHelperContext>(), It.IsAny<TagHelperOutput>()))
                .Callback<TagHelperContext, TagHelperOutput>((c, o) =>
                {
                    o.TagName = "div";
                    o.Content.Append("test");
                }).CallBase();
            mock.Setup(t => t.Init(It.IsAny<TagHelperContext>())).CallBase().Verifiable();
            var tagHelper = mock.Object;
            var tagHelperContext = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                Guid.NewGuid().ToString("N"));
            Should.NotThrow(() => tagHelper.Init(tagHelperContext));
        }

        [Fact]
        public void Process()
        {
            var mock = new Mock<TagHelper>();
            mock.Setup(t => t.Process(It.IsAny<TagHelperContext>(), It.IsAny<TagHelperOutput>()))
                .Callback<TagHelperContext, TagHelperOutput>((c, o) =>
                {
                    o.TagName = "div";
                    o.Content.Append("test");
                }).CallBase();
            mock.Setup(t => t.ProcessAsync(It.IsAny<TagHelperContext>(), It.IsAny<TagHelperOutput>())).CallBase();
            var tagHelper = mock.Object;
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
            Should.NotThrow(() => tagHelper.ProcessAsync(tagHelperContext, tagHelperOutput));
            tagHelperOutput.TagName.ShouldBe("div");
            tagHelperOutput.Content.GetContent().ShouldBe("test");
        }
    }

}
