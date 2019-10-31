using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.AspNetCore.TagHelpers.Breadcrumb
{
    /// <summary>
    /// 
    /// </summary>
    public class BreadcrumbTagHelperService : TagHelperService<BreadcrumbTagHelper>
    {
        /// <summary>
        /// 
        /// </summary>
        internal protected static readonly string BreadcrumbItemActivePlaceholder = "{_Breadcrumb_Active_Placeholder_}";
        /// <summary>
        /// 
        /// </summary>
        internal protected static readonly string BreadcrumbItemsContent = "BreadcrumbItemsContent";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var list = InitilizeFormGroupContentsContext(context, output);
            output.TagName = "nav";
            output.Attributes.Add("aria-label", "breadcrumb");
            await output.GetChildContentAsync();

            SetInnerOlTag(context, output);
            SetInnerList(context, output, list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        protected virtual void SetInnerOlTag(TagHelperContext context, TagHelperOutput output)
        {
            output.PreContent.SetHtmlContent("<ol class=\"breadcrumb\">");
            output.PostContent.SetHtmlContent("</ol>");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <param name="list"></param>
        protected virtual void SetInnerList(TagHelperContext context, TagHelperOutput output, List<BreadcrumbItem> list)
        {
            SetLastOneActiveIfThereIsNotAny(context, output, list);

            var html = new StringBuilder("");

            foreach (var breadcrumbItem in list)
            {
                var htmlPart = SetActiveClassIfActiveAndGetHtml(breadcrumbItem);

                html.AppendLine(htmlPart);
            }

            output.Content.SetHtmlContent(html.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        protected virtual List<BreadcrumbItem> InitilizeFormGroupContentsContext(TagHelperContext context, TagHelperOutput output)
        {
            return context.InitValue<List<BreadcrumbItem>>(BreadcrumbItemsContent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <param name="list"></param>
        protected virtual void SetLastOneActiveIfThereIsNotAny(TagHelperContext context, TagHelperOutput output, List<BreadcrumbItem> list)
        {
            if (list.Count > 0 && !list.Any(bc => bc.Active))
            {
                list.Last().Active = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual string SetActiveClassIfActiveAndGetHtml(BreadcrumbItem item)
        {
            return item.Active ?
                item.Html.Replace(BreadcrumbItemActivePlaceholder, " active") :
                item.Html.Replace(BreadcrumbItemActivePlaceholder, "");
        }

    }
}
