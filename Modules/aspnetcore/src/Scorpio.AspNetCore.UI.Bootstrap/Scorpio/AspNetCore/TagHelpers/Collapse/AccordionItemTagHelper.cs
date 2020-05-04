using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers.Collapse
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("item",ParentTag ="accordion")]
    public class AccordionItemTagHelper : TagHelper<AccordionItemTagHelper, AccordionItemTagHelperService>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; } = $"aci-{Guid.NewGuid():N}";

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? Active { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public AccordionItemTagHelper(AccordionItemTagHelperService service) : base(service)
        {
        }
    }
}
