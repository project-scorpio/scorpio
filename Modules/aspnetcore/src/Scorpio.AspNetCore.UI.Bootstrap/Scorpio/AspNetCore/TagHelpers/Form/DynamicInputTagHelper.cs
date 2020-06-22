using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers.Form
{
    /// <summary>
    /// 
    /// </summary>
    public class DynamicInputTagHelper : TagHelper<DynamicInputTagHelper, DynamicInputTagHelperService>
    {
        /// <summary>
        /// 
        /// </summary>
        public ModelExpression AspFor { get; set; }

      

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public DynamicInputTagHelper(DynamicInputTagHelperService service) : base(service)
        {
           
        }
    }
}
