﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Collapse
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement(Attributes = "collapse")]
    [HtmlTargetElement("collapse")]
    public class CollapseTagHelper : TagHelper<CollapseTagHelper, CollapseTagHelperService>
    {

        /// <summary>
        /// 
        /// </summary>
        public CollapseType Collapse { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public CollapseTagHelper(CollapseTagHelperService service) : base(service)
        {
        }
    }
}
