using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers.Form
{
    public class InputTagHelper : TagHelper<InputTagHelper, InputTagHelperService>
    {
        public string Title { get; set; }

        public Orientation Orientation { get; set; }

        public Size Size { get; set; }

        public InputTagHelper(InputTagHelperService service) : base(service)
        {

        }
    }
}
