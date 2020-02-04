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
            var badgeType = GetBadgeType(context, output);

            if (badgeType != BadgeType.Default && badgeType != BadgeType._)
            {
                output.AddClass("badge-" + badgeType.ToString().ToLowerInvariant());
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

            if (TagHelper.BadgePillType != BadgeType._)
            {
                output.AddClass("badge-pill");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        protected virtual BadgeType GetBadgeType(TagHelperContext context, TagHelperOutput output)
        {
            return TagHelper.BadgeType != BadgeType._ ? TagHelper.BadgeType : TagHelper.BadgePillType;
        }
    }
}