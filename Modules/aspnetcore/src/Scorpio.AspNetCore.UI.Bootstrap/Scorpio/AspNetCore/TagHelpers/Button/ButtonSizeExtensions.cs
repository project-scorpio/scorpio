using System;

namespace Scorpio.AspNetCore.TagHelpers.Button
{
    internal static class ButtonSizeExtensions
    {
        public static string ToClassName(this Size size)
        {
            return size switch
            {
                Size.Small => "btn-sm",
                Size.Medium => "btn-md",
                Size.Large => "btn-lg",
                Size.Default => "",
                _ => throw new NotSupportedException($"Unknown {nameof(Size)}: {size}"),
            };
        }
    }
}