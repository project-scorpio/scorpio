using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Button
{
    /// <summary>
    /// 
    /// </summary>
    public class ButtonToolbarTagHelperService : TagHelperService<ButtonToolbarTagHelper>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AddClass("btn-toolbar");
            output.Attributes.Add("role","toolbar");
        }
    }
}