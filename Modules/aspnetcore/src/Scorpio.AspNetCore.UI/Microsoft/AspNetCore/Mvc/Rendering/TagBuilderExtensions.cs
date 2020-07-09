using System;

using Microsoft.AspNetCore.Html;

namespace Microsoft.AspNetCore.Mvc.Rendering
{
    /// <summary>
    /// 
    /// </summary>
    public static class TagBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static TagBuilder AddClass(this TagBuilder tagBuilder, string className)
        {
            tagBuilder.AddCssClass(className);
            return tagBuilder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TagBuilder AddAttribute(this TagBuilder tagBuilder, string name, string value)
        {
            tagBuilder.MergeAttribute(name, value, true);
            return tagBuilder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TagBuilder Id(this TagBuilder tagBuilder, string id)
        {
            tagBuilder.GenerateId(id, "-");
            return tagBuilder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static TagBuilder Content(this TagBuilder tagBuilder, IHtmlContent content)
        {
            tagBuilder.InnerHtml.AppendHtml(content);
            return tagBuilder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static TagBuilder Content(this TagBuilder tagBuilder, string content)
        {
            tagBuilder.InnerHtml.AppendHtml(new HtmlString(content));
            return tagBuilder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TagBuilder AddChild(this TagBuilder tagBuilder, Func<TagBuilder, TagBuilder> func)
        {
            var child = func(tagBuilder);
            tagBuilder.InnerHtml.AppendHtml(child);
            return tagBuilder;
        }
    }
}
