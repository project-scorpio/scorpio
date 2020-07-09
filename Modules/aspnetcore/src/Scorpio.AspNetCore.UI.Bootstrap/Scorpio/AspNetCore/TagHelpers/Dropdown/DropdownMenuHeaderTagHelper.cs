using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("menu-header", ParentTag = "dropdown-menu")]
    public class DropdownMenuHeaderTagHelper : TagHelper<DropdownMenuHeaderTagHelper, DropdownMenuHeaderTagHelperService>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public DropdownMenuHeaderTagHelper(DropdownMenuHeaderTagHelperService service) : base(service)
        {
        }
    }
}
