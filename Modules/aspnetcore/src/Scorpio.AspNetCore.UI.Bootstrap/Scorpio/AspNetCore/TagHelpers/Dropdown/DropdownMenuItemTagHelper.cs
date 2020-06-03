using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("a", ParentTag = "dropdown-menu")]
    [HtmlTargetElement("button", ParentTag = "dropdown-menu")]
    public class DropdownMenuItemTagHelper : TagHelper<DropdownMenuItemTagHelper, DropdownMenuItemTagHelperService>
    {

        /// <summary>
        /// 
        /// </summary>
        public DropdownItemStatus Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public DropdownMenuItemTagHelper(DropdownMenuItemTagHelperService service) : base(service)
        {
        }

    }
}
