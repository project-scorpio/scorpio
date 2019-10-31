using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers.Alerts
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("alert")]
    public class AlertTagHelper : TagHelper<AlertTagHelper, AlertTagHelperService>
    {
        /// <summary>
        /// 
        /// </summary>
        public AlertType Type { get; set; } = AlertType.Default;

        /// <summary>
        /// 
        /// </summary>
        public bool? Dismissible { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public AlertTagHelper(AlertTagHelperService service) : base(service)
        {

        }
    }
}
