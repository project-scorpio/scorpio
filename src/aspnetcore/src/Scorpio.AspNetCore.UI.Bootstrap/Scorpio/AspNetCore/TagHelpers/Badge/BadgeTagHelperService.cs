using Microsoft.AspNetCore.Razor.TagHelpers;
namespace Scorpio.AspNetCore.TagHelpers.Badge
{
    /// <summary>
    /// 
    /// </summary>
    public class BadgeTagHelperService : TagHelperService<BadgeTagHelper>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            SetBadgeClass(context, output);
            SetBadgeStyle(context, output);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        protected virtual void SetBadgeStyle(TagHelperContext context, TagHelperOutput output)
        {
            var badgeType = TagHelper.BadgeType;

            if (badgeType is not BadgeType.Default and not BadgeType._)
            {
                output.AddClass(badgeType.ToClassName("badge-{0}"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        protected virtual void SetBadgeClass(TagHelperContext context, TagHelperOutput output)
        {
            output.AddClass("badge");

            if (TagHelper.BadgePill)
            {
                output.AddClass("badge-pill");
            }
        }


    }
}