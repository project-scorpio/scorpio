using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Mvc.Rendering
{
    public static class TagBuilderExtensions
    {
        public static TagBuilder AddClass(this TagBuilder tagBuilder,string className)
        {
            tagBuilder.AddCssClass(className);
            return tagBuilder;
        }

        public static TagBuilder AddAttribute(this TagBuilder tagBuilder, string name,string value)
        {
            tagBuilder.MergeAttribute(name,value,true);
            return tagBuilder;
        }


        public static TagBuilder Id(this TagBuilder tagBuilder,string id)
        {
            tagBuilder.GenerateId(id, "-");
            return tagBuilder;
        }

        public static TagBuilder Content(this TagBuilder tagBuilder,IHtmlContent content)
        {
            tagBuilder.InnerHtml.AppendHtml(content);
            return tagBuilder;
        }

        public static TagBuilder Content(this TagBuilder tagBuilder, string content)
        {
            tagBuilder.InnerHtml.AppendHtml(new HtmlString(content));
            return tagBuilder;
        }


        public static TagBuilder AddChild(this TagBuilder tagBuilder,Func<TagBuilder,TagBuilder> func)
        {
            var child = func(tagBuilder);
            tagBuilder.InnerHtml.AppendHtml(child);
            return tagBuilder;
        }
    }
}
