using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Badge
{
    /// <summary>
    /// 
    /// </summary>
    public enum BadgeType
    {
        /// <summary>
        /// 
        /// </summary>
        [ClassName("")]
        _,
        /// <summary>
        /// 
        /// </summary>
        Default,
        /// <summary>
        /// 
        /// </summary>
        Primary,
        /// <summary>
        /// 
        /// </summary>
        Secondary,
        /// <summary>
        /// 
        /// </summary>
        Success,
        /// <summary>
        /// 
        /// </summary>
        Danger,
        /// <summary>
        /// 
        /// </summary>
        Warning,
        /// <summary>
        /// 
        /// </summary>
        Info,
        /// <summary>
        /// 
        /// </summary>
        Light,
        /// <summary>
        /// 
        /// </summary>
        Dark
    }
}