using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Alerts
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("a", Attributes = "alert-link", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class AlertLinkTagHelper : TagHelper
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
            output.TagName = "a";
            output.Attributes.RemoveAll("alert-link");
            return base.ProcessAsync(context, output);
        }
    }
}
