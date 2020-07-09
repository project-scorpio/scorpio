using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Form
{
    /// <summary>
    /// 
    /// </summary>

    [HtmlTargetElement("input", Attributes = "[type=checkbox]")]
    [HtmlTargetElement("input", Attributes = "[type=radio]")]
    public class CheckInputTagHelper : TagHelper<CheckInputTagHelper, CheckInputTagHelperService>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public CheckInputTagHelper(CheckInputTagHelperService service) : base(service)
        {
        }
    }
}
