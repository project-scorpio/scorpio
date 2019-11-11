using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
namespace Scorpio.AspNetCore.TagHelpers.Card
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("card-body",ParentTag ="card")]
    public class CardBodyTagHelper : TagHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SubTilte { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AddClass("card-body");
            ProcessTitle(output);
            ProcessSubTitle(output);
            return base.ProcessAsync(context, output);
        }

        private void ProcessTitle(TagHelperOutput output)
        {
            if (!Title.IsNullOrWhiteSpace())
            {
                var tag = new TagBuilder(CardTitleTagHelper.DefaultHeading.ToTagName());
                tag.AddCssClass("card-title");
                tag.InnerHtml.Append(Title);
                output.PreContent.AppendHtml(tag);
            }
        }
        private void ProcessSubTitle(TagHelperOutput output)
        {
            if (!SubTilte.IsNullOrWhiteSpace())
            {
                var tag = new TagBuilder(CardSubTitleTagHelper.DefaultHeading.ToTagName());
                tag.AddCssClass("card-subtitle");
                tag.InnerHtml.Append(SubTilte);
                output.PreContent.AppendHtml(tag);
            }
        }
    }
}
