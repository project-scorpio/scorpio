namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    /// <summary>
    /// 
    /// </summary>
    public class DropdownTagHelper : TagHelper<DropdownTagHelper, DropdownTagHelperService>
    {
        /// <summary>
        /// 
        /// </summary>
        public Direction Direction { get; set; } = Direction.Down;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public DropdownTagHelper(DropdownTagHelperService service) : base(service)
        {
        }

    }
}
