using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers.Form
{
    public class DynamicInputTagHelper : TagHelper<DynamicInputTagHelper, DynamicInputTagHelperService>
    {

        public ModelExpression AspFor { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public DynamicInputTagHelper(DynamicInputTagHelperService service) : base(service)
        {
        }
    }
}
