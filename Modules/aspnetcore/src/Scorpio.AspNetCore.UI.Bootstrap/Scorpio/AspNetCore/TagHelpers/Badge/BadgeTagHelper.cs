using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

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
        public BadgeType BadgePillType { get; set; } = BadgeType._;

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
