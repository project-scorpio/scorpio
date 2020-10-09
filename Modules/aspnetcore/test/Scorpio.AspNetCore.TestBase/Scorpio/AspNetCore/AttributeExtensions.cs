using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Razor.TagHelpers;

using Shouldly;

namespace Scorpio.AspNetCore
{
    public static class AttributeExtensions
    {
        public static void ShouldContainsAttribute(this TagHelperOutput output, string name, string value)
        {
            (output.Attributes[name]?.Value?.ToString()?.Split(' ')?.Contains(value) ?? false).ShouldBe(true);
        }

        public static void ShouldHasAttributeAndContainsValues(this TagHelperOutput output, string name, params string[] values)
        {
            values.ForEach(c => output.ShouldContainsAttribute(name, c));
        }

        public static void ShouldHasAttributeAndNotContainsValue(this TagHelperOutput output, string name, string value)
        {
            (output.Attributes[name]?.Value?.ToString()?.Split(' ')?.Contains(value) ?? false).ShouldBe(false);
        }

        public static void ShouldHasAttributeAndNotContainsValues(this TagHelperOutput output, string name, params string[] values)
        {
            values.ForEach(c => output.ShouldHasAttributeAndNotContainsValue(name, c));
        }

        public static void ShouldHasAttributeAndJustContainsValues(this TagHelperOutput output, string name, params string[] values)
        {
            var classes = output.Attributes[name]?.Value?.ToString()?.Split(' ') ?? new string[] { };
            classes.OrderBy(c => c).SequenceEqual(values.OrderBy(c => c)).ShouldBeTrue();
        }

        public static void ShouldJustHasAttributesAndValues(this TagHelperOutput output, params (string attr, string value)[] values)
        {
            values.Select(v => v.attr).OrderBy(a => a).SequenceEqual(output.Attributes.Where(a => a.Name != "class").Select(a => a.Name).OrderBy(a => a)).ShouldBeTrue();
            values.ForEach(c => output.ShouldHasAttributeAndJustContainsValues(c.attr, c.value));
        }

        public static void ShouldJustHasAttributesAndValues(this TagHelperOutput output, params (string attr, string[] value)[] values)
        {
            values.Select(v => v.attr).OrderBy(a => a).SequenceEqual(output.Attributes.Where(a => a.Name != "class").Select(a => a.Name).OrderBy(a => a)).ShouldBeTrue();
            values.ForEach(c => output.ShouldHasAttributeAndJustContainsValues(c.attr, c.value));
        }
    }
}
