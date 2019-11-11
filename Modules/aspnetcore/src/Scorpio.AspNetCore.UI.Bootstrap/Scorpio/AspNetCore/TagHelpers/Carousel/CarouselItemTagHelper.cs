using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Carousel
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement(ParentTag ="carousel")]
    public class CarouselItemTagHelper:TagHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var list = context.GetValue<List<CarouselItem>>(CarouselTagHelper.CAROUSEL_ITEMS_CONTENT);
            list.Add(new CarouselItem { IsActive = IsActive ?? false });
            output.TagName = "div";
            output.AddClass("carousel-item");
            if (IsActive??false)
            {
                output.AddClass("active");
            }
            return Task.CompletedTask;
        }
    }
}
