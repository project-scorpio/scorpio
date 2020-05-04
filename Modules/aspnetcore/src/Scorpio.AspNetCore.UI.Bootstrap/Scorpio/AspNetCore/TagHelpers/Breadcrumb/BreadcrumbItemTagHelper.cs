namespace Scorpio.AspNetCore.TagHelpers.Breadcrumb
{

    /// <summary>
    /// 
    /// </summary>
    
    public class BreadcrumbItemTagHelper : TagHelper<BreadcrumbItemTagHelper, BreadcrumbItemTagHelperService>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagHelperService"></param>
        public BreadcrumbItemTagHelper(BreadcrumbItemTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
