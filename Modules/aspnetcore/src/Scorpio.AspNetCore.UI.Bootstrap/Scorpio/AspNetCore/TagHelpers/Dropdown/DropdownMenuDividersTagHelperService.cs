using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    public class DropdownMenuDividersTagHelperService:TagHelperService<DropdownMenuDividersTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AddClass("dropdown-divider");
            base.Process(context, output);
        }
    }
}