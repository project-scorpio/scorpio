using Microsoft.AspNetCore.Razor.TagHelpers;

using Xunit;

namespace Scorpio.AspNetCore.TagHelpers.Form
{
    /// <summary>
    /// 
    /// </summary>
    public class DynamicInputTagHelper_Tests : AspNetCoreUiBootstrapTestBase
    {
        [Fact]
        public void Default() => this.Test<DynamicInputTagHelper>(t => { }, c => { }, o => o.AddAttribute("id", "id"), (a, c, o) => o.ShouldJustHasClasses());


    }
}
