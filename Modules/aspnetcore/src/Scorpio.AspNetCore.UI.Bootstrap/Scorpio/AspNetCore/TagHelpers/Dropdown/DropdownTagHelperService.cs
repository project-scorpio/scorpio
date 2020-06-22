using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    /// <summary>
    /// 
    /// </summary>
    public class DropdownTagHelperService:TagHelperService<DropdownTagHelper>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            output.TagName = "div";
            output.AddClass("dropdown");
            output.AddClass("btn-group");

            SetDirection(context, output);

            output.TagMode = TagMode.StartTagAndEndTag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        protected virtual void SetDirection(TagHelperContext context, TagHelperOutput output)
        {
            switch (TagHelper.Direction)
            {
                case Direction.Down:
                    return;
                case Direction.Up:
                    output.AddClass("dropup");
                    return;
                case Direction.Right:
                    output.AddClass("dropright");
                    return;
                case Direction.Left:
                    output.AddClass("dropleft");
                    return;
            }
        }

    }
}