using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

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
            if (TagHelper.Border== BorderType.All)
            {
                output.AddClass("border");
            }
            output.AddClass($"border-{TagHelper.Border.ToClassName()}");
            output.Attributes.RemoveAll("border");
        }
    }
}
