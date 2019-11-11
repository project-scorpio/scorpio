using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.AspNetCore.TagHelpers.Card
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("card-title", ParentTag = "card-body")]
    public class CardTitleTagHelper:TagHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static HtmlHeadingType DefaultHeading { get; set; } = HtmlHeadingType.H5;

        /// <summary>
        /// 
        /// </summary>
        public HtmlHeadingType Heading { get; set; } = DefaultHeading;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>s
        /// <param name="output"></param>
        /// <returns></returns>
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = Heading.ToTagName();
            output.AddClass("card-title");
            return base.ProcessAsync(context, output);
        }
    }
}
