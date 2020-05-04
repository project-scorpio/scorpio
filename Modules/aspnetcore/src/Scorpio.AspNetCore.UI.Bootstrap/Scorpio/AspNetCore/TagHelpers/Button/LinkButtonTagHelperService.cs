using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Button
{
    /// <summary>
    /// 
    /// </summary>
    public class LinkButtonTagHelperService : ButtonTagHelperServiceBase<LinkButtonTagHelper>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            AddType(context, output);
            AddRole(context, output);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        protected virtual void AddType(TagHelperContext context, TagHelperOutput output)
        {
            if (!output.Attributes.ContainsName("type") &&
                output.TagName.Equals("input", StringComparison.InvariantCultureIgnoreCase))
            {
                output.Attributes.Add("type", "button");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        protected virtual void AddRole(TagHelperContext context, TagHelperOutput output)
        {
            if (!output.Attributes.ContainsName("role"))
            {
                output.Attributes.Add("role", "button");
            }
        }
    }
}