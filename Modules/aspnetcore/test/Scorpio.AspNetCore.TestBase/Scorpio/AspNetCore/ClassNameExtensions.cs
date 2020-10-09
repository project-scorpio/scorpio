using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Microsoft.AspNetCore.Razor.TagHelpers;

using Shouldly;

namespace Scorpio.AspNetCore
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ClassNameExtensions
    {
        public static void ShouldContainsClass(this TagHelperOutput output, string className)
        {
            (output.Attributes["class"]?.Value?.ToString()?.Split(' ')?.Contains(className) ?? false).ShouldBe(true);
        }

        public static void ShouldContainsClasses(this TagHelperOutput output, params string[] classNames)
        {
            classNames.ForEach(c => output.ShouldContainsClass(c));
        }

        public static void ShouldNotContainsClass(this TagHelperOutput output, string className)
        {
            (output.Attributes["class"]?.Value?.ToString()?.Split(' ')?.Contains(className) ?? false).ShouldBe(false);
        }

        public static void ShouldNotContainsClasses(this TagHelperOutput output, params string[] classNames)
        {
            classNames.ForEach(c => output.ShouldNotContainsClass(c));
        }

        public static void ShouldJustHasClasses(this TagHelperOutput output, params string[] classNames)
        {
            var classes = output.Attributes["class"]?.Value?.ToString()?.Split(' ') ?? new string[] { };
            classes.OrderBy(c => c).SequenceEqual(classNames.OrderBy(c => c)).ShouldBeTrue(() => classes.ExpandToString(" "));
        }

    }
}
