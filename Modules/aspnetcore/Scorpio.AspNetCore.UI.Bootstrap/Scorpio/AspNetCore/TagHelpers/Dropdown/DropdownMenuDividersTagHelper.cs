using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    [HtmlTargetElement("divider", ParentTag = "dropdown-menu")]
    public class DropdownMenuDividersTagHelper : TagHelper<DropdownMenuDividersTagHelper, DropdownMenuDividersTagHelperService>
    {
        public DropdownMenuDividersTagHelper(DropdownMenuDividersTagHelperService service) : base(service)
        {
        }
    }
}
