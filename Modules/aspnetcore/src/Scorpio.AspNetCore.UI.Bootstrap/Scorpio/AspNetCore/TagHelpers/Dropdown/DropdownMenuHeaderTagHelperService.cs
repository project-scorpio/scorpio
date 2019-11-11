using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    public class DropdownMenuHeaderTagHelperService:TagHelperService<DropdownMenuHeaderTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "h6";
            output.AddClass("dropdown-header");
            base.Process(context, output);
        }
    }
}