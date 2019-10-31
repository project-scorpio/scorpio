using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Collapse
{
    /// <summary>
    /// 
    /// </summary>
    public class AccordionItemTagHelperService : TagHelperService<AccordionItemTagHelper>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var parent = context.GetValue<AccordionTagHelper>("Accordion");
            AddHeader(output);
            output.TagName = "div";
            output.AddClass("collapse");
            output.Attributes.Add("id", TagHelper.Id);
            if (TagHelper.Active ?? false)
            {
                output.AddClass("show");
            }
            output.AddAttribute("aria-labelledby", $"head-{TagHelper.Id}");
            output.AddAttribute("data-parent", $"__PARENT_ID__");
            AddBody(output);
            output.Content.SetContent((await output.GetChildContentAsync()).GetContent());
            var writer = new System.IO.StringWriter();
            output.WriteTo(writer, NullHtmlEncoder.Default);
            var content = writer.ToString();
            output.SuppressOutput();
            context.GetValue<AccordionItemList>().Add(new AccordionItem { Order = TagHelper.Order, Content = content });
        }

        private static void AddBody(TagHelperOutput output)
        {
            var body = new TagBuilder("div").AddClass("card-body").AddClass("__LAST_CARD_BODY__");
            output.PreContent.AppendHtml(body.RenderStartTag());
            output.PostContent.AppendHtml(body.RenderEndTag());
        }

        private void AddHeader(TagHelperOutput output)
        {
            var header = new TagBuilder("div")
                .AddClass("card-header").AddClass("__LAST_CARD_HEADER__")
                .Id($"head-{TagHelper.Id}")
                .AddChild(h =>
                    new TagBuilder("h5")
                    .AddClass("mb-0")
                    .AddChild(b =>
                        new TagBuilder("button")
                        .AddClass("btn btn-link")
                        .AddAttribute("data-toggle", "collapse")
                        .AddAttribute("data-target", $"#{TagHelper.Id}")
                        .AddAttribute("aria-expanded", "true")
                        .AddAttribute("aria-controls", $"{TagHelper.Id}")
                        .Content(TagHelper.Title)));
            output.PreElement.AppendHtml(header);
        }
    }
}

//    <div class="card-header" id="headingOne">
//    <h5 class="mb-0">
//        <button class="btn btn-link" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
//            Collapsible Group Item #1
//        </button>
//    </h5>
//</div>
//<div id = "collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#accordion">
//    <div class="card-body" border="Bottom">
//        Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch.Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS.
//    </div>
//</div>
//<div class="card-header" id="headingThree" border="SubtractiveBottom">
//    <h5 class="mb-0">
//        <button class="btn btn-link collapsed" data-toggle="collapse" data-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
//            Collapsible Group Item #3
//        </button>
//    </h5>
//</div>
//<div id = "collapseThree" class="collapse" aria-labelledby="headingThree" data-parent="#accordion">
//    <div class="card-body" border="Top">
//        Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch.Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS.
//    </div>
//</div>