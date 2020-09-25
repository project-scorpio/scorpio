using System;
using System.Linq;
using System.Reflection;

using Shouldly;

using Xunit;

namespace Microsoft.AspNetCore.Razor.TagHelpers
{
    public class TagHelperExtensions
    {
        [Fact]
        public void ToClassName()
        {
            ClassName.Fa.ToClassName().ShouldBe("fa");
            ClassName.Red.ToClassName().ShouldBe("color-red");
        }

        [Fact]
        public void ToTagName()
        {
            TagName.Div.ToTagName().ShouldBe("div");
            TagName.ListGroup.ToTagName().ShouldBe("list-group");
        }
    }

    public enum ClassName
    {
        Fa,
        [ClassName("color-red")]
        Red
    }

    public enum TagName
    {
        Div,
        [TagName("list-group")]
        ListGroup
    }


}
