using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Razor.TagHelpers;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers
{
    public class TagHelperContextExtensions_Tests
    {
        [Fact]
        public void GetValue()
        {
            var context = new TagHelperContext(
                 new TagHelperAttributeList(),
                 new Dictionary<object, object>(),
                 Guid.NewGuid().ToString("N"));
            context.GetValue<string>().ShouldBe(default);
        }

        [Fact]
        public void SetValue()
        {
            var context = new TagHelperContext(
                 new TagHelperAttributeList(),
                 new Dictionary<object, object>(),
                 Guid.NewGuid().ToString("N"));
            context.GetValue<string>().ShouldBe(default);
            context.SetValue<string>("test");
            context.GetValue<string>().ShouldBe("test");

        }

        [Fact]
        public void InitValue()
        {
            var context = new TagHelperContext(
                 new TagHelperAttributeList(),
                 new Dictionary<object, object>(),
                 Guid.NewGuid().ToString("N"));
            context.GetValue<List<string>>().ShouldBe(default);
            context.InitValue<List<string>>();
            context.GetValue<List<string>>().ShouldBeEmpty();

        }

    }
}
