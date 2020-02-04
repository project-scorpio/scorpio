using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.AspNetCore.TagHelpers.Border
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement(HtmlTargetElementAttribute.ElementCatchAllTarget, Attributes = "border")]
    public class BorderTagHelper : TagHelper<BorderTagHelper,BorderTagHelperService>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public BorderTagHelper(BorderTagHelperService service) : base(service)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public BorderType Border { get;  set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {

            return base.ProcessAsync(context, output);
        }
    }
}
