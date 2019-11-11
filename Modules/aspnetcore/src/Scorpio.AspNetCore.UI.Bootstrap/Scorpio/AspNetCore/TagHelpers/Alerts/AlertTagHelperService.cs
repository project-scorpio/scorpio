using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
namespace Scorpio.AspNetCore.TagHelpers.Alerts
{
    /// <summary>
    /// 
    /// </summary>
    public class AlertTagHelperService : TagHelperService<AlertTagHelper>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            AddClasses(context, output);
            AddDismissButtonIfDismissible(context, output);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        protected virtual void AddClasses(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("role", "alert");
            output.AddClass("alert");

            if (TagHelper.Type != AlertType.Default)
            {
                output.AddClass("alert-" + TagHelper.Type.ToString().ToLowerInvariant());
            }

            if (TagHelper.Dismissible ?? false)
            {
                output.AddClass("alert-dismissible");
                output.AddClass("fade");
                output.AddClass("show");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        protected virtual void AddDismissButtonIfDismissible(TagHelperContext context, TagHelperOutput output)
        {
            if (!TagHelper.Dismissible ?? true)
            {
                return;
            }

            var buttonAsHtml =
                "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">" + Environment.NewLine +
                "    <span aria-hidden=\"true\">&times;</span>" + Environment.NewLine +
                " </button>";

            output.PostContent.SetHtmlContent(buttonAsHtml);
        }

    }
}