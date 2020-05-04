using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Collapse
{
    /// <summary>
    /// 
    /// </summary>
    public class CollapseTagHelperService:TagHelperService<CollapseTagHelper>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (output.TagName=="collapse")
            {
                output.TagName = "div";
            }
            output.AddClass("collapse");
            if (TagHelper.Collapse== CollapseType.Show)
            {
                output.AddClass("show");
            }
            base.Process(context, output);
        }
    }
}