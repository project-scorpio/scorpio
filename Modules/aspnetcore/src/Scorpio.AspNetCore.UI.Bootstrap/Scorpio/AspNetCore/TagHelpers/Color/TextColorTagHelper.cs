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
    [HtmlTargetElement(Attributes ="txt-color")]
    public class TextColorTagHelper : TagHelper
    {
        /// <summary>
        /// 
        /// </summary>
        [HtmlAttributeName("txt-color")]
        public TextColorType   TextColor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.AddClass($"text-{TextColor.ToClassName()}");
            output.Attributes.RemoveAll("txt-color");
            return base.ProcessAsync(context, output);
        }
    }
}
