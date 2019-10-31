using System.Collections.Generic;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Breadcrumb
{
    /// <summary>
    /// 
    /// </summary>
    public class BreadcrumbItemTagHelperService : TagHelperService<BreadcrumbItemTagHelper>
    {
        private readonly HtmlEncoder _encoder;

        /// <summary>
        /// 
        /// </summary>
        protected static readonly string BreadcrumbItemActivePlaceholder = "{_Breadcrumb_Active_Placeholder_}";

        /// <summary>
        /// 
        /// </summary>
        internal protected static readonly string BreadcrumbItemsContent = "BreadcrumbItemsContent";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encoder"></param>
        public BreadcrumbItemTagHelperService(HtmlEncoder encoder)
        {
            _encoder = encoder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "li";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddClass("breadcrumb-item");
            output.AddClass(BreadcrumbItemActivePlaceholder);

            var list = context.GetValue<List<BreadcrumbItem>>(BreadcrumbItemsContent);

            output.Content.SetHtmlContent(GetInnerHtml(context, output));
            
            list.Add(new BreadcrumbItem
            {
                Html = RenderTagHelperOutput(output, _encoder),
                Active = TagHelper.Active
            });

            output.SuppressOutput();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        protected virtual string GetInnerHtml(TagHelperContext context, TagHelperOutput output)
        {
            var content = output.GetChildContentAsync().Result.GetContent();
            if (string.IsNullOrWhiteSpace(TagHelper.Href))
            {
                output.Attributes.Add("aria-current", "page");
                return content;
            }
            return "<a href=\"" + TagHelper.Href + "\" title=\""+TagHelper.Title+"\">" + content+ "</a>";
        }

    }
}