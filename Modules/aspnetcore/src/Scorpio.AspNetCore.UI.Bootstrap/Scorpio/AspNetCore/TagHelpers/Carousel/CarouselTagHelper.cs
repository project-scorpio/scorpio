using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Carousel
{
    /// <summary>
    /// 
    /// </summary>
    [RestrictChildren("carousel-item")]
    public class CarouselTagHelper : TagHelper
    {
        internal const string CAROUSEL_ITEMS_CONTENT = "CarouselItemsContent";

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? Crossfade { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? Controls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? Indicators { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void Init(TagHelperContext context)
        {
            context.InitValue<List<CarouselItem>>(CAROUSEL_ITEMS_CONTENT);
            base.Init(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            AddBasicAttributes(context, output);
           await output.GetChildContentAsync();
            var itemList = context.GetValue<List<CarouselItem>>(CAROUSEL_ITEMS_CONTENT);
            SetOneItemAsActive(context, output, itemList);
            SetIndicators(context, output, itemList);
            output.PreContent.AppendHtml("<div class=\"carousel-inner\">");
            output.PostContent.AppendHtml("</div>");
            SetControls(context, output, itemList);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <param name="itemList"></param>
        protected virtual void SetControls(TagHelperContext context, TagHelperOutput output, List<CarouselItem> itemList)
        {
            if (!Controls ?? false)
            {
                return;
            }

            var html = new StringBuilder("");

            html.AppendLine("<a class=\"carousel-control-prev\" href=\"#" + Id + "\" role=\"button\" data-slide=\"prev\">");
            html.AppendLine("<span class=\"carousel-control-prev-icon\" aria-hidden=\"true\"></span>");
            html.AppendLine("<span class=\"sr-only\">Previous</span>");
            html.AppendLine("</a>");
            html.AppendLine("<a class=\"carousel-control-next\" href=\"#" + Id + "\" role=\"button\" data-slide=\"next\">");
            html.AppendLine("<span class=\"carousel-control-next-icon\" aria-hidden=\"true\"></span>");
            html.AppendLine("<span class=\"sr-only\">Next</span>");
            html.AppendLine("</a>");

            output.PostContent.AppendHtml(html.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <param name="itemList"></param>
        protected virtual void SetIndicators(TagHelperContext context, TagHelperOutput output, List<CarouselItem> itemList)
        {
            if (!Indicators ?? false)
            {
                return;
            }

            var html = new StringBuilder("<ol class=\"carousel-indicators\">");

            for (var i = 0; i < itemList.Count; i++)
            {
                html.AppendLine(
                    "<li " +
                    "data-target=\"#" + Id + "\"" +
                    " data-slide-to=\"" + i + "\"" +
                    (itemList[i].IsActive ? " class=\"active\">" : "") +
                    "</li>");
            }

            html.AppendLine("</ol>");
            output.PreContent.AppendHtml(html.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <param name="itemList"></param>
        private void SetOneItemAsActive(TagHelperContext context, TagHelperOutput output, List<CarouselItem> itemList)
        {
            if (!itemList.Any(it => it.IsActive) && itemList.Count > 0)
            {
                itemList.FirstOrDefault().IsActive = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        protected virtual void AddBasicAttributes(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("id", Id);
            AddBasicClasses(context, output);
            output.Attributes.Add("data-ride", "carousel");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        protected virtual void AddBasicClasses(TagHelperContext context, TagHelperOutput output)
        {
            output.AddClass("carousel");
            output.AddClass("slide");
            SetFadeAnimation(context, output);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        protected virtual void SetFadeAnimation(TagHelperContext context, TagHelperOutput output)
        {
            if (Crossfade ?? false)
            {
                output.AddClass("carousel-fade");
            }
        }

    }
}
