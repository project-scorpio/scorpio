using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

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
