﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Badge
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("a", Attributes = "badge")]
    [HtmlTargetElement("span", Attributes = "badge")]
    [HtmlTargetElement("a", Attributes = "badge-pill")]
    [HtmlTargetElement("span", Attributes = "badge-pill")]
    public class BadgeTagHelper : TagHelper<BadgeTagHelper, BadgeTagHelperService>
    {
        /// <summary>
        /// 
        /// </summary>
        [HtmlAttributeName("badge")]
        public BadgeType BadgeType { get; set; } = BadgeType._;

        /// <summary>
        /// 
        /// </summary>
        [HtmlAttributeName("badge-pill")]
        public bool BadgePill { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagHelperService"></param>
        public BadgeTagHelper(BadgeTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
