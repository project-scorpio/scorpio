using System.Collections.Generic;

namespace Scorpio.AspNetCore.TagHelpers.Collapse
{
    internal class AccordionItem
    {
        public int Order { get; set; }

        public string Content { get; set; }
    }

    internal class AccordionItemList : List<AccordionItem>
    {

    }
}
