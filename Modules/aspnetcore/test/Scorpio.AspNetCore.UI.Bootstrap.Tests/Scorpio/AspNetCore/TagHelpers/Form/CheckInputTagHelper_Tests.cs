using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Form
{
    /// <summary>
    /// 
    /// </summary>

    [HtmlTargetElement("input", Attributes = "[type=checkbox]")]
    [HtmlTargetElement("input", Attributes = "[type=radio]")]
    public class CheckInputTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
       
    }
}
