using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{

    public class DropdownButtonTagHelperService : TagHelperService<DropdownButtonTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            switch (TagHelper.DropdownButtonType)
            {
                case DropdownButtonType.Single:
                    output.AddClass("dropdown-toggle");
                    output.AddAttribute("data-toggle", "dropdown");
                    break;
                case DropdownButtonType.Split:
                    var tagbuilder = new TagBuilder(output.TagName);
                    tagbuilder.MergeAttributes(output.Attributes.ToDictionary(a=>a.Name,a=>a.Value));
                    tagbuilder.AddClass("dropdown-toggle").AddClass("dropdown-toggle-split").AddAttribute("data-toggle", "dropdown");
                    output.PostElement.AppendHtml(tagbuilder);
                    break;
                default:
                    break;
            }
            base.Process(context, output);
        }
    }
}