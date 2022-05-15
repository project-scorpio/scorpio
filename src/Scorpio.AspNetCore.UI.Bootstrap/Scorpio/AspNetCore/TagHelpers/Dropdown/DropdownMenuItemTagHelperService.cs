using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    /// <summary>
    /// 
    /// </summary>
    public class DropdownMenuItemTagHelperService : TagHelperService<DropdownMenuItemTagHelper>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.AddClass("dropdown-item");
            switch (TagHelper.Status)
            {
                case DropdownItemStatus.Active:
                    output.AddClass("active");
                    break;
                case DropdownItemStatus.Disabled:
                    output.AddClass("disabled");
                    break;
                default:
                    break;
            }
        }
    }
}