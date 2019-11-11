using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    public class DropdownMenuTagHelperService:TagHelperService<DropdownMenuTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AddClass("dropdown-menu");
            base.Process(context, output);
        }
    }
}