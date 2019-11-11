using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers.Border
{
    /// <summary>
    /// 
    /// </summary>
    public class RoundedTagHelperService : TagHelperService<RoundedTagHelper>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var roundedClass = "rounded";

            if (TagHelper.Rounded != RoundedType.Default)
            {
                roundedClass += "-" + TagHelper.Rounded.ToClassName();
            }

            output.AddClass(roundedClass);
        }

    }
}
