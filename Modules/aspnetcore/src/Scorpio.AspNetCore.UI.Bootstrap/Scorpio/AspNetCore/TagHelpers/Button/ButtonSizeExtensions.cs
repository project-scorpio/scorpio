using System;

namespace Scorpio.AspNetCore.TagHelpers.Button
{
    internal static class ButtonSizeExtensions
    {
        public static string ToClassName(this Size size)
        {
            switch (size)
            {
                case Size.Small:
                    return "btn-sm";
                case Size.Medium:
                    return "btn-md";
                case Size.Large:
                    return "btn-lg";
                case Size.Default:
                    return "";
                default:
                    throw new Exception($"Unknown {nameof(Size)}: {size}");
            }
        }
    }
}