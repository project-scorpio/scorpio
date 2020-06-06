using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("button", ParentTag = "dropdown")]
    [HtmlTargetElement("a", ParentTag = "dropdown")]
    public class DropdownButtonTagHelper : TagHelper<DropdownButtonTagHelper, DropdownButtonTagHelperService>
    {
        /// <summary>
        /// 
        /// </summary>
        public DropdownButtonType  DropdownButtonType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public DropdownButtonTagHelper(DropdownButtonTagHelperService service) : base(service)
        {
        }
    }
}
