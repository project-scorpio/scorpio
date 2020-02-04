using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Card
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("img",ParentTag ="card")]
    public class CardImageTagHelper:TagHelper
    {

        /// <summary>
        /// 
        /// </summary>
        public CardImagePosition  Position { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            output.AddClass($"card-img{Position.ToClassName()}");
            return base.ProcessAsync(context, output);
        }
    }
}
