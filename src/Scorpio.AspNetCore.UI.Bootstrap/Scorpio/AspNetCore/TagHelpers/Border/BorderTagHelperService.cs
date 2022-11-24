using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Border
{
    /// <summary>
    /// 
    /// </summary>
    public class BorderTagHelperService : TagHelperService<BorderTagHelper>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (TagHelper.Border == BorderType.All)
            {
                output.AddClass("border");
            }
            else
            {
                output.AddClass(TagHelper.Border.ToClassName("border-{0}"));
            }
            output.Attributes.RemoveAll("border");
        }
    }
}
