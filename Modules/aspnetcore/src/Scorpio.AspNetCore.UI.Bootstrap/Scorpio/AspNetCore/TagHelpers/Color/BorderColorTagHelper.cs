using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
namespace Scorpio.AspNetCore.TagHelpers.Color
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement(Attributes ="bd-color")]
    public class BorderColorTagHelper:TagHelper
    {
        /// <summary>
        /// 
        /// </summary>
        [HtmlAttributeName("bd-color")]
        public BorderColorType   BorderColor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.AddClass($"border-{BorderColor.ToClassName()}");
            output.Attributes.RemoveAll("bd-color");
            return base.ProcessAsync(context, output);
        }
    }
}
