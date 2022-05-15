using System;

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Collapse
{
    /// <summary>
    /// 
    /// </summary>
    [RestrictChildren("item")]
    public class AccordionTagHelper : TagHelper<AccordionTagHelper, AccordionTagHelperService>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; } = $"acc-{Guid.NewGuid():N}";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public AccordionTagHelper(AccordionTagHelperService service) : base(service)
        {
        }
    }
}
