namespace Scorpio.AspNetCore.TagHelpers.Button
{
    /// <summary>
    /// 
    /// </summary>
    public class ButtonGroupTagHelper : TagHelper<ButtonGroupTagHelper, ButtonGroupTagHelperService>
    {
        /// <summary>
        /// 
        /// </summary>
        public ButtonGroupDirection Direction { get; set; } = ButtonGroupDirection.Horizontal;

        /// <summary>
        /// 
        /// </summary>
        public Size Size { get; set; } = Size.Default;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagHelperService"></param>
        public ButtonGroupTagHelper(ButtonGroupTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
