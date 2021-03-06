﻿using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Form
{
    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Critical Code Smell", "S4487:Unread \"private\" fields should be removed", Justification = "<挂起>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:删除未读的私有成员", Justification = "<挂起>")]
    public class DynamicInputTagHelperService : TagHelperService<DynamicInputTagHelper>
    {
        private readonly IHtmlGenerator _htmlGenerator;
        private readonly HtmlEncoder _htmlEncoder;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlGenerator"></param>
        /// <param name="htmlEncoder"></param>
        public DynamicInputTagHelperService(IHtmlGenerator htmlGenerator, HtmlEncoder htmlEncoder)
        {
            _htmlGenerator = htmlGenerator;
            _htmlEncoder = htmlEncoder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output) => base.Process(context, output);
    }

}