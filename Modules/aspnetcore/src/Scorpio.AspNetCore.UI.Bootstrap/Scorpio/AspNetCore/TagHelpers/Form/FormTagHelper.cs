using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers.Form
{
    public class FormTagHelper : TagHelper<FormTagHelper, FormTagHelperService>
    {
        public FormTagHelper(FormTagHelperService service) : base(service)
        {
        }
    }
}
