using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTagHelper"></typeparam>
    public abstract class TagHelperService<TTagHelper> : ITagHelperService<TTagHelper>
                 where TTagHelper : TagHelper

    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int Order { get; }

        /// <summary>
        /// 
        /// </summary>
        public TTagHelper TagHelper { get ; internal set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public virtual void Init(TagHelperContext context)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public virtual void Process(TagHelperContext context, TagHelperOutput output)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public virtual Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            Process(context, output);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="output"></param>
        /// <param name="htmlEncoder"></param>
        /// <returns></returns>
        protected virtual string RenderTagHelperOutput(TagHelperOutput output, HtmlEncoder htmlEncoder)
        {
            using (var writer = new StringWriter())
            {
                output.WriteTo(writer, htmlEncoder);
                return writer.ToString();
            }
        }

    }
}
