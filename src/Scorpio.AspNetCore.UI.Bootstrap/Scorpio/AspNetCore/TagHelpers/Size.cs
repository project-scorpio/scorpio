using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers
{
    /// <summary>
    /// 
    /// </summary>
    public enum Size
    {
        /// <summary>
        /// 
        /// </summary>
        [ClassName("")]
        Default,
        /// <summary>
        /// 
        /// </summary>
        [ClassName("sm")]
        Small,
        /// <summary>
        /// 
        /// </summary>
        [ClassName("md")]
        Medium,
        /// <summary>
        /// 
        /// </summary>
        [ClassName("lg")]
        Large,
    }
}