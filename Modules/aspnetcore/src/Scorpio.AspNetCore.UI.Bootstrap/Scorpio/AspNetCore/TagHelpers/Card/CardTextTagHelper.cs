using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.AspNetCore.TagHelpers.Card
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("card-text")]
    public class CardTextTagHelper:TagHelper
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>s
        /// <param name="output"></param>
        /// <returns></returns>
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "p";
            output.AddClass("card-text");
            return base.ProcessAsync(context, output);
        }
    }
}
