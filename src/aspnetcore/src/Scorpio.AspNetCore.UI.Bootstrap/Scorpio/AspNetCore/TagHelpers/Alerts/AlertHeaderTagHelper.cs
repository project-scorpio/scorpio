
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Alerts
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("h1", ParentTag = "alert", TagStructure = TagStructure.NormalOrSelfClosing)]
    [HtmlTargetElement("h2", ParentTag = "alert", TagStructure = TagStructure.NormalOrSelfClosing)]
    [HtmlTargetElement("h3", ParentTag = "alert", TagStructure = TagStructure.NormalOrSelfClosing)]
    [HtmlTargetElement("h4", ParentTag = "alert", TagStructure = TagStructure.NormalOrSelfClosing)]
    [HtmlTargetElement("h5", ParentTag = "alert", TagStructure = TagStructure.NormalOrSelfClosing)]
    [HtmlTargetElement("h6", ParentTag = "alert", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class AlertHeaderTagHelper : TagHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.AddClass("alert-heading");
        }
    }
}
