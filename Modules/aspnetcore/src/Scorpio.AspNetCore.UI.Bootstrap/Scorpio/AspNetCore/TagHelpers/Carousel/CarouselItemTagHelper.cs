using System.Collections.Generic;

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Carousel
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement(ParentTag = "carousel")]
    public class CarouselItemTagHelper : TagHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void Init(TagHelperContext context)
        {
            context.InitValue<List<CarouselItem>>(CarouselTagHelper.CAROUSEL_ITEMS_CONTENT);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var list = context.GetValue<List<CarouselItem>>(CarouselTagHelper.CAROUSEL_ITEMS_CONTENT);
            list.Add(new CarouselItem { IsActive = IsActive ?? false });
            output.TagName = "div";
            output.AddClass("carousel-item");
            if (IsActive ?? false)
            {
                output.AddClass("active");
            }
        }
    }
}
