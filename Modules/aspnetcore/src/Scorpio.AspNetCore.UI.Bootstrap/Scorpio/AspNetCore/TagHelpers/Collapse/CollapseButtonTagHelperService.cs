using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Collapse
{
    /// <summary>
    /// 
    /// </summary>
    public class CollapseButtonTagHelperService : TagHelperService<CollapseButtonTagHelper>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            AddCommonAttributes(output);
            if (output.TagName == "button")
            {
                AddButtonAttribute(output);
            }
            if (output.TagName=="a")
            {
                AddLinkAttribute(output);
            }
            base.Process(context, output);
        }

        private void AddButtonAttribute(TagHelperOutput output)
        {
            output.Attributes.Add("data-target", $"{TagHelper.Target}");
        }
        private void AddLinkAttribute(TagHelperOutput output)
        {
            output.Attributes.Add("href", $"{TagHelper.Target}");
        }

        private void AddCommonAttributes(TagHelperOutput output)
        {
            output.Attributes.Add("data-toggle", "collapse");
            output.Attributes.Add("aria-expanded", "false");
            output.Attributes.Add("aria-controls", TagHelper.Target);
        }
    }
}