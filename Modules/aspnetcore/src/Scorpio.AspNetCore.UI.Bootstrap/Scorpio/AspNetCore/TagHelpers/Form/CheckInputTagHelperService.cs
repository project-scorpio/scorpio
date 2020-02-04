using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
namespace Scorpio.AspNetCore.TagHelpers.Form
{
    public class CheckInputTagHelperService:TagHelperService<CheckInputTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.PreElement.AppendHtml("<div class=\"form-check\">");
            output.AddClass("form-check-input");
            output.PostElement.AppendHtml($"<label class=\"form-check-label\" for=\"{output.Attributes["id"].Value}\">{ (TagHelper.Text.IsNullOrWhiteSpace()? TagHelper.Title:TagHelper.Text)}</label></div>");

        }
    }
}