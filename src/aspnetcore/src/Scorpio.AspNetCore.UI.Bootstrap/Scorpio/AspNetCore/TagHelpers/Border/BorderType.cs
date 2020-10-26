using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Border
{
    /// <summary>
    /// 
    /// </summary>
    public enum BorderType
    {
        /// <summary>
        /// 
        /// </summary>
        All,

        /// <summary>
        /// 
        /// </summary>
        Top,

        /// <summary>
        /// 
        /// </summary>
        [ClassName("top-0")]
        SubtractiveTop,

        /// <summary>
        /// 
        /// </summary>
        Right,

        /// <summary>
        /// 
        /// </summary>
        [ClassName("right-0")]
        SubtractiveRight,

        /// <summary>
        /// 
        /// </summary>
        Bottom,

        /// <summary>
        /// 
        /// </summary>
        [ClassName("bottom-0")]
        SubtractiveBottom,

        /// <summary>
        /// 
        /// </summary>
        Left,

        /// <summary>
        /// 
        /// </summary>
        [ClassName("left-0")]
        SubtractiveLeft,

        /// <summary>
        /// 
        /// </summary>
        [ClassName("0")]
        None,
    }
}