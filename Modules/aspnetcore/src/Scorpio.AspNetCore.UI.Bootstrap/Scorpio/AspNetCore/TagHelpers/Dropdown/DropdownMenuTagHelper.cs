using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("dropdown-menu", ParentTag = "dropdown")]
    public class DropdownMenuTagHelper : TagHelper<DropdownMenuTagHelper, DropdownMenuTagHelperService>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public DropdownMenuTagHelper(DropdownMenuTagHelperService service) : base(service)
        {
        }
    }
}
