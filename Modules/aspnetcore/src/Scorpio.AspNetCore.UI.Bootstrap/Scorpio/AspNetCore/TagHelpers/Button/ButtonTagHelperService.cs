using System;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Scorpio.AspNetCore.TagHelpers.Button
{
    /// <summary>
    /// 
    /// </summary>
    public class ButtonTagHelperService : ButtonTagHelperServiceBase<ButtonTagHelper>
    {
        /// <summary>
        /// 
        /// </summary>
        protected static readonly string DataBusyTextAttributeName = "data-busy-text";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            output.TagName = "button";
            AddType(context, output);
            AddBusyText(context, output);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        protected virtual void AddType(TagHelperContext context, TagHelperOutput output)
        {
            if (output.Attributes.ContainsName("type"))
            {
                return;
            }

            output.Attributes.Add("type", "button");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        protected virtual void AddBusyText(TagHelperContext context, TagHelperOutput output)
        {
            var busyText = TagHelper.BusyText ?? "...";
            if (busyText.IsNullOrWhiteSpace())
            {
                return;
            }

            output.Attributes.SetAttribute(DataBusyTextAttributeName, busyText);
        }
    }
}