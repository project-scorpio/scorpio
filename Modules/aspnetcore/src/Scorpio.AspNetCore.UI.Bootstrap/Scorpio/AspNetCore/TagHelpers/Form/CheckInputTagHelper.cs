using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers.Form
{

    [HtmlTargetElement("input", Attributes = "[type=checkbox]")]
    [HtmlTargetElement("input", Attributes = "[type=radio]")]
    public class CheckInputTagHelper : TagHelper<CheckInputTagHelper, CheckInputTagHelperService>
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public CheckInputTagHelper(CheckInputTagHelperService service) : base(service)
        {
        }
    }
}
