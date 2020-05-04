namespace Scorpio.AspNetCore.TagHelpers.Button
{
    /// <summary>
    /// 
    /// </summary>
    public interface IButtonTagHelperBase
    {
        /// <summary>
        /// 
        /// </summary>
        ButtonType ButtonType { get; }

        /// <summary>
        /// 
        /// </summary>
        bool OutLine { get; }

        /// <summary>
        /// 
        /// </summary>
        Size Size { get; }

        /// <summary>
        /// 
        /// </summary>
        bool Block { get; }

        /// <summary>
        /// 
        /// </summary>
        string Text { get; }

        /// <summary>
        /// 
        /// </summary>
        string Icon { get; }

        /// <summary>
        /// 
        /// </summary>
        bool? Disabled { get; }

        /// <summary>
        /// 
        /// </summary>
        FontIconType IconType { get; }
    }
}