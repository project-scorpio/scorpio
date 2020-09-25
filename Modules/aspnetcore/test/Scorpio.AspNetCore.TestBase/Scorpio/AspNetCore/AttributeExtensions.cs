using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;

using Shouldly;

namespace Scorpio.AspNetCore
{
    public static class AttributeExtensions
    {
        public static void ContainsAttribute(this TagHelperOutput output,string name, string value)
        {
            (output.Attributes[name]?.Value?.ToString()?.Split(' ')?.Contains(value) ?? false).ShouldBe(true);
        }

        public static void ContainsAttributes(this TagHelperOutput output, string name, params string[] values)
        {
            values.ForEach(c => output.ContainsAttribute(name,c));
        }

        public static void NotContainsAttribute(this TagHelperOutput output, string name, string value)
        {
            (output.Attributes[name]?.Value?.ToString()?.Split(' ')?.Contains(value) ?? false).ShouldBe(false);
        }

        public static void NotContainsAttributes(this TagHelperOutput output, string name, params string[] values)
        {
            values.ForEach(c => output.NotContainsAttribute(name,c));
        }

        public static void JustHasAttributes(this TagHelperOutput output, string name, params string[] values)
        {
            var classes = output.Attributes[name]?.Value?.ToString()?.Split(' ') ?? new string[] { };
            classes.OrderBy(c=>c).SequenceEqual(values.OrderBy(c=>c)).ShouldBeTrue();
        }

    }
}
