using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Form
{
    public class InputTagHelperService : TagHelperService<InputTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!output.Attributes.ContainsName("placeholder"))
            {
                output.AddAttribute("placeholder", $"请输入{TagHelper.Title}");
            }
            var old = output.PreElement.GetContent();
            output.PreElement.Clear();
            switch (TagHelper.Orientation)
            {
                case Orientation.Horizontal:
                    output.PreElement.AppendHtml($"<div class=\"form-group row\"><label class=\"col-md-2 col-form-label{TagHelper.Size switch { Size.Large => " col-form-label-lg",Size.Small=> " col-form-label-sm",_=>"" }}\" for=\"{output.Attributes["id"].Value}\">{TagHelper.Title}</label><div class=\"col-md-10\">{old}");
                    output.PostElement.AppendHtml($"</div></div>");
                    break;
                default:
                    output.PreElement.AppendHtml($"<div class=\"form-group\"><label for=\"{output.Attributes["id"]}\">{TagHelper.Title}</label>{old}");
                    output.PostElement.AppendHtml($"</div>");
                    break;
            }
            switch (output.Attributes["type"].Value.ToString())
            {
                case "checkbox":
                case "radio":
                    return;
                default:
                    break;
            }
            output.AddClass("form-control");
            switch (TagHelper.Size)
            {
                case Size.Small:
                    output.AddClass("form-control-sm");
                    break;
                case Size.Large:
                    output.AddClass("form-control-lg");
                    break;
                default:
                    break;
            }
            base.Process(context, output);
        }
    }
}