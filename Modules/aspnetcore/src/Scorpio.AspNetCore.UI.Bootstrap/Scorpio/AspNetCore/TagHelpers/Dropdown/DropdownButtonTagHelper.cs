using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    [HtmlTargetElement("button", ParentTag = "dropdown")]
    [HtmlTargetElement("a", ParentTag = "dropdown")]
    public class DropdownButtonTagHelper : TagHelper<DropdownButtonTagHelper, DropdownButtonTagHelperService>
    {
        public DropdownButtonType  DropdownButtonType { get; set; }
        public DropdownButtonTagHelper(DropdownButtonTagHelperService service) : base(service)
        {
        }
    }
}
