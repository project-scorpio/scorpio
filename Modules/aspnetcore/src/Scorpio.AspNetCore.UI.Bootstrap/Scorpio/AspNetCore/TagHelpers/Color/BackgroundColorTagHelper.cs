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
    [HtmlTargetElement(Attributes ="bg-color")]
    public class BackgroundColorTagHelper:TagHelper
    {
        /// <summary>
        /// 
        /// </summary>
        [HtmlAttributeName("bg-color")]
        public BackgroundColorType  BackgroundColor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.AddClass($"bg-{BackgroundColor.ToClassName()}");
            output.Attributes.RemoveAll("bg-color");
            return base.ProcessAsync(context, output);
        }
    }
}
