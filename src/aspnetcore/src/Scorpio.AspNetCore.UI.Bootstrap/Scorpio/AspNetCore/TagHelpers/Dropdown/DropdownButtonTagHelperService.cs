﻿using System.Linq;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    /// <summary>
    /// 
    /// </summary>
    public class DropdownButtonTagHelperService : TagHelperService<DropdownButtonTagHelper>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            switch (TagHelper.DropdownButtonType)
            {
                case DropdownButtonType.Single:
                    output.AddClass("dropdown-toggle");
                    output.AddAttribute("data-toggle", "dropdown");
                    break;
                default:
                    var tagbuilder = new TagBuilder(output.TagName);
                    tagbuilder.MergeAttributes(output.Attributes.ToDictionary(a => a.Name, a => a.Value));
                    tagbuilder.AddClass("dropdown-toggle").AddClass("dropdown-toggle-split").AddAttribute("data-toggle", "dropdown");
                    output.PostElement.AppendHtml(tagbuilder);
                    break;
            }
        }
    }
}