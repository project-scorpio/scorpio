using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;

using Shouldly;

namespace Scorpio.AspNetCore
{
    public static class ClassNameExtensions
    {
        public static void ContainsClass(this TagHelperOutput output, string className)
        {
            (output.Attributes["class"]?.Value?.ToString()?.Split(' ')?.Contains(className) ?? false).ShouldBe(true);
        }

        public static void ContainsClasses(this TagHelperOutput output, params string[] classNames)
        {
            classNames.ForEach(c => output.ContainsClass(c));
        }

        public static void NotContainsClass(this TagHelperOutput output, string className)
        {
            (output.Attributes["class"]?.Value?.ToString()?.Split(' ')?.Contains(className) ?? false).ShouldBe(false);
        }

        public static void NotContainsClasses(this TagHelperOutput output, params string[] classNames)
        {
            classNames.ForEach(c => output.NotContainsClass(c));
        }

        public static void JustHasClasses(this TagHelperOutput output, params string[] classNames)
        {
            var classes = output.Attributes["class"]?.Value?.ToString()?.Split(' ') ?? new string[] { };
            classes.OrderBy(c=>c).SequenceEqual(classNames.OrderBy(c=>c)).ShouldBeTrue(()=>classes.ExpandToString(" "));
        }

    }
}
