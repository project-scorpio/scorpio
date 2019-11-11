using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    public class DropdownMenuItemTagHelperService:TagHelperService<DropdownMenuItemTagHelper>
    {
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
            base.Process(context, output);
        }
    }
}