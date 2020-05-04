using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    [HtmlTargetElement("a", ParentTag = "dropdown-menu")]
    [HtmlTargetElement("button", ParentTag = "dropdown-menu")]
    public class DropdownMenuItemTagHelper : TagHelper<DropdownMenuItemTagHelper, DropdownMenuItemTagHelperService>
    {

        /// <summary>
        /// 
        /// </summary>
        public DropdownItemStatus Status { get; set; }
        public DropdownMenuItemTagHelper(DropdownMenuItemTagHelperService service) : base(service)
        {
        }

    }
}
