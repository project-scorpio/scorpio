using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Button
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("a", Attributes = "button", TagStructure = TagStructure.NormalOrSelfClosing)]
    [HtmlTargetElement("input", Attributes = "button", TagStructure = TagStructure.WithoutEndTag)]
    public class LinkButtonTagHelper : TagHelper<LinkButtonTagHelper, LinkButtonTagHelperService>, IButtonTagHelperBase
    {
        /// <summary>
        /// 
        /// </summary>
        [HtmlAttributeName("button")]
        public ButtonType ButtonType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Size Size { get; set; } = Size.Default;

        /// <summary>
        /// 
        /// </summary>
        public bool OutLine { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Block { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? Disabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FontIconType IconType { get; } = FontIconType.FontAwesome;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public LinkButtonTagHelper(LinkButtonTagHelperService service) 
            : base(service)
        {

        }
    }
}
