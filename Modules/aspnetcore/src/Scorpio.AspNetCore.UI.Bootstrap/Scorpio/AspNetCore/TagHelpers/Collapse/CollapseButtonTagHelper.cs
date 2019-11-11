using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers.Collapse
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("button", Attributes = "collapse-target")]
    [HtmlTargetElement("a", Attributes = "collapse-target")]
    public class CollapseButtonTagHelper : TagHelper<CollapseButtonTagHelper, CollapseButtonTagHelperService>
    {
        /// <summary>
        /// 
        /// </summary>
        [HtmlAttributeName("collapse-target")]
        public string Target { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public CollapseButtonTagHelper(CollapseButtonTagHelperService service) : base(service)
        {
        }
    }
}
