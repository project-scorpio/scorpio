using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("divider", ParentTag = "dropdown-menu")]
    public class DropdownMenuDividersTagHelper : TagHelper<DropdownMenuDividersTagHelper, DropdownMenuDividersTagHelperService>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public DropdownMenuDividersTagHelper(DropdownMenuDividersTagHelperService service) : base(service)
        {
        }
    }
}
