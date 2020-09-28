using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Border
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement(HtmlTargetElementAttribute.ElementCatchAllTarget, Attributes = "border")]
    public class BorderTagHelper : TagHelper<BorderTagHelper, BorderTagHelperService>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public BorderTagHelper(BorderTagHelperService service) : base(service)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public BorderType Border { get; set; } = BorderType.All;

       
    }
}
