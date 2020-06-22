using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers.Form
{
    /// <summary>
    /// 
    /// </summary>
    public class FormTagHelper : TagHelper<FormTagHelper, FormTagHelperService>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public FormTagHelper(FormTagHelperService service) : base(service)
        {
        }
    }
}
