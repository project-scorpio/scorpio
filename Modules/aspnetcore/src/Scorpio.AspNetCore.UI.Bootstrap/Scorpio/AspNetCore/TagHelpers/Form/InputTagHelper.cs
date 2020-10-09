using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Form
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("input")]
    public class InputTagHelper : TagHelper<InputTagHelper, InputTagHelperService>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Orientation Orientation { get; set; } = Orientation.Vertical;

        /// <summary>
        /// 
        /// </summary>
        public Size Size { get; set; } = Size.Default;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public InputTagHelper(InputTagHelperService service) : base(service)
        {

        }
    }
}
