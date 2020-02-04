using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    [HtmlTargetElement("dropdown-menu", ParentTag = "dropdown")]
    public class DropdownMenuTagHelper : TagHelper<DropdownMenuTagHelper, DropdownMenuTagHelperService>
    {
        public DropdownMenuTagHelper(DropdownMenuTagHelperService service) : base(service)
        {
        }
    }
}
