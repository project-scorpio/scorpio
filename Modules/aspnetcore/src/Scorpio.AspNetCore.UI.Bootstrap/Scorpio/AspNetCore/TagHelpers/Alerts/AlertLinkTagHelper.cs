using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.AspNetCore.TagHelpers.Alerts
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("a", Attributes = "alert-link", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class AlertLinkTagHelper:TagHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.AddClass("alert-link");
            output.Attributes.RemoveAll("alert-link");
            return base.ProcessAsync(context, output);
        }
    }
}
