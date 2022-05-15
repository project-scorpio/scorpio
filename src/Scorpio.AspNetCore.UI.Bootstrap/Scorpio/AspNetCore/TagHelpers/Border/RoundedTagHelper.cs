using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Border
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement(HtmlTargetElementAttribute.ElementCatchAllTarget, Attributes = "rounded")]
    public class RoundedTagHelper : TagHelper<RoundedTagHelper, RoundedTagHelperService>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public RoundedTagHelper(RoundedTagHelperService service) : base(service)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public RoundedType Rounded { get; set; } = RoundedType.Default;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output) => base.ProcessAsync(context, output);
    }
}
