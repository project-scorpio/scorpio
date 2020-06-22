using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers.Form
{
    /// <summary>
    /// 
    /// </summary>
    public class InputTagHelper : TagHelper<InputTagHelper, InputTagHelperService>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Orientation Orientation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Size Size { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public InputTagHelper(InputTagHelperService service) : base(service)
        {

        }
    }
}
