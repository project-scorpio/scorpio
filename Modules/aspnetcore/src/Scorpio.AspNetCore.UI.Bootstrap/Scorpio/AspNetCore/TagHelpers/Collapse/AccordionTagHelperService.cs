using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Collapse
{
    /// <summary>
    /// 
    /// </summary>
    public class AccordionTagHelperService : TagHelperService<AccordionTagHelper>
    {
        public override void Init(TagHelperContext context)
        {
            context.InitValue<AccordionItemList>();
            base.Init(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AddClass("card");
            output.Attributes.Add("id", TagHelper.Id);
            await output.GetChildContentAsync();
            var content = ApplyContent(context);
            output.Content.AppendHtml(content);
        }

        private string ApplyContent(TagHelperContext context)
        {
            var children = context.GetValue<AccordionItemList>().OrderBy(i => i.Order).ToArray();
            for (int i = 0; i < children.Length; i++)
            {
                ref AccordionItem item = ref children[i];
                item.Content = item.Content.Replace("__PARENT_ID__", $"#{TagHelper.Id}");
                if (i == children.Length - 1)
                {
                    item.Content = item.Content.Replace("__LAST_CARD_BODY__", "border-top").Replace("__LAST_CARD_HEADER__", "border-bottom-0");
                }
                else
                {
                    item.Content = item.Content.Replace("__LAST_CARD_BODY__", "border-bottom").Replace("__LAST_CARD_HEADER__", "");
                }
            }
            return string.Concat(children.Select(c => c.Content));
        }
    }
}