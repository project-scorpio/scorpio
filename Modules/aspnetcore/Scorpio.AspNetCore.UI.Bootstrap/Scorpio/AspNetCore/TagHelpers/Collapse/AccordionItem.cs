using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers.Collapse
{
    internal class AccordionItem
    {
        public int Order { get; set; }

        public string Content { get; set; }
    }

    internal class AccordionItemList:List<AccordionItem>
    {

    }
}
