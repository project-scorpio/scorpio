using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Button
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("button", Attributes ="button-type", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ButtonTagHelper : TagHelper<ButtonTagHelper, ButtonTagHelperService>, IButtonTagHelperBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ButtonType ButtonType { get; set; }

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
        public Size Size { get; set; } = Size.Default;

        /// <summary>
        /// 
        /// </summary>
        public string BusyText { get; set; }

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
        public FontIconType IconType { get; set; } = FontIconType.FontAwesome;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public ButtonTagHelper(ButtonTagHelperService service) 
            : base(service)
        {

        }
    }
}

