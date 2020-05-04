using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers.Breadcrumb
{
    /// <summary>
    /// 
    /// </summary>
    public class BreadcrumbTagHelper : TagHelper<BreadcrumbTagHelper, BreadcrumbTagHelperService>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagHelperService"></param>
        public BreadcrumbTagHelper(BreadcrumbTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
