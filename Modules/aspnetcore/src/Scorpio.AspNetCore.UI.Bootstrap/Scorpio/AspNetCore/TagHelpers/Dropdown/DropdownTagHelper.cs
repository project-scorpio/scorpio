using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    public class DropdownTagHelper:TagHelper<DropdownTagHelper,DropdownTagHelperService>
    {

        public Direction Direction { get; set; }

        public DropdownTagHelper(DropdownTagHelperService service) : base(service)
        {
        }

    }
}
