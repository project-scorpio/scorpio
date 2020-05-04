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
    [HtmlTargetElement(HtmlTargetElementAttribute.ElementCatchAllTarget, Attributes = "rounded")]
    public class RoundedTagHelper : TagHelper<RoundedTagHelper,RoundedTagHelperService>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public RoundedTagHelper(RoundedTagHelperService service) : base(service)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public RoundedType  Rounded { get;  set; }

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
