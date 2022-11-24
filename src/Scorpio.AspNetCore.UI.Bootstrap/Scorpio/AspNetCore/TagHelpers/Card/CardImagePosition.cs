using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Card
{
    /// <summary>
    /// 
    /// </summary>
    public enum CardImagePosition
    {
        /// <summary>
        /// 
        /// </summary>
        [ClassName("")]
        None,
        /// <summary>
        /// 
        /// </summary>
        [ClassName("-top")]
        Top,
        /// <summary>
        /// 
        /// </summary>
        [ClassName("-bottom")]
        Bottom
    }
}
