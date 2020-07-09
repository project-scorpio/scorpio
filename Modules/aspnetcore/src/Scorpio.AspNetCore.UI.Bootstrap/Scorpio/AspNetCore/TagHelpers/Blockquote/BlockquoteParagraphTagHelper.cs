using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Blockquote
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("p", ParentTag = "blockquote")]
    public class BlockquoteParagraphTagHelper : TagHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.AddClass("mb-0");
            return base.ProcessAsync(context, output);
        }
    }
}
