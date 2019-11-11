using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Scorpio;
using Scorpio.Threading;

namespace Scorpio.AspNetCore.TagHelpers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTagHelper"></typeparam>
    /// <typeparam name="TService"></typeparam>
    public abstract class TagHelper<TTagHelper, TService> : TagHelper
        where TTagHelper : TagHelper<TTagHelper, TService>
        where TService : class, ITagHelperService<TTagHelper>

    {


        /// <summary>
        /// 
        /// </summary>
        protected TService Service { get; }

        /// <summary>
        /// 
        /// </summary>
        public override int Order => Service.Order;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        protected TagHelper(TService service)
        {
            Service = service;
            Service.As<TagHelperService<TTagHelper>>().TagHelper = (TTagHelper)this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void Init(TagHelperContext context)
        {
            Service.Init(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await Service.ProcessAsync(context, output);
        }

    }
}
