using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("menu-header", ParentTag = "dropdown-menu")]
    public class DropdownMenuHeaderTagHelper : TagHelper<DropdownMenuHeaderTagHelper, DropdownMenuHeaderTagHelperService>
    {
        public DropdownMenuHeaderTagHelper(DropdownMenuHeaderTagHelperService service) : base(service)
        {
        }
    }
}
